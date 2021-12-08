using System;
using System.Text.RegularExpressions;

namespace StringVerification
{
    public static class IsbnVerifier
    {
        /// <summary>
        /// Verifies if the string representation of number is a valid ISBN-10 identification number of book.
        /// </summary>
        /// <param name="number">The string representation of book's number.</param>
        /// <returns>true if number is a valid ISBN-10 identification number of book, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if number is null or empty or whitespace.</exception>
        public static bool IsValid(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("Source string cannot be null or empty or whitespace.");
            }

            Regex[] regexArray = new Regex[8]
            {
                new Regex(@"^\d{9}\w{1}$"),
                new Regex(@"^\d{9}-\w{1}$"),
                new Regex(@"^\d{1}-\d{8}\w{1}$"),
                new Regex(@"^\d{4}-\d{5}\w{1}$"),
                new Regex(@"^\d{1}-\d{8}-\w{1}$"),
                new Regex(@"^\d{4}-\d{5}-\w{1}$"),
                new Regex(@"^\d{1}-\d{3}-\d{5}\w{1}$"),
                new Regex(@"^\d{1}-\d{3}-\d{5}-\w{1}$"),
            };
            
            bool isMatch = false;

            for (int i = 0; i < regexArray.Length; i++)
            {
                if (regexArray[i].IsMatch(number))
                {
                  isMatch = true;
                  break;
                }
            }

            if (!isMatch)
            {
                return false;
            }

            int sum = 0;
            number = number.Replace("-", string.Empty, StringComparison.InvariantCulture);

            for (int i = 0, j = number.Length; i < number.Length; i++, j--)
            {
                if (char.IsDigit(number[i]))
                {
                    sum += (number[i] - '0') * j;
                }
                else if (number[i] == 'X')
                {
                    sum += 10;
                }
            }

            return sum % 11 == 0;
        }
    }
}
