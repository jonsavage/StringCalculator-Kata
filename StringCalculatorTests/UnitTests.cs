using System;
using System.Collections.Generic;
using System.Linq;
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
        [TestCase("1", Result = 1)]
        [TestCase("1,1", Result = 2)]
        [TestCase("1,1", Result = 2)]
        [TestCase("1\n2,3" , Result = 6)]
        public int TestEmpty(string s)
        {
            var x = calculator.Add(s);

            return x;
        }

    }
}
