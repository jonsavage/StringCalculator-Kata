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

        private string headerCaptureGroup = @"^//(?s:.)+?(?=\n\d)";
        private string extractDelimiterCaptureGroup = @"(?<=//)(?s:.)+?(?=\n\d)";
        private string delimiterCaptureGroup = @"(?<=^//)(?s:.)+?(?=\n\d)";

        private char[] defaultDelimiters = {',', '\n'};
        private string specifiedDelimiter;

        public int Add(string inputString)
        {
            string numbers = inputString;
            char[] delimiters = new char[] {',', '\n'};

            if (DelimiterSpecified(inputString))
            {
                var a = Regex.Match(inputString, delimiterCaptureGroup);


                //var specifiedDelimiter = GetSpecifiedDelimiter(inputString);
                //numbers = NormalizeDelimiter(inputString, specifiedDelimiter);

                delimiters = GetSpecifiedDelimiter(inputString);
                numbers = RemoveDelimiterSpecificationLine(inputString);
            }

            CheckForNegatives(numbers);

            int sum = 0;
            foreach (string number in numbers.Split(new string[] {specifiedDelimiter}, StringSplitOptions.RemoveEmptyEntries))
            //foreach (string number in numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries))
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
            return Regex.IsMatch(inputString, headerCaptureGroup);
        }

        private char[] GetSpecifiedDelimiter(string inputString)
        {
            specifiedDelimiter = Regex.Match(inputString, extractDelimiterCaptureGroup).ToString(); 

            return new char[] { inputString.Substring(2, inputString.LastIndexOf('\n')).ToCharArray()[0]};
        }

        private string RemoveDelimiterSpecificationLine(string inputString)
        {
            int headerLength = Regex.Match(inputString, headerCaptureGroup).Length;
            return inputString.Substring(headerLength);
        }

        private void CheckForNegatives(string input)
        {
            if (Regex.IsMatch(input, @"(^|[^\d])-"))
            {

                throw new ArgumentException(GetNegatives(input));
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

        private string NormalizeDelimiter(string input, string delimiter)
        {
            return input.Replace(delimiter, ",");
        }

    }

    //public class Calculator
    //{
    //    private char[] defaultDelimiter = new char[] {','};
    //    private char[] additionalDelimiter = 

    //    public int Add(string inputString)
    //    {
    //        char[] delimiters;
            
    //        if (CustomDelimiterSpecified(inputString))
    //        {
    //            delimiters = GetCustomDelimiter(inputString).Concat(defaultDelimiter).ToArray();
    //        }
    //        else
    //        {
    //            delimiters = defaultDelimiter;
    //        }

    //        string a = RemoveDelimiterInfo(inputString);
    //        string[] numbers = RemoveDelimiterInfo(inputString).Split(delimiters, StringSplitOptions.RemoveEmptyEntries);


    //        CheckForNegativeNumbers(numbers);


    //        int sum = 0;
    //        for (int i = 0; i < numbers.Length; i++)
    //        {       
    //            sum += int.Parse(numbers[i]);
    //        }

    //        return sum;
    //    }

    //    public char[] GetCustomDelimiter(string input)
    //    {
            
    //        {
    //            return new char[] {input.Substring(2, input.IndexOf('\n')).ToCharArray()[0], '\n', ','};
    //        }

    //        return new char[] {' ', ',', '\n'};
    //    }

    //    public string RemoveDelimiterInfo(string input)
    //    {
    //        if (input.IndexOf("//") == 0)
    //        {
    //            return input.Substring(input.LastIndexOf("\n"));
    //        }
    //        return input;
    //    }

    //    public static void CheckForNegativeNumbers(IEnumerable<int> numbers)
    //    {
    //        if (numbers.Any(number => number < 0))
    //        {
    //            throw new ArgumentException("neg");
    //        }
    //    }

    //    public bool CustomDelimiterSpecified(string input)
    //    {
    //        return Regex.IsMatch(input, @"//.{1,2}\n");
    //    }
        
    //}
}
