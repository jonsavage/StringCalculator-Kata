using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        private char[] defaultDelimiter = new char[] {','};

        public int Add(string s)
        {
            char[] delimiters;

            if (CustomDelimiterSet(s))
            {
                delimiters = GetCustomDelimiter(s).Concat(defaultDelimiter).ToArray();
            }
            else
            {
                delimiters = defaultDelimiter;
            }




            //char[] delimiters = GetDelimiters(s);

            string a = RemoveDelimiterInfo(s);
            String[] input = RemoveDelimiterInfo(s).Split(delimiters, StringSplitOptions.RemoveEmptyEntries);


            CheckForNegativeNumbers(input);


            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {       
                sum += int.Parse(input[i]);
            }

            return sum;
        }

        public char[] GetCustomDelimiter(string input)
        {
            
            {
                return new char[] {input.Substring(2, input.IndexOf('\n')).ToCharArray()[0], '\n', ','};
            }

            return new char[] {' ', ',', '\n'};
        }

        public string RemoveDelimiterInfo(string input)
        {
            if (input.IndexOf("//") == 0)
            {
                return input.Substring(input.LastIndexOf("\n"));
            }
            return input;
        }

        public static void CheckForNegativeNumbers(IEnumerable<int> numbers)
        {
            if (numbers.Any(number => number < 0))
            {
                throw new ArgumentException("neg");
            }
        }

        public bool CustomDelimiterSet(string input)
        {
            return Regex.IsMatch(input, @"//.{1,2}\n");
        }
        
    }
}
