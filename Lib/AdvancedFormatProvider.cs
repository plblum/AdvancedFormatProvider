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
For use as an IFormatProvider in String.Format() and other consumers
of this interface.
It supports all existing formats supplied by the .net framework, 
plus those included with this product and you create.
AdvancedFormatProvider makes it easy to drop in new formatters,
tools that convert a value into a string based on a format string in
the {#:here} token. (For example, {0:c} and {1:yyyy-MM-dd}.)
The formatters are registered globally so you only install them once,
in Application_Start.

It pre-installs support for these formatters:
 * IntegerFormatter - For use with integers, or to format decimals
   like integers. Uses the formatter "i" or "I".
   Similar to the N0 formatter built into the native system,
   except it has an option to omit group separators.
 * CurrencyAndPercentFormatter - For use with numbers to format
   them either as a currency or percent. This overrides
   the native formatters, using the same format strings ("c", "C", "p", "P"),
   adding rules to omit the group separator, currency or percent symbol,
   or decimal digits when all zero.

Add more by creating a class that implements the IAdvancedFormatterPlugIn interface.
In Application_Start, call AdvancedFormatProvider.RegisterFormatPlugIn() with
an instance of the class.

It can be used in several ways:
 * Pass AdvancedFormatProvider.Current as the first parameter of String.Format
   when you don't need to specify a custom CultureInfo object.
   It uses the CultureInfo.CurrentUICulture.
   text = String.Format(AdvancedFormatProvider.Current, "{0:C}", 1.0)
 * Create an AdvancedFormatProvider with the desired CultureInfo
   and pass it as the first parameter of String.Format.
   text = String.Format(new AdvancedFormatProvider(cultureInfo), "{0:C}", 1.0)
 * Call the static function AdvancedFormatProvider.Format with or without
   a cultureInfo object. It is a wrapper around String.Format that knows
   how to establish the AdvancedFormatProvider that is passed into String.Format.
------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// For use as an IFormatProvider in String.Format() and other consumers
/// of this interface.
/// </summary>
public class AdvancedFormatProvider : IFormatProvider, ICustomFormatter
{
   public AdvancedFormatProvider()
   {
   }

   public AdvancedFormatProvider(IFormatProvider parentProvider)
   {
      ParentProvider = parentProvider;
   }

   public IFormatProvider ParentProvider;

/// <summary>
/// Add a formatter plugin.
/// </summary>
/// <remarks>
/// <para>Your formatter object will be maintained globally. Do not attempt to modify it after application startup.</para>
/// </remarks>
/// <param name="customFormatter">The formatter which is a class that implements IAdvancedFormatterPlugin.</param>
/// <param name="first">When true, insert the formatter first in the list, allowing it to override other
/// formatters that may handle the same token. When false, it is added last in the list.</param>
   public static void RegisterFormatPlugIn(IAdvancedFormatterPlugin customFormatter, bool first = false)
   {
      lock (typeof(AdvancedFormatProvider))
      {
         if (_registeredFormatters == null)
         {
            _registeredFormatters = new List<IAdvancedFormatterPlugin>();
            RegisterDefaultFormatters(_registeredFormatters);
         }
         if (first)
            AdvancedFormatProvider._registeredFormatters.Insert(0, customFormatter);
         else
            AdvancedFormatProvider._registeredFormatters.Add(customFormatter);
      }
   }

/// <summary>
/// All registered Formatters with the format tokens they support.
/// There is only one format token (like "c", "p", etc) but there can be multiple formatProviders
/// registered. If this list does not contain a mapped provider, AdvancedFormatProvider's Format()
/// method falls back to using the default FormatProvider.
/// </summary>
   protected static List<IAdvancedFormatterPlugin> _registeredFormatters;
      
   protected static void RegisterDefaultFormatters(List<IAdvancedFormatterPlugin> registeredProviders)
   {
      registeredProviders.Add(new IntegerFormatter());
      registeredProviders.Add(new CurrencyAndPercentFormatter());
   }

   #region IFormatProvider Members

   public object GetFormat(Type formatType)
   {
      if (formatType == typeof(ICustomFormatter))
         return this;
      return null;
   }

   #endregion

   #region ICustomFormatter Members

/// <summary>
/// Attempts to find a matching provider by its format name and uses
/// that. If not found, it uses the standard String.Format to handle the format.
/// </summary>
/// <param name="format"></param>
/// <param name="arg"></param>
/// <param name="formatProvider"></param>
/// <returns></returns>
   public string Format(string format, object arg, IFormatProvider formatProvider)
   {
      if (_registeredFormatters == null)
         lock (typeof(AdvancedFormatProvider))
         {
            if (_registeredFormatters == null)
            {
               _registeredFormatters = new List<IAdvancedFormatterPlugin>();
               RegisterDefaultFormatters(_registeredFormatters);
            }
         }

      if (formatProvider is AdvancedFormatProvider)
         formatProvider = ParentProvider; 
      if (formatProvider == null)
         formatProvider = CultureInfo.CurrentUICulture; 

      foreach (IAdvancedFormatterPlugin item in _registeredFormatters)
      {
         if (item.Supported(format, arg != null ? arg.GetType() : null))
         {
            return item.Format(format, arg, formatProvider);
         }
      }

      return String.Format(formatProvider, "{0:" + format + "}", arg);  
   }

   #endregion

/// <summary>
/// Replacement for (and wrapper around) String.Format that lets the AdvancedFormatProvider
/// have a first shot at the format string. If there are no register Formatters,
/// the traditional String.Format is used to handle the native format strings.
/// </summary>
/// <param name="formatProvider"></param>
/// <param name="format"></param>
/// <param name="args"></param>
/// <returns></returns>
   public static string Format(IFormatProvider formatProvider, string format, params object[] args)
   {
      if (formatProvider == null)
         return Format(format, args);
      AdvancedFormatProvider afp = new AdvancedFormatProvider();
      afp.ParentProvider = formatProvider;
      return String.Format(afp, format, args);  // returns null when there is no match
   }


   public static string Format(string format, params object[] args)
   {
      return String.Format(Current, format, args);
   }

/// <summary>
/// A global instance of AdvancedFormatProvider that you can use in String.Format's first parameter,
/// so long as you don't need to specify a parent FormatProvider.
/// This will use the CultureInfo.CurrentUICulture as the parent FormatProvider.
/// </summary>
   public static AdvancedFormatProvider Current = new AdvancedFormatProvider();

}

