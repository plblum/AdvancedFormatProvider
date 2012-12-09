using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Test_AdvanceFormatProvider
{
    
    
    /// <summary>
    ///This is a test class for AdvancedFormatProviderTest and is intended
    ///to contain all AdvancedFormatProviderTest Unit Tests
    ///</summary>
   [TestClass()]
   public class AdvancedFormatProviderTest
   {


      /// <summary>
      ///A test for Format
      ///</summary>
      [TestMethod()]
      public void FormatTest()
      {
         CultureInfo altCulture = CultureInfo.CreateSpecificCulture("en-US");
         altCulture.NumberFormat.CurrencySymbol = "!";
         altCulture.NumberFormat.CurrencyDecimalDigits = 3;
         altCulture.NumberFormat.CurrencyNegativePattern = 15; // = "(n $)";
         string actual = AdvancedFormatProvider.Format("{0:N0}", 2); // standard formatter
         Assert.AreEqual("2", actual);
         actual = AdvancedFormatProvider.Format("{0:C}", 2.0); // standard formatter
         Assert.AreEqual("$2.00", actual);
         actual = AdvancedFormatProvider.Format("{0:P}", 2.0); // standard formatter
         Assert.AreEqual("200.00 %", actual);
         actual = AdvancedFormatProvider.Format("{0:P0}", 2.0); // standard formatter
         Assert.AreEqual("200 %", actual);


         actual = AdvancedFormatProvider.Format("{0:I}", 20000); // custom formatter
         Assert.AreEqual("20,000", actual);

         actual = AdvancedFormatProvider.Format("{0:i-,}", 20000); // custom formatter
         Assert.AreEqual("20000", actual);

         actual = AdvancedFormatProvider.Format("{0:i3}", 2); // custom formatter
         Assert.AreEqual("002", actual);

         actual = AdvancedFormatProvider.Format("{0:C-$-,}", 2.0); // standard formatter
         Assert.AreEqual("2.00", actual);
         actual = AdvancedFormatProvider.Format("{0:P-%-,}", 2.0); // standard formatter
         Assert.AreEqual("200.00", actual);
         actual = AdvancedFormatProvider.Format("{0:c-$}", 20000.0); // standard formatter
         Assert.AreEqual("20,000.00", actual);
         actual = AdvancedFormatProvider.Format("{0:c-$0}", 20000.0); // standard formatter
         Assert.AreEqual("20,000", actual);
         actual = AdvancedFormatProvider.Format("{0:p-%}", 200.0); // standard formatter
         Assert.AreEqual("20,000.00", actual);
         actual = AdvancedFormatProvider.Format("{0:p-%0}", 200.0); // standard formatter
         Assert.AreEqual("20,000", actual);
      }
   }
}
