using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Test_AdvanceFormatProvider
{
    
    
    /// <summary>
    ///This is a test class for IntegerFormatterTest and is intended
    ///to contain all IntegerFormatterTest Unit Tests
    ///</summary>
   [TestClass()]
   public class IntegerFormatterTest
   {

      /// <summary>
      ///A test for Format
      ///</summary>
      [TestMethod()]
      public void FormatTest()
      {
         CultureInfo altCulture = CultureInfo.CreateSpecificCulture("en-US");
         altCulture.NumberFormat.NegativeSign = "!";
         altCulture.NumberFormat.NumberGroupSeparator = "#";
         IntegerFormatter target = new IntegerFormatter();

         Assert2.IsException(() => target.Format(null, 1, null), typeof(ArgumentException));

         string result = target.Format("i", 1, null);
         Assert.AreEqual("1", result);

         result = target.Format("I", 1, null);
         Assert.AreEqual("1", result);

         result = target.Format("i", 1000000, null);
         Assert.AreEqual("1,000,000", result);

         result = target.Format("i,", 1000000, null);
         Assert.AreEqual("1,000,000", result);

         result = target.Format("i-,", 1000000, null);
         Assert.AreEqual("1000000", result);

         result = target.Format("I", 1000000, null);
         Assert.AreEqual("1,000,000", result);

         result = target.Format("I,", 1000000, null);
         Assert.AreEqual("1,000,000", result);

         result = target.Format("I-,", 1000000, null);
         Assert.AreEqual("1000000", result);

         result = target.Format("i", -1000000, null);
         Assert.AreEqual("-1,000,000", result);

         result = target.Format("i-,", -1000000, null);
         Assert.AreEqual("-1000000", result);

         result = target.Format("I", 1, null);
         Assert.AreEqual("1", result);

         result = target.Format("I", 1000000, null);
         Assert.AreEqual("1,000,000", result);

         result = target.Format("I", -1000000, null);
         Assert.AreEqual("-1,000,000", result);

         result = target.Format("I", -1000000, altCulture);
         Assert.AreEqual("!1#000#000", result);

         result = target.Format("i,", -1000000, altCulture);
         Assert.AreEqual("!1#000#000", result);

         result = target.Format("i-,", -1000000, altCulture);
         Assert.AreEqual("!1000000", result);

         result = target.Format("I3", 1, null);
         Assert.AreEqual("001", result);

         result = target.Format("I1", 1, null);
         Assert.AreEqual("1", result);

         result = target.Format("I2", 1, null);
         Assert.AreEqual("01", result);

         result = target.Format("I3", 100, null);
         Assert.AreEqual("100", result);

         result = target.Format("i3", 1000, null);
         Assert.AreEqual("1,000", result);

         result = target.Format("I10", 1234, null);
         Assert.AreEqual("000001,234", result);


         result = target.Format("I", 100.50, null);
         Assert.AreEqual("100", result);

         result = target.Format("I", 1000.5, null);
         Assert.AreEqual("1,000", result);

         result = target.Format("I", 1000.6, null);   // conversion from decimal using Convert.ToInt64 performs rounding.
         Assert.AreEqual("1,001", result);
      }
   }
}
