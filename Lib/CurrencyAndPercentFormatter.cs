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
Formats numbers as currencies and percents.

Format must start with "c" or "C" for currency and "p" or "P" for percent.
These values are handled by the native String.Format() rules but overridden here to include more functionality.
At a lower level, it uses the native String.Format() and then strips out the unwanted formatting.

The initial character can be followed with these modifiers:
 * "-$"  - omit the currency symbol. When not present, the currency symbol is used.
 * "$"   - include the currency symbol. Not necessary because when there is no $, 
           it means include the currency symbol
 * "-%"  - omit the percent symbol. When not present, the percent symbol is used.
 * "%"   - include the percent symbol. Not necessary because when there is no %, 
           it means include the percent symbol
 * "-,"  - omit the group separator character. When not present, the group separator is used.
 * ","   - include the group separator. Not necessary because when there is no comma,
           it includes the group separator.
 * "-0"  - omit the floating point part when its zero (the value would be the same as an integer).
 * number - The number of floating point digits to show. If not specified, it uses
           the culture's rule. Use 0 to remove the floating point part. However, if not specified
           and the value is already an integer, the floating point part is always removed.
Examples for currency: 
 * {0:C}, {0:C,}     5.0 -> "$5.00"       (includes currency symbol and group separators) 
                     5000.0 -> "$5,000.00"
                     50   -> "$50"        (because it is an integer passed in)
 * {0:C-$}           5.0  -> "5.00"       (omits currency symbol; includes group separators)
                     5000.0 -> "5,000.00"
 * {0:C-$-,}         5000.0 -> "5000.00"  (omits currency symbol and group separators)
 * {0:C3}, {0:C,3}   5.00 -> "$5.000"     (includes group separators and requires 3 decimal digits)
 * {0:C0}, {0:C,0}   50.0 -> "$50"        (includes group separators and removes decimal digits), 
 * {0:C-,}           5000.0 -> "$5000.00" (omits group separators)
 * {0:C-,3}          5000.0 -> "$5000.000"(omits group separators and requires 3 decimal digits)
 * {0:C-,0}          5000.0 -> "$5000"    (omits group separators and removes decimal digits)
 * {0:C-0}           5000.00 -> "$5,000"  (removes decimal part when zero; includes currency symbol and group separators)
                     5000.01 -> "$5,000.01"
 * {0:C-$-,-0}       5000.0 -> "5000"     (removes decimal part when zero; omits currency symbol and group separators)
                     5000.01 -> "5000.01"
Examples for percent:
 * {0:P}, {0:P,}     0.50 -> "50.00 %"    (includes percent symbol and group separators)
                     50.0 -> "5,000.00 %"
                     50   -> "5,000 %"    (because it is an integer passed in)
 * {0:P-%}           0.50 -> "50.00"      (omits percent symbol; includes group separators)
                     50.0 -> "5,000.00"   
 * {0:P-%-,}         50.0 -> "5000.00"    (omits percent symbol and group separators)
 * {0:P3}, {0:P,3}   0.50 -> "50.000 %"   (includes group separators and requires 3 decimal digits)
 * {0:P0}, {0:P,0}   50.0 -> "$5,000"     (includes group separators and removes decimal digits)
 * {0:P-,}           50.0 -> "5000.0 %"   (omits group separators)
 * {0:P-,3}          50.0 -> "5000.000 %" (omits group separators and requires 3 decimal digits), 
 * {0:P-,0}          50.0 -> "5000 %"     (omits group separators and removes decimal digits)
 * {0:P-0}           0.50 -> "50 %"       (removes decimal part when zero; includes percent symbol and group separators)
                     0.501 -> "50.1 %"
 * {0:P-%-,-0}       0.50 -> "50"         (removes decimal part when zero; omits percent symbol and group separators)
                     0.501 -> "50.1"

