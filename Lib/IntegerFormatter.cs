/* -----------------------------------------------------------
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
-------------------------------------------------------------
Purpose:
Formats numbers as integers (including floating point values).
It is used as a default plugin for AdvancedFormatProvider.
Because it implements ICustomFormatter, it can be used with your
own IFormatProvider class.

Format must start with "i" or "I". It can be followed with these modifiers:
* "-," - omit the group separator character. When not present, the group separator is used.
* "," - include the group separator. Not necessary because when there is no comma, 
        it includes the group separator.
* number - The number of digits to show. If the value is less than this,
       lead zeros are used to fill.
Examples: 
 * {0:i}, {0:I}, 
   {0:i,} and {0:I,}   10000 -> "10,000"  (includes group separators)
 * {0:i3} and {0:i,3}  1 -> "001"         (includes group separators and fills to 3 digits)
 * {0:I-,}             10000 -> "10000"   (omits group separators) 
 * {0:I-,7}            11234 -> "0011234" (omits group separators and fills to 8 digits)

Internally all values are converted to Int64. This conversion 
rounds up decimal values above .5.

------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;


/// <summary>
/// Formats numbers as integers (including floating point values).
/// </summary>
/// <remarks>
/// <para>Format must start with "i" or "I". It can be followed with these modifiers.</para>
/// <para> "-," - omit the group separator character. When not present, the group separator is used.</para>
/// <para> "," - include the group separator. Not necessary because when there is no comma,
/// it includes the group separator.</para>
/// <para> number - The number of digits to show. If the value is less than this,
/// lead zeros are used to fill.</para>
/// <para>Examples: {0:i}, {0:I}, {0:i,} and {0:I,}(includes group separators), 
/// {0:i3} and {0:i,3} (includes group separators and fills to 3 digits), 
/// {0:I-,} (omits group separators), 
/// {0:I-,3) (omits group separators and fills to 3 digits), 
/// </para>
/// <para>Internally all values are converted to Int64. This conversion rounds up decimal values
/// above .5.</para>
/// </remarks>
public class IntegerFormatter : IAdvancedFormatterPlugin
{
   #region IAdvancedFormatterPlugin Members

   public bool Supported(string format, Type valueType)
   {
      Match m = formatRE.Match(format);
      if (!m.Success)
         return false;
      if (valueType != null)
      {
         switch (Type.GetTypeCode(valueType))
         {
            case TypeCode.Byte:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.SByte:
            case TypeCode.Single:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
               return true;
            default:
               return false;
         }
      }
      return true;
   }

   public string Format(string format, object arg, IFormatProvider formatProvider)
   {
      Int64 value = Convert.ToInt64(arg);
      if (formatProvider == null)
         formatProvider = CultureInfo.CurrentUICulture;

      Match m = formatRE.Match(format);
      if (!m.Success)
         throw new ArgumentException("Only supports i or I");

      format = m.Groups["format"].Value;
      int fillzeros = 0;
      if (!String.IsNullOrEmpty(m.Groups["fill"].Value))
         fillzeros = Convert.ToInt32(m.Groups["fill"].Value);

      string result = String.Format(formatProvider, "{0:N0}", value);

      // for some reason, the minus character is always used instead of NumberFormatInfo.NegativeSign.
      NumberFormatInfo nfi = formatProvider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
      if (nfi == null)
         nfi = CultureInfo.CurrentUICulture.NumberFormat;
      result = result.Replace("-", nfi.NegativeSign);

      string parms = m.Groups["parm"].Value;
      if (!String.IsNullOrEmpty(parms) && parms.StartsWith("-"))
      {
         result = result.Replace(nfi.NumberGroupSeparator, "");
      }

      if (fillzeros > 0)
      {
         int len = result.Length;
         if (fillzeros - len > 0)
            result = "0000000000".Substring(0, fillzeros - len) + result;
      }
      return result;
   }

   #endregion
   static Regex formatRE = new Regex(@"^(?<format>I)(?<parm>\-?\,)?(?<fill>\d*)$", RegexOptions.IgnoreCase);
}
