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

        private const string HEADER_CAPTURE_REGEX = @"^//(?s:.)+?\n(?=-?\d)";
        private const string DELIMITER_CAPTURE_REGEX = @"(?<=//)(?s:.)+?(?=\n\D?\d)";

        private readonly char[] defaultDelimiters = {',', '\n'};
        private string specifiedDelimiter;

        public int Add(string inputString)
        {
            string numbers = inputString;
            char[] delimiters = defaultDelimiters;

            if (DelimiterSpecified(inputString))
            {
                delimiters = GetSpecifiedDelimiter(inputString);
                numbers = RemoveDelimiterSpecificationLine(inputString);
            }

            CheckForNegatives(numbers, delimiters);

            int sum = 0;
            foreach (string number in numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries))
            {
                sum += ConvertToInt(number) <= 1000 ? ConvertToInt(number) : 0;
            }
            return sum;
        }

        private int ConvertToInt(string number)
        {
            return int.Parse(number);
        }

        private bool DelimiterSpecified(string inputString)
        {
            return Regex.IsMatch(inputString, HEADER_CAPTURE_REGEX);
        }

        private char[] GetSpecifiedDelimiter(string inputString)
        {
            specifiedDelimiter = Regex.Match(inputString, DELIMITER_CAPTURE_REGEX).ToString();
            return specifiedDelimiter.ToCharArray();
        }

        private string RemoveDelimiterSpecificationLine(string inputString)
        {
            int headerLength = Regex.Match(inputString, HEADER_CAPTURE_REGEX).Length;
            return inputString.Substring(headerLength);
        }

        private void CheckForNegatives(string input, char[] delimiters)
        {
            foreach (char delimiter in delimiters)
            {
                if (Regex.IsMatch(input, @"(^|\d\D+?)-\d"))
                {
                    throw new ArgumentException(GetNegatives(input));
                }
            }
        }

        private string GetNegatives(string input)
        {

            string negatives = "";

            if (input[0] == '-')
            {
                negatives += Regex.Match(input, @"^-\d*") + ",";
            }

            var matches = Regex.Matches(input, @"[^\d]-\d");

            
            foreach (var match in matches)
            {
                negatives += match.ToString().Substring(1) + ",";
            }
            return negatives.Substring(0, negatives.Length-1);
        }
    }
}
