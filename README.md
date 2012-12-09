<h1>AdvancedFormatProvider class and plug-ins</h1>

<p>For platform: Microsoft .net 1.1 and higher.</p>

<p>&nbsp;</p>

<p>AdvancedFormatProvider is an <a
href="http://msdn.microsoft.com/en-us/library/system.iformatprovider%28v=vs.100%29.aspx">IFormatProvider</a>
which means it works with <a
href="http://msdn.microsoft.com/en-us/library/system.string.format%28v=vs.100%29.aspx">String.Format()</a>
and other consumers of this interface. For example:</p>

<p><span style='font-family:"Courier New"'>text =
String.Format(AdvancedFormatProvider.Current, &quot;{0:C-$}&quot;, 100.0)</span></p>

<p>It supports all existing formats supplied by the .net
framework, plus those included with this product and you create.</p>

<p>AdvancedFormatProvider makes it easy to drop in new
formatters, tools that convert a value into a string based on a format string
in the <span style='font-family:"Courier New"'>{#:here}</span> token. (For
example, <span style='font-family:"Courier New"'>{0:c}</span> and <span
style='font-family:"Courier New"'>{1:yyyy-MM-dd}</span>.)</p>

<p>The formatters are registered globally so you only install
them once, in Application_Start.</p>

<p>&nbsp;</p>

<p>It pre-installs support for these formatters:</p>

<ul style='margin-top:0in' type=disc>
 <li><b>IntegerFormatter</b> - For use with integers, or to
     format decimals like integers. Uses the symbol &quot;i&quot; or
     &quot;I&quot; such as <span style='font-family:"Courier New"'>{0:I}</span>
     and <span style='font-family:"Courier New"'>{0:i}</span>. It is similar to
     the <span style='font-family:"Courier New"'><a
     href="http://msdn.microsoft.com/en-us/library/dwhawy9k.aspx#CFormatString">N0<span
     style='font-family:"Times New Roman"'> formatter</span></a></span> built
     into .net, except it has an option to omit group separators.

<p style='margin-left:.5in'>The symbol can be followed with
these modifiers:</p>

<ul style='margin-top:0in' type=disc>
 <ul style='margin-top:0in' type=circle>
  <li><span style='font-family:"Courier New"'>{0:I-,}</span> -
      Omit the group separator character. When not present, the group separator
      is used.</li>
  <li><span style='font-family:"Courier New"'>{0:I,}</span> -
      Include the group separator. Not necessary because when there is no
      comma, it includes the group separator.</li>
  <li><span style='font-family:"Courier New"'>{0:I#}</span> - The
      number of digits to show where # is shown. If the value is less than
      this, lead zeros are used to fill.</li>
 </ul>
</ul>

<p style='margin-left:.5in'><i>Examples: </i></p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:I}</span>,  <span
style='font-family:"Courier New"'>{0:I,}</span>      10000 -&gt;
&quot;10,000&quot;       (includes group separators)</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:I3}</span>, <span
style='font-family:"Courier New"'>{0:I,3} </span> 1 -&gt;
&quot;001&quot;                  (includes group separators and fills to 3
digits)</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:I-,}</span>            
      10000 -&gt; &quot;10000&quot;        (omits group separators) </p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:I-,7}</span>           
    11234 -&gt; &quot;0011234&quot;    (omits group separators and fills to 8
digits)</p>
</li>
</ul>
<ul style='margin-top:0in' type=disc>
 <li><b>CurrencyAndPercentFormatter</b> - For use with numbers
     to format them either as a currency or percent. This overrides the native
     formatters, using the same format strings (&quot;c&quot;, &quot;C&quot;,
     &quot;p&quot;, &quot;P&quot;), adding rules to omit the group separator,
     currency or percent symbol, or decimal digits when all zero.
<p style='margin-left:.5in'>The initial character can be
followed with these modifiers:</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-$}</span> - Omit the
currency symbol. When not present, the currency symbol is used.</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C$}</span> - Include
the currency symbol. Not necessary because when there is no $, it means include
the currency symbol</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P-%}</span> - Omit the
percent symbol. When not present, the percent symbol is used.</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P%}</span> - Include
the percent symbol. Not necessary because when there is no %, it means include
the percent symbol</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-,}</span> or <span
style='font-family:"Courier New"'>{0:P-,}</span> - Omit the group separator
character. When not present, the group separator is used.</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C,}</span> or <span
style='font-family:"Courier New"'>{0:P,}</span> - Include the group separator.
Not necessary because when there is no comma, it includes the group separator.</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-0}</span> or <span
style='font-family:"Courier New"'>{0:P-0}</span> - Omit the floating point part
when its zero (the value would be the same as an integer).</p>

