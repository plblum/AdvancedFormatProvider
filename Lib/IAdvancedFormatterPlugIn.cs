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
For Formatter classse that are registered with AdvancedFormatProvider.

The class defines two methods. The Format() method is defined by
the ICustomFormatter interface from which this inherits.
For documentation, see: http://msdn.microsoft.com/en-us/library/system.icustomformatter.aspx

The Supported() method is defined here. See the documentation inline below.

To add your class to AdvancedFormatProvider, in application startup code,
call AdvancedFormatProvider.RegisterFormatPlugIn(your class) passing
an instance of your class. Your object will be maintained globally.
Do not attempt to modify it after application startup.

------------------------------------------------------------*/
using System;

/// <summary>
/// For Formatter classse that are registered with AdvancedFormatProvider.
/// </summary>
public interface IAdvancedFormatterPlugin : ICustomFormatter
{
/// <summary>
/// Called to determine if the registered AdvancedFormatterPlugin class
/// handles the format string.
/// </summary>
/// <param name="format">The format string to evaluate.</param>
/// <param name="valueType">The type of the value that will be formatted. 
/// It can be used to reject types that this formatter cannot handle.
/// Can be null. If null, there should be no evaluation of the value type,
/// with the determination made solely based on the format parameter.</param>
/// <returns>When true, the formatter supports the parameters passed.</returns>
   bool Supported(string format, Type valueType);
}
