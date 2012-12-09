using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_AdvanceFormatProvider
{
   public static class Assert2
   {
      public static void IsException(Func<object> test, Type exceptionClass)
      {
         bool success = true;
         try
         {
            test();
            success = false;
         }
         catch (Exception e)
         {
            if (exceptionClass.IsAssignableFrom(e.GetType()))
               return;
            Assert.Fail("Expected Exception class " + exceptionClass.FullName);
         }
         if (!success)
            Assert.Fail("Exception was not thrown.");
      }

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Result of the function type.</typeparam>
/// <param name="test"></param>
/// <param name="compareResult"></param>
      public static void IsNotException<T>(Func<T> test, T compareResult)
      {
         try
         {
            T result = test();
            Assert.IsTrue(result.Equals(compareResult));
         }
         catch (Exception e)
         {
            Assert.Fail("Exception was thrown. Details: " + e.ToString());

         }
      }

      public static void IsNotException(Func<bool> test)
      {
         try
         {
            test();
         }
         catch (Exception e)
         {
            Assert.Fail("Exception was thrown. Details: " + e.ToString());

         }
      }
   }
}