<p style='margin-left:1.0in;text-indent:-.25in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C#}</span> or <span
style='font-family:"Courier New"'>{0:P#}</span> - The number of floating point
digits to show where # is shown. If not specified, it uses the culture's rule.
Use 0 to remove the floating point part. However, if not specified and the
value is already an integer, the floating point part is always removed.</p>

<p style='margin-left:.5in'><i>Examples for currency:</i></p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C}</span>, <span
style='font-family:"Courier New"'>{0:C,}</span>       5.0 -&gt;
&quot;$5.00&quot;                 (includes currency symbol and group
separators) </p>

<p style='margin-left:2.0in;text-indent:.5in'>5000.0 -&gt;
&quot;$5,000.00&quot;</p>

<p style='margin-left:4.15in;text-indent:-3.4in'>                                          50  
-&gt; &quot;$50&quot;                   (because it is an integer passed in)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-$}</span>                   5.0 
-&gt; &quot;5.00&quot;                  (omits currency symbol; includes group
separators)</p>

<p style='margin-left:2.0in;text-indent:.5in'>5000.0 -&gt;
&quot;5,000.00&quot;</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-$-,}</span>              5000.0
-&gt; &quot;5000.00&quot;       (omits currency symbol and group separators)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C3}</span>, <span
style='font-family:"Courier New"'>{0:C,3}</span>  5.00 -&gt; &quot;$5.000&quot;             (includes
group separators and requires 3 decimal digits)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C0}</span>, <span
style='font-family:"Courier New"'>{0:C,0}</span>  50.0 -&gt; &quot;$50&quot;                  (includes
group separators and removes decimal digits), </p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-,}</span>                   5000.0
-&gt; &quot;$5000.00&quot;     (omits group separators)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-,3}</span>                5000.0
-&gt; &quot;$5000.000&quot;   (omits group separators and requires 3 decimal
digits)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-,0}</span>                5000.0
-&gt; &quot;$5000&quot;          (omits group separators and removes decimal
digits)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-0}</span>                   5000.00
-&gt; &quot;$5,000&quot;       (removes decimal part when zero; includes
currency symbol and group separators)</p>

<p style='margin-left:2.0in;text-indent:.5in'>5000.01 -&gt;
&quot;$5,000.01&quot;</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:C-$-,-0}</span>         5000.0
-&gt; &quot;5000&quot;            (removes decimal part when zero; omits
currency symbol and group separators)</p>

<p style='margin-left:2.0in;text-indent:.5in'>5000.01 -&gt;
&quot;5000.01&quot;</p>

<p style='margin-left:.5in'><i>Examples for percent:</i></p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P}</span>, <span
style='font-family:"Courier New"'>{0:P,}</span>       0.50 -&gt; &quot;50.00
%&quot;           (includes percent symbol and group separators)</p>

<p style='margin-left:2.0in;text-indent:.5in'>50.0 -&gt;
&quot;5,000.00 %&quot;</p>

<p style='margin-left:2.0in;text-indent:.5in'>50   -&gt;
&quot;5,000 %&quot;           (because it is an integer passed in)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P-%}</span>                   0.50
-&gt; &quot;50.00&quot;               (omits percent symbol; includes group
separators)</p>

<p style='margin-left:2.0in;text-indent:.5in'>50.0 -&gt;
&quot;5,000.00&quot;   </p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P-%-,}</span>              50.0
-&gt; &quot;5000.00&quot;           (omits percent symbol and group separators)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P3}</span>, <span
style='font-family:"Courier New"'>{0:P,3}</span>  0.50 -&gt; &quot;50.000
%&quot;         (includes group separators and requires 3 decimal digits)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P0}</span>, <span
style='font-family:"Courier New"'>{0:P,0}</span>  50.0 -&gt; &quot;$5,000&quot;             (includes
group separators and removes decimal digits)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P-,}       </span>50.0
-&gt; &quot;5000.0 %&quot;         (omits group separators)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P-,3}</span>                50.0
-&gt; &quot;5000.000 %&quot;     (omits group separators and requires 3 decimal
digits), </p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P-,0}</span>                50.0
-&gt; &quot;5000 %&quot;            (omits group separators and removes decimal
digits)</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P-0}</span>                   0.50
-&gt; &quot;50 %&quot;                (removes decimal part when zero; includes
percent symbol and group separators)</p>

<p>                                                            0.501
-&gt; &quot;50.1 %&quot;</p>

