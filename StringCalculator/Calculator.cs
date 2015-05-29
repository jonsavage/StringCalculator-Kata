using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string s)
        {
            if (s == "")
                return 0;

            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n' };

            int sum = 0;
            String[] input = s.Split(delimiterChars);
            

            for (int i = 0; i < input.Length; i++)
            {
                sum += Int32.Parse(input[i]);
            }

            return sum;
        }
    }
}
