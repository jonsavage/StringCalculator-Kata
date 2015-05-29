using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StringCalculator;

namespace StringCalculatorTests
{
   
    public class UnitTests
    {

        Calculator calculator = new Calculator();


        [Test]
        [TestCase("", Result = 0)]                // Step 1
        [TestCase("1", Result = 1)]
        [TestCase("1,1", Result = 2)]
        [TestCase("1,2", Result = 3)]
        [TestCase("1,1,1,1,1,1,1,1", Result = 8)] // Step 2
        [TestCase("1\n2,3", Result = 6)]          // Step 3
        [TestCase("1\n2,3,4,5", Result = 15)]                    
        [TestCase("//;\n1;2", Result = 3)]         // Step 4
        [TestCase("//1\n213", Result = 5)]
        [TestCase("//\t\n2\t3", Result = 5)]
        [TestCase("//\t\n2,3\n5", Result = 5)]
        public int TestEmpty(string s)
        {
            var x = calculator.Add(s);

            return x;
        }

        [Test]
        [TestCase("-1", "-1")]
        [TestCase("-1,-2", "-1,-2")]
        [TestCase("-1,1,-1", "-1,-1,-1")]
        public void TestNegatives(string s, string expectedMessage)
        {
            Assert.Throws<ArgumentException>(
                () => calculator.Add(s)
            );

            var exception = Assert.Throws<ArgumentException>(() => calculator.Add(s));

            Assert.AreEqual(expectedMessage, exception.Message);

        }
    }
}