<p style='margin-left:4.15in;text-indent:-3.4in'><span
style='font-family:"Courier New"'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-family:"Courier New"'>{0:P-%-,-0}</span>         0.50
-&gt; &quot;50&quot;                    (removes decimal part when zero; omits
percent symbol and group separators)</p>

<p>                                                            0.501
-&gt; &quot;50.1&quot;</p>
</li>
</ul>

<p>Add more by creating a class that implements the
IAdvancedFormatterPlugIn interface. </p>

<h2>Getting started</h2>

<p>Either add the Visual Studio project into your application
or add the source code files.</p>

<h3>Using the Visual Studio project file</h3>

<p>After adding the project, add a reference from your
application to the project.</p>

<h3>Using the source code</h3>

<p>The source code files are in C#. If your application is not
in C#, use the Visual Studio project file.</p>

<p>Add the source code files to your application. The source
code files do not specify a namespace. Consider adding your own application’s
namespace to them.</p>

<h2>Using AdvancedFormatProvider</h2>

<p>You can either call <a
href="http://msdn.microsoft.com/en-us/library/system.string.format%28v=vs.100%29.aspx">String.Format()</a>
passing in the AdvancedFormatProvider as the first parameter, or call
AdvancedFormatProvider.Format(), with similar parameters to String.Format.
AdvancedFormatProvider.Format() lets you avoid creating an instance of
AdvancedFormatProvider and makes it easy to pass in a specific <a
href="http://msdn.microsoft.com/en-US/library/system.globalization.cultureinfo%28v=vs.100%29.aspx">System.Globalization.CultureInfo</a>
object.</p>

<h3>Do not need to pass a CultureInfo object</h3>

<p><span style='font-family:"Courier New"'>text =
String.Format(AdvancedFormatProvider.Current, <span style='color:maroon'>&quot;<i>text
with tokens</i>&quot;</span>, value1, value2, <i>etc.</i>)</span></p>

<p><span style='font-family:"Courier New"'>text =
AdvancedFormatProvider.Format(<span style='color:maroon'>&quot;text with tokens&quot;</span>,
value1, value2, <i>etc.</i>)</span></p>

<p><i>Examples</i></p>

<p><span style='font-family:"Courier New"'>text =
String.Format(AdvancedFormatProvider.Current, <span style='color:maroon'>&quot;The
{0} has a value of {1:C}.&quot;</span>, <span style='color:maroon'>&quot;book&quot;</span>,
10.0)</span></p>

<p><span style='font-family:"Courier New"'>text =
AdvancedFormatProvider.Format<span style='color:maroon'>(&quot;The {0} has a
value of {1:C}.&quot;</span>, <span style='color:maroon'>&quot;book&quot;</span>,
10.0</span></p>

<p>Result:</p>

<p>&quot;The book has a value of $10.00.&quot;</p>

<h3>Need to pass a CultureInfo object</h3>

<p><span style='font-family:"Courier New"'>text =
String.Format(<span style='color:blue'>new</span> AdvancedFormatProvider(cultureInfo),
<span style='color:maroon'>&quot;<i>text with tokens</i>&quot;</span>, value1,
value2, <i>etc.</i>)</span></p>

<p><span style='font-family:"Courier New"'>text =
AdvancedFormatProvider.Format(cultureInfo, <span style='color:maroon'>&quot;text
with tokens&quot;</span>, value1, value2, <i>etc.</i>)</span></p>

<p><i>Examples</i></p>

<p><span style='font-family:"Courier New"'>text =
String.Format(AdvancedFormatProvider.Current, <span style='color:maroon'>&quot;The
{0} has a value of {1:C}.&quot;</span>, <span style='color:maroon'>&quot;book&quot;</span>,
10.0)</span></p>

<p><span style='font-family:"Courier New"'>text =
AdvancedFormatProvider.Format(<span style='color:maroon'>&quot;The {0} has a
value of {1:C}.&quot;</span>, <span style='color:maroon'>&quot;book&quot;</span>,
10.0</span></p>

<p>Result:</p>

<p>&quot;The book has a value of $10.00.&quot;</p>

<h2>Creating your own plug-in</h2>

<p>Create a class that implements the IAdvancedFormatterPlugIn
interface. See the code in the IAdvancedFormatProvider.cs source code file.</p>

<p>To add your class to AdvancedFormatProvider, in application
startup code, call <span style='font-family:"Courier New"'>AdvancedFormatProvider.RegisterFormatPlugIn(<span
style='color:blue'>new</span> YourClass())</span> passing an instance of your
class. Your object will be maintained globally.</p>

<p>Do not attempt to modify it after application startup.</p>

<p>&nbsp;</p>

