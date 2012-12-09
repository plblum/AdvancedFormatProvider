using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Test_AdvanceFormatProvider
{
    
    
    /// <summary>
    ///This is a test class for NoSymbolFormatterTest and is intended
    ///to contain all NoSymbolFormatterTest Unit Tests
    ///</summary>
   [TestClass()]
   public class NoSymbolFormatterTest
   {

      /// <summary>
      ///A test for Format
      ///</summary>
      [TestMethod()]
      public void FormatTestCurrency()
      {
         CultureInfo altCulture = CultureInfo.CreateSpecificCulture("en-US");
         altCulture.NumberFormat.CurrencySymbol = "!";
         altCulture.NumberFormat.CurrencyDecimalDigits = 3;
         altCulture.NumberFormat.CurrencyGroupSeparator = "#";
         altCulture.NumberFormat.CurrencyNegativePattern = 15; // = "(n $)";
         CurrencyAndPercentFormatter target = new CurrencyAndPercentFormatter();

         Assert2.IsException(() => target.Format(null, 1, null), typeof(ArgumentException));

         string result = target.Format("C", 1.0, null);
         Assert.AreEqual("$1.00", result);
         result = target.Format("c", 100.0, null);
         Assert.AreEqual("$100.00", result);
         result = target.Format("C", 1000000.0, null);
         Assert.AreEqual("$1,000,000.00", result);
         result = target.Format("C$", 1000000.0, null);
         Assert.AreEqual("$1,000,000.00", result);

         result = target.Format("C-$", 1.0, null);
         Assert.AreEqual("1.00", result);
         result = target.Format("c-$", 100.0, null);
         Assert.AreEqual("100.00", result);
         result = target.Format("C-$", 1000000.0, null);
         Assert.AreEqual("1,000,000.00", result);
         result = target.Format("C-$-,", 1000000.0, null);
         Assert.AreEqual("1000000.00", result);
         result = target.Format("C-,-$", 1000000.0, null);
         Assert.AreEqual("1000000.00", result);
         result = target.Format("C,$", 1000000.0, null);
         Assert.AreEqual("$1,000,000.00", result);
         result = target.Format("C-,$", 1000000.0, null);
         Assert.AreEqual("$1000000.00", result);
         result = target.Format("C,-$", 1000000.0, null);
         Assert.AreEqual("1,000,000.00", result);

         result = target.Format("C", -1000000.0, null);
         Assert.AreEqual("($1,000,000.00)", result);

         result = target.Format("C-$", -1000000.0, null);
         Assert.AreEqual("(1,000,000.00)", result);
         result = target.Format("C-$-,", -1000000.0, null);
         Assert.AreEqual("(1000000.00)", result);
         result = target.Format("C-,-$", -1000000.0, null);
         Assert.AreEqual("(1000000.00)", result);
         result = target.Format("C-$-$-$-$", -1000000.0, null);  // show this is harmless
         Assert.AreEqual("(1,000,000.00)", result);

         result = target.Format("C", -1000000.0, altCulture);
         Assert.AreEqual("(1#000#000.000 !)", result);
         result = target.Format("C-$", -1000000.0, altCulture);
         Assert.AreEqual("(1#000#000.000)", result);
         result = target.Format("C-$-,", -1000000.0, altCulture);
         Assert.AreEqual("(1000000.000)", result);

         for (int i = 1; i <= 15; i++)
         {
            altCulture.NumberFormat.CurrencyNegativePattern = i; 
            result = target.Format("C", -1000000.0, altCulture);
            Assert.IsTrue(result.Contains("!"));  // culture symbol has already been switched to !
            result = target.Format("C-$-,", -1000000.0, altCulture);
            Assert.IsFalse(result.Contains("!"));  // culture symbol has already been switched to !
         }

         // integers showing they strip .00 unless the precision is specified
         result = target.Format("C", 1000000, null);
         Assert.AreEqual("$1,000,000", result);

         result = target.Format("C-$", 1000000, null);
         Assert.AreEqual("1,000,000", result);

         result = target.Format("C3", 1000000, null);
         Assert.AreEqual("$1,000,000.000", result);

         result = target.Format("C-$3", 1000000, null);
         Assert.AreEqual("1,000,000.000", result);

      // strip decimals when zero
         result = target.Format("C-0", 1.0, null);
         Assert.AreEqual("$1", result);
         result = target.Format("C-0", 1.1, null);
         Assert.AreEqual("$1.10", result);
         result = target.Format("C-0-$", 1.0, null);
         Assert.AreEqual("1", result);
         result = target.Format("C-$-0", 1.1, null);
         Assert.AreEqual("1.10", result);
         result = target.Format("C-0-$", 1000000.0, null);
         Assert.AreEqual("1,000,000", result);
         result = target.Format("C-$-0", 1000000.1, null);
         Assert.AreEqual("1,000,000.10", result);
         result = target.Format("C-,-0-$", 1000000.0, null);
         Assert.AreEqual("1000000", result);
         result = target.Format("C-$-0-,", 1000000.1, null);
         Assert.AreEqual("1000000.10", result);

         Assert2.IsException(() => target.Format("x", 1000, null), typeof(ArgumentException));
         Assert2.IsException(() => target.Format("C-x", 1000, null), typeof(ArgumentException));
         Assert2.IsException(() => target.Format("Cc", 1000, null), typeof(ArgumentException));

      }

      /// <summary>
      ///A test for Format
      ///</summary>
      [TestMethod()]
      public void FormatTestPercent()
      {
         CultureInfo altCulture = CultureInfo.CreateSpecificCulture("en-US");
         altCulture.NumberFormat.PercentSymbol = "!";
         altCulture.NumberFormat.PercentDecimalDigits = 3;
         altCulture.NumberFormat.PercentNegativePattern = 11; // = "n- %";
         altCulture.NumberFormat.PercentGroupSeparator = "#";
         CurrencyAndPercentFormatter target = new CurrencyAndPercentFormatter();

         Assert2.IsException(() => target.Format(null, 1, null), typeof(ArgumentException));

         string result = target.Format("P", 1.0, null);
         Assert.AreEqual("100.00 %", result);
         result = target.Format("P", 0.50, null);
         Assert.AreEqual("50.00 %", result);
         result = target.Format("p", 100.0, null);
         Assert.AreEqual("10,000.00 %", result);
         result = target.Format("P", 10000.0, null);
         Assert.AreEqual("1,000,000.00 %", result);
         result = target.Format("p%", 10000.0, null);
         Assert.AreEqual("1,000,000.00 %", result);

         result = target.Format("p-%", 1.0, null);
         Assert.AreEqual("100.00", result);
         result = target.Format("p-%", 100.0, null);
         Assert.AreEqual("10,000.00", result);
         result = target.Format("P-%", 10000.0, null);
         Assert.AreEqual("1,000,000.00", result);
         result = target.Format("P-%-,", 10000.0, null);
         Assert.AreEqual("1000000.00", result);
         result = target.Format("P-,-%", 10000.0, null);
         Assert.AreEqual("1000000.00", result);
         result = target.Format("P,%", 10000.0, null);
         Assert.AreEqual("1,000,000.00 %", result);
         result = target.Format("P-,%", 10000.0, null);
         Assert.AreEqual("1000000.00 %", result);
         result = target.Format("P,-%", 10000.0, null);
         Assert.AreEqual("1,000,000.00", result);

         result = target.Format("P", -10000.0, null);
         Assert.AreEqual("-1,000,000.00 %", result);

         result = target.Format("P-%", -10000.0, null);
         Assert.AreEqual("-1,000,000.00", result);
         result = target.Format("p-%-,", -10000.0, null);
         Assert.AreEqual("-1000000.00", result);
         result = target.Format("P-,-%", -10000.0, null);
         Assert.AreEqual("-1000000.00", result);
         result = target.Format("p-%-%-%-%", -10000.0, null);  // show this is harmless
         Assert.AreEqual("-1,000,000.00", result);

         result = target.Format("P", -10000.0, altCulture);
         Assert.AreEqual("1#000#000.000- !", result);
         result = target.Format("P-%", -10000.0, altCulture);
         Assert.AreEqual("1#000#000.000-", result);
         result = target.Format("P-%-,", -10000.0, altCulture);
         Assert.AreEqual("1000000.000-", result);

         for (int i = 1; i <= 11; i++)
         {
            altCulture.NumberFormat.CurrencyNegativePattern = i; 
            result = target.Format("p", -10000.0, altCulture);
            Assert.IsTrue(result.Contains("!"));  // culture symbol has already been switched to !
            result = target.Format("P-%-,", -10000.0, altCulture);
            Assert.IsFalse(result.Contains("!"));  // culture symbol has already been switched to !
         }

         // integers showing they strip .00 unless the precision is specified
         result = target.Format("P", 10000, null);
         Assert.AreEqual("1,000,000 %", result);

         result = target.Format("P-%", 10000, null);
         Assert.AreEqual("1,000,000", result);

         result = target.Format("P3", 10000, null);
         Assert.AreEqual("1,000,000.000 %", result);

         result = target.Format("P-%3", 10000, null);
         Assert.AreEqual("1,000,000.000", result);

      // strip decimals when zero
         result = target.Format("P-0", 1.0, null);
         Assert.AreEqual("100 %", result);
         result = target.Format("P-0", 1.001, null);
         Assert.AreEqual("100.10 %", result);
         result = target.Format("P-0-%", 1.0, null);
         Assert.AreEqual("100", result);
         result = target.Format("P-%-0", 1.001, null);
         Assert.AreEqual("100.10", result);
         result = target.Format("P-0-%", 10000.0, null);
         Assert.AreEqual("1,000,000", result);
         result = target.Format("P-%-0", 10000.001, null);
         Assert.AreEqual("1,000,000.10", result);
         result = target.Format("p-,-0-%", 10000.0, null);
         Assert.AreEqual("1000000", result);
         result = target.Format("p-%-0-,", 10000.001, null);
         Assert.AreEqual("1000000.10", result);

         Assert2.IsException(() => target.Format("P-x", 1000, null), typeof(ArgumentException));
         Assert2.IsException(() => target.Format("pp", 1000, null), typeof(ArgumentException));


      }
   }
}
