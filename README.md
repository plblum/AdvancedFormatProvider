<h1>AdvancedFormatProvider class and plug-ins</h1>

<p>For platform: Microsoft .net 1.1 and higher.</p>

<p>
   AdvancedFormatProvider is an <a
href="http://msdn.microsoft.com/en-us/library/system.iformatprovider%28v=vs.100%29.aspx">IFormatProvider</a>
   which means it works with <a
href="http://msdn.microsoft.com/en-us/library/system.string.format%28v=vs.100%29.aspx">String.Format()</a>
   and other consumers of this interface. For example:
</p>

<p>
   <code>
      text =
      String.Format(AdvancedFormatProvider.Current, &quot;{0:C-$}&quot;, 100.0)
   </code>
</p>

<p>
   It supports all existing formats supplied by the .net
   framework, plus those included with this product and you create.
</p>

<p>
   AdvancedFormatProvider makes it easy to drop in new
   formatters, tools that convert a value into a string based on a format string
   in the <code>{#:here}</code> token. (For
   example, <code>{0:c}</code> and <code>{1:yyyy-MM-dd}</code>.)
</p>

<p>
   The formatters are registered globally so you only install
   them once, in Application_Start.
</p>

<p>It pre-installs support for these formatters:</p>

<ul  type='disc'>
   <li>
      <b>IntegerFormatter</b> - For use with integers, or to
      format decimals like integers. Uses the symbol &quot;i&quot; or
      &quot;I&quot; such as <code>{0:I}</code>
      and <code>{0:i}</code>. It is similar to
      the <code>
         <a
     href="http://msdn.microsoft.com/en-us/library/dwhawy9k.aspx#CFormatString">
            N0
         </a>
      </code> formatter built
      into .net, except it has an option to omit group separators.

      <p style='margin-left:.5in'>
         The symbol can be followed with
         these modifiers:
      </p>

      <ul type='circle'>
         <li>
            <code>{0:I-,}</code> -
            Omit the group separator character. When not present, the group separator
            is used.
         </li>
         <li>
            <code>{0:I,}</code> -
            Include the group separator. Not necessary because when there is no
            comma, it includes the group separator.
         </li>
         <li>
            <code>{0:I#}</code> - The
            number of digits to show where # is shown. If the value is less than
            this, lead zeros are used to fill.
         </li>
      </ul>

      <p style='margin-left:.5in'>
         <i>Examples: </i>
      </p>
      <ul type='circle'>
         <li>
            
            <code>{0:I}</code>,  <code>{0:I,}</code>      10000 -&gt;
            &quot;10,000&quot;       (includes group separators)
         </li>

         <li >
            
            <code>{0:I3}</code>, <code>{0:I,3} </code> 1 -&gt;
            &quot;001&quot;                  (includes group separators and fills to 3
            digits)
         </li>

         <li>
            
            <code>{0:I-,}</code>            
                  10000 -&gt; &quot;10000&quot;        (omits group separators)
         </li>

         <li>
            
            <code>{0:I-,7}</code>           
                11234 -&gt; &quot;0011234&quot;    (omits group separators and fills to 8
            digits)
         </li>
      </ul>
   </li>
   <li>
      <b>CurrencyAndPercentFormatter</b> - For use with numbers
      to format them either as a currency or percent. This overrides the native
      formatters, using the same format strings (&quot;c&quot;, &quot;C&quot;,
      &quot;p&quot;, &quot;P&quot;), adding rules to omit the group separator,
      currency or percent symbol, or decimal digits when all zero.<br />
      <p style='margin-left:.5in'>
         The initial character can be
         followed with these modifiers:
      </p>
      <ul type='circle'>
         <li>
            
            <code>{0:C-$}</code> - Omit the
            currency symbol. When not present, the currency symbol is used.
         </li>

         <li>
            
            <code>{0:C$}</code> - Include
            the currency symbol. Not necessary because when there is no $, it means include
            the currency symbol
         </li>

         <li>
            
            <code>{0:P-%}</code> - Omit the
            percent symbol. When not present, the percent symbol is used.
         </li>

         <li>
            
            <code>{0:P%}</code> - Include
            the percent symbol. Not necessary because when there is no %, it means include
            the percent symbol
         </li>

         <li>
            
            <code>{0:C-,}</code> or <code>{0:P-,}</code> - Omit the group separator character. When not present, the group separator is used.
         </li>

         <li>
            
            <code>{0:C,}</code> or <code>{0:P,}</code> - Include the group separator.
            Not necessary because when there is no comma, it includes the group separator.
         </li>

         <li>
            
            <code>{0:C-0}</code> or <code>{0:P-0}</code> - Omit the floating point part
            when its zero (the value would be the same as an integer).
         </li>

         <li>
            
            <code>{0:C#}</code> or <code>{0:P#}</code> - The number of floating point
            digits to show where # is shown. If not specified, it uses the culture's rule.
            Use 0 to remove the floating point part. However, if not specified and the
            value is already an integer, the floating point part is always removed.
         </li>
      </ul>
      <p style='margin-left:.5in'>
         <i>Examples for currency:</i>
      </p>
      <ul type='circle'>
         <li>
            
            <code>{0:C}</code>, <code>{0:C,}</code>       5.0 -&gt;
            &quot;$5.00&quot;                 (includes currency symbol and group
            separators)<br />
            <p style='margin-left:2.0in;text-indent:.5in'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               5000.0 -&gt;
               &quot;$5,000.00&quot;
            </p>
            <p style='margin-left:4.15in;text-indent:-3.4in'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               50  
               -&gt; &quot;$50&quot;                   (because it is an integer passed in)
            </p>
         </li>

         <li>
            
            <code>{0:C-$}</code>                   5.0 
            -&gt; &quot;5.00&quot;                  (omits currency symbol; includes group
            separators)<br />
            <p style='margin-left:2.0in;text-indent:.5in'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               5000.0 -&gt;
               &quot;5,000.00&quot;
            </p>
         </li>

         <li>
            
            <code>{0:C-$-,}</code>              5000.0
            -&gt; &quot;5000.00&quot;       (omits currency symbol and group separators)
         </li>

         <li>
            
            <code>{0:C3}</code>, <code>{0:C,3}</code>  5.00 -&gt; &quot;$5.000&quot;             (includes
            group separators and requires 3 decimal digits)
         </li>

         <li>
            
            <code>{0:C0}</code>, <code>{0:C,0}</code>  50.0 -&gt; &quot;$50&quot;                  (includes
            group separators and removes decimal digits),
         </li>

         <li>
            
            <code>{0:C-,}</code>                   5000.0
            -&gt; &quot;$5000.00&quot;     (omits group separators)
         </li>

         <li>
            
            <code>{0:C-,3}</code>                5000.0
            -&gt; &quot;$5000.000&quot;   (omits group separators and requires 3 decimal
            digits)
         </li>

         <li>
            
            <code>{0:C-,0}</code>                5000.0
            -&gt; &quot;$5000&quot;          (omits group separators and removes decimal
            digits)
         </li>

         <li>
            
            <code>{0:C-0}</code>                   5000.00
            -&gt; &quot;$5,000&quot;       (removes decimal part when zero; includes
            currency symbol and group separators)<br />
            <p style='margin-left:2.0in;text-indent:.5in'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               5000.01 -&gt;
               &quot;$5,000.01&quot;
            </p>

         </li>

         <li>
            
            <code>{0:C-$-,-0}</code>         5000.0
            -&gt; &quot;5000&quot;            (removes decimal part when zero; omits
            currency symbol and group separators)<br />
            <p style='margin-left:2.0in;text-indent:.5in'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               5000.01 -&gt;
               &quot;5000.01&quot;
            </p>

         </li>
      </ul>

      <p style='margin-left:.5in'>
         <i>Examples for percent:</i>
      </p>
      <ul type='circle'>
         <li>
            
            <code>{0:P}</code>, <code>{0:P,}</code>       0.50 -&gt; &quot;50.00
            %&quot;           (includes percent symbol and group separators)<br/>
            <p style='margin-left:2.0in;text-indent:.5in'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               50.0 -&gt;
               &quot;5,000.00 %&quot;
            </p>

            <p style='margin-left:2.0in;text-indent:.5in'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               50   -&gt;
               &quot;5,000 %&quot;           (because it is an integer passed in)
            </p>
         </li>


         <li>
            
            <code>{0:P-%}</code>                   0.50
            -&gt; &quot;50.00&quot;               (omits percent symbol; includes group separators)<br />
            <p style='margin-left:2.0in;text-indent:.5in'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               50.0 -&gt;
               &quot;5,000.00&quot;  
            </p>

         </li>


         <li>
            
            <code>{0:P-%-,}</code>              50.0
            -&gt; &quot;5000.00&quot;           (omits percent symbol and group separators)

         </li>

         <li>
            
            <code>{0:P3}</code>, <code>{0:P,3}</code>  0.50 -&gt; &quot;50.000
            %&quot;         (includes group separators and requires 3 decimal digits)
         </li>

         <li>
            
            <code>{0:P0}</code>, <code>{0:P,0}</code>  50.0 -&gt; &quot;$5,000&quot;             (includes
            group separators and removes decimal digits)
         </li>

         <li>
            
            <code>{0:P-,}</code>                50.0
            -&gt; &quot;5000.0 %&quot;         (omits group separators)
         </li>

         <li>
            
            <code>{0:P-,3}</code>                50.0
            -&gt; &quot;5000.000 %&quot;     (omits group separators and requires 3 decimal
            digits),
         </li>

         <li>
            
            <code>{0:P-,0}</code>                50.0
            -&gt; &quot;5000 %&quot;            (omits group separators and removes decimal
            digits)
         </li>

         <li>
            
            <code>{0:P-0}</code>                   0.50
            -&gt; &quot;50 %&quot;                (removes decimal part when zero; includes
            percent symbol and group separators)<br />
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               0.501
               -&gt; &quot;50.1 %&quot;
            </p>

         </li>


         <li>
            
            <code>{0:P-%-,-0}</code>         0.50
            -&gt; &quot;50&quot;                    (removes decimal part when zero; omits percent symbol and group separators)<br />
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               0.501
               -&gt; &quot;50.1&quot;
            </p>

         </li>
      </ul>

   </li>
</ul>

<p>
   Add more by creating a class that implements the
   IAdvancedFormatterPlugIn interface.
</p>

<h2>Getting started</h2>

<p>
   Either add the Visual Studio project into your application
   or add the source code files.
</p>

<h3>Using the Visual Studio project file</h3>

<p>
   After adding the project, add a reference from your
   application to the project.
</p>

<h3>Using the source code</h3>

<p>
   The source code files are in C#. If your application is not
   in C#, use the Visual Studio project file.
</p>

<p>
   Add the source code files to your application. The source
   code files do not specify a namespace. Consider adding your own application’s
   namespace to them.
</p>

<h2>Using AdvancedFormatProvider</h2>

<p>
   You can either call <a
href="http://msdn.microsoft.com/en-us/library/system.string.format%28v=vs.100%29.aspx">String.Format()</a>
   passing in the AdvancedFormatProvider as the first parameter, or call
   AdvancedFormatProvider.Format(), with similar parameters to String.Format.
   AdvancedFormatProvider.Format() lets you avoid creating an instance of
   AdvancedFormatProvider and makes it easy to pass in a specific <a
href="http://msdn.microsoft.com/en-US/library/system.globalization.cultureinfo%28v=vs.100%29.aspx">System.Globalization.CultureInfo</a>
   object.
</p>

<h3>Do not need to pass a CultureInfo object</h3>

<p>
   <code>
      text =
      String.Format(AdvancedFormatProvider.Current,
      &quot;<i>
         text
         with tokens
      </i>&quot;, value1, value2, <i>etc.</i>)
   </code>
</p>

<p>
   <code>
      text =
      AdvancedFormatProvider.Format(&quot;text with tokens&quot;,
      value1, value2, <i>etc.</i>)
   </code>
</p>

<p>
   <i>Examples</i>
</p>

<p>
   <code>
      text =
      String.Format(AdvancedFormatProvider.Current, <span style='color:maroon'>
         &quot;The
         {0} has a value of {1:C}.&quot;, &quot;book&quot;,
      10.0)
   </code>
</p>

<p>
   <code>
      text =
      AdvancedFormatProvider.Format
      (&quot;The {0} has a
      value of {1:C}.&quot;
      , &quot;book&quot;,
      10.0
   </code>
</p>

<p>Result:</p>

<p>&quot;The book has a value of $10.00.&quot;</p>

<h3>Need to pass a CultureInfo object</h3>

<p>
   <code>
      text =
      String.Format(<span style='color:blue'>new</span> AdvancedFormatProvider(cultureInfo),
      &quot;<i>text with tokens</i>&quot;, value1,
      value2, <i>etc.</i>)
   </code>
</p>

<p>
   <code>
      text =
      AdvancedFormatProvider.Format(cultureInfo,
      &quot;text
      with tokens&quot;
      , value1, value2, <i>etc.</i>)
   </code>
</p>

<p>
   <i>Examples</i>
</p>

<p>
   <code>
      text =
      String.Format(AdvancedFormatProvider.Current,
      &quot;The
      {0} has a value of {1:C}.&quot;
      , &quot;book&quot;,
      10.0)
   </code>
</p>

<p>
   <code>
      text =
      AdvancedFormatProvider.Format(
      &quot;The {0} has a
      value of {1:C}.&quot;
      , &quot;book&quot;,
      10.0
   </code>
</p>

<p>Result:</p>

<p>&quot;The book has a value of $10.00.&quot;</p>

<h2>Creating your own plug-in</h2>

<p>
   Create a class that implements the IAdvancedFormatterPlugIn
   interface. See the code in the IAdvancedFormatProvider.cs source code file.
</p>

<p>
   To add your class to AdvancedFormatProvider, in application
   startup code, call <code>
      AdvancedFormatProvider.RegisterFormatPlugIn(<span
style='color:blue'>new</span> YourClass())
      </code> passing an instance of your
      class. Your object will be maintained globally.
   </p>

<p>Do not attempt to modify it after application startup.</p>