------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Formats numbers as currencies and percents.
/// </summary>
/// <remarks>
/// <para>Format must start with "c" or "C" for currency and "p" or "P" for percent. 
/// These values are handled by the native String.Format() rules but overridden here to include more functionality.
/// At a lower level, it uses the native String.Format() and then strips out the unwanted formatting.</para>
/// <para>The initial character can be followed with these modifiers.</para>
/// <para>"-$" - omit the currency symbol. When not present, the currency symbol is used.</para>
/// <para> "$" - include the currency symbol. Not necessary because when there is no $, it means include the currency symbol</para>
/// <para>"-%" - omit the percent symbol. When not present, the percent symbol is used.</para>
/// <para> "%" - include the percent symbol. Not necessary because when there is no %, it means include the percent symbol</para>
/// <para> "-," - omit the group separator character. When not present, the group separator is used.</para>
/// <para> "," - include the group separator. Not necessary because when there is no comma,
/// it includes the group separator.</para>
/// <para> "-0" - omit the floating point part when its zero (the value would be the same as an integer).</para>
/// <para> number - The number of floating point digits to show. If not specified, it uses
/// the culture's rule. Use 0 to remove the floating point part. However, if not specified
/// and the value is already an integer, the floating point part is always removed.</para>
/// <para>Examples:  
/// {0:p}, {0:P}, {0:p,}, {0:P,}, {0:c}, {0:C}, {0:c,} and {0:C,} (includes currency or percent symbol and group separators), 
/// {0:P-%} (omits percent symbol; includes group separators), 
/// {0:C-$} (omits currency symbol; includes group separators), 
/// {0:P-%-,} (omits percent symbol and group separators), 
/// {0:C-$-,} (omits currency symbol and group separators), 
/// {0:p3}, {0:P,3}, {0:C3}, {0:c,3} (includes group separators and requires 3 decimal digits), 
/// {0:p0}, {0:P,0}, {0:C0}, {0:c,0} (includes group separators and removes decimal digits), 
/// {0:P-,}, {0:C-,} (omits group separators), 
/// {0:C-,3), {0:P-,3)  (omits group separators and requires 3 decimal digits), 
/// {0:C-,0), {0:P-,0)  (omits group separators and removes decimal digits)
/// {0:P-0} (removes decimal part when zero; includes percent symbol and group separators), 
/// {0:C-0} (removes decimal part when zero; includes currency symbol and group separators), 
/// {0:P-%-,-0} (removes decimal part when zero; omits percent symbol and group separators), 
/// {0:C-$-,-0} (removes decimal part when zero; omits currency symbol and group separators), 
/// </para>
/// </remarks>
public class CurrencyAndPercentFormatter : IAdvancedFormatterPlugin
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
         if (formatProvider == null)
            formatProvider = CultureInfo.CurrentUICulture;

         Match m = formatRE.Match(format);
         if (!m.Success)
            throw new ArgumentException("Only supports C, c, P or p");

         bool isCurrency = format.ToLowerInvariant()[0] == 'c';

         string updatedFormat = m.Groups["format"].Value + m.Groups["precision"].Value;

         string result = String.Format(formatProvider, "{0:" + updatedFormat + "}", arg);

         NumberFormatInfo nfi = formatProvider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
         if (nfi == null)
            nfi = CultureInfo.CurrentUICulture.NumberFormat;

         // if its an integer and no precision is included, strip off floating point part
         if ((m.Groups["precision"].Value.Length == 0) && (arg != null))
            switch (Type.GetTypeCode(arg.GetType()))
            {
               case TypeCode.Byte:
               case TypeCode.Int16:
               case TypeCode.Int32:
               case TypeCode.Int64:
               case TypeCode.SByte:
               case TypeCode.UInt16:
               case TypeCode.UInt32:
               case TypeCode.UInt64:
                  string decSep = isCurrency ? nfi.CurrencyDecimalSeparator : nfi.PercentDecimalSeparator;
                  Regex stripper = new Regex(Regex.Escape(decSep) + @"\d+");
                  result = stripper.Replace(result, "");
                  break;
            }


         foreach (Capture capture in m.Groups["parm"].Captures)
         {
            if (capture.Value.StartsWith("-"))
            {
               Regex stripper = null;
               switch (capture.Value[1])
               {
                  case '$':
                     stripper = new Regex(@"\s*" + Regex.Escape(nfi.CurrencySymbol) + @"\s*");
                     break;
                  case '%':
                     stripper = new Regex(@"\s*" + Regex.Escape(nfi.PercentSymbol) + @"\s*");
                     break;
                  case ',':
                     string groupSep = isCurrency ? nfi.CurrencyGroupSeparator : nfi.PercentGroupSeparator;
                     stripper = new Regex(Regex.Escape(groupSep));
                     break;
                  case '0':
                     string decSep = isCurrency ? nfi.CurrencyDecimalSeparator : nfi.PercentDecimalSeparator;
                     stripper = new Regex(Regex.Escape(decSep) + @"0+(?=$|\D)");
                     break;
               }  // switch
               if (stripper == null)
                  throw new ArgumentException("format parameters must be [-$], [-%], [-,] or [-0].");
               result = stripper.Replace(result, "");
            }  // if
         }  // foreach
         return result;
      }

      #endregion
      static Regex formatRE = new Regex(@"^(?<format>[CP])(?<parm>(?:\-?[$%\,])|(?:\-0))*(?<precision>\d*)$", RegexOptions.IgnoreCase);
   }

