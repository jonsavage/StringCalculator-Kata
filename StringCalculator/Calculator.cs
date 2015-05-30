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
        private char[] additionalDelimiter = 

        public int Add(string inputString)
        {
            char[] delimiters;
            
            if (CustomDelimiterSpecified(inputString))
            {
                delimiters = GetCustomDelimiter(inputString).Concat(defaultDelimiter).ToArray();
            }
            else
            {
                delimiters = defaultDelimiter;
            }

            string a = RemoveDelimiterInfo(inputString);
            string[] numbers = RemoveDelimiterInfo(inputString).Split(delimiters, StringSplitOptions.RemoveEmptyEntries);


            CheckForNegativeNumbers(numbers);


            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {       
                sum += int.Parse(numbers[i]);
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

        public bool CustomDelimiterSpecified(string input)
        {
            return Regex.IsMatch(input, @"//.{1,2}\n");
        }
        
    }
}
