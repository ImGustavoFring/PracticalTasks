using System;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    public static class StringChanger
    {
        public static string Change(string text)
        {
            bool hasEvenLength = text.Length % 2 == 0;

            if (hasEvenLength)
            {
                return ChangeWithEvenLength(text);
            }
            else
            {
                return ChangeWithOddLength(text);
            }
        }

        private static string ChangeWithEvenLength(string text)
        {
            var result = new StringBuilder();
            int startOfSecondHalf = text.Length / 2;

            string firstHalf = text.Substring(0, startOfSecondHalf);
            string secondHalf = text.Substring(startOfSecondHalf);

            result.Append(new string(firstHalf.Reverse().ToArray()));
            result.Append(new string(secondHalf.Reverse().ToArray()));

            return result.ToString();
        }

        private static string ChangeWithOddLength(string text)
        {
            var result = new StringBuilder();

            string reversedText = new string(text.Reverse().ToArray());
            result.Append(reversedText);
            result.Append(text);

            return result.ToString();
        }
    }
}
