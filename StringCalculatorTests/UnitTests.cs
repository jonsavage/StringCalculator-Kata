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
        [TestCase("", Result = 0)]
        public int Test_Empty_String(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("", Result = 0)]
        [TestCase("0", Result = 0)]
        [TestCase("1", Result = 1)]
        public int Test_Add_One_Number(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("0,0", Result = 0)]
        [TestCase("0,1", Result = 1)]
        [TestCase("1,2", Result = 3)]
        public int Test_AddTwoNumbers(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("0,0,0", Result = 0)]
        [TestCase("0,1,2", Result = 3)]
        [TestCase("1,2,3,4", Result = 10)]
        [TestCase("1,2,3,4,5,6,7,8,9", Result = 45)]
        public int Test_AddUnknownNumberOfIntegers(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("0\n0\n0", Result = 0)]
        [TestCase("0,1\n2", Result = 3)]
        [TestCase("1,2,3\n4", Result = 10)]
        public int Test_UsingNewLineAsADelimiter(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("//;\n1;2", Result = 3)]
        [TestCase("//.\n2.3", Result = 5)]
        [TestCase("//\t\n1\t2", Result = 3)]
        [TestCase("//\n\n1\n2", Result = 3)]
        [TestCase("//\n\n1\n2\n3", Result = 6)]
        [TestCase("//\\\n1\\2", Result = 3)]
        [TestCase("//\v\n1\v2", Result = 3)]
        [TestCase("//-\n1-2", Result = 3)]
        [TestCase("//-\n1-2-3-4-5-6-70-80-90", Result = 261)]
        public int Test_UserSpecifiedDelimiter(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("-1", "-1")]
        [TestCase("-1,-2", "-1,-2")]
        [TestCase("-1,1", "-1")]
        [TestCase("//-\n1--1", "-1")]
        [TestCase("//-\n1--1-2", "-1")]
        [TestCase("//\n\n1\n-1", "-1")]
        [TestCase("//\n\n-1\n2", "-1")]
        [TestCase("//;\n1\n-1;2", "-1")]
        [TestCase("//\n\n1\n-1\n-2\n3", "-1,-2")]
        [TestCase("//;*...\n-1", "-1")]
        [TestCase("//;*...\n-11", "-11")]
        [TestCase("//;*...\n-11,-3333", "-11,-3333")]
        [TestCase("//;*...\n-11.-3333.-2;6", "-11,-3333,-2")]
        public void Test_NegativesThrowArguementExceptionWithMessage(string input, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => calculator.Add(input));

            Assert.AreEqual("negatives not allowed " + expectedMessage, exception.Message);
        }


        [Test]
        [TestCase("1,1000", Result = 1001)]
        [TestCase("1,1001", Result = 1)]
        [TestCase("1001,1", Result = 1)]
        public int Test_IngoreNumbersGreaterThanOneThousand(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("//..\n1..2", Result = 3)]
        [TestCase("//;;;;;\n1;;;;;2;;;;;3", Result = 6)]
        [TestCase("//\n\n\n1\n\n2", Result = 3)]
        [TestCase("//\t\t\t\n2\t\t\t\n3", Result = 5)]
        public int Test_UserSpecifiedDelimiterOfAnyLength(string input)
        {
            var x = calculator.Add(input);

            return x;
        }

        [Test]
        [TestCase("//.;\n1.2", Result = 3)]
        [TestCase("//;;\n1;2;3", Result = 6)]
        [TestCase("//\t\n\n2\t3\n", Result = 5)]
        [TestCase("///\t\n\n1/2\t3", Result = 6)]
        [TestCase("///.-\n1.2-3", Result = 6)]
        public int Test_UserSpecifiedMultipleDelimiters(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("//..::\n1..2::3", Result = 6)]
        [TestCase("//abcd\n1ab2cd3", Result = 6)]
        [TestCase("//;.:.\n1;.2:.3", Result = 6)]
        [TestCase("//\t\n\v\b\n1\t\n2\v\b3", Result = 6)]
        [TestCase("//\t\n\v\v\b\n1\t\n\v2\v\b3", Result = 6)]
        [TestCase("//\t::\v;\n1\t::2\v;3", Result = 6)]
        public int Test_UserSpecifiedMultipleDelimitersLongerThanOneChar(string input)
        {
            var x = calculator.Add(input);

            return x;
        }


        [Test]
        [TestCase("//.\n1.-2", "-2")]
        [TestCase("//-;\n1--2;3", "-2")]
        [TestCase("//-;\n1-;-2;3", "-2")]
        [TestCase("//abcdefgh-;ij\n-1fgh--2;3", "-1,-2")]
        public void Test_NegativesThrowArguementExceptionWithMessageWithMultipleCustomDelimiters(string input, string expectedMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => calculator.Add(input));

            Assert.AreEqual("negatives not allowed " + expectedMessage, exception.Message);
        }
    }
}
