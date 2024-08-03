using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class SubstringFinder
    {
        private static readonly HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'y' };

        public static string FindLongestVowelSubstring(string text)
        {
            string longestSubstring = string.Empty;
            int maxLength = 0;

            for (int i = 0; i < text.Length; i++)
            {
                // проверяем, является ли текущий символ гласной
                if (vowels.Contains(text[i]))
                {
                    // проходим по строке с конца к началу до текущего символа
                    for (int j = text.Length - 1; j > i; j--)
                    {
                        // проверяем, является ли символ на позиции j гласной
                        if (vowels.Contains(text[j]))
                        {
                            int length = j - i + 1;

                            // если длина подстроки больше максимальной длины, обновляем
                            if (length > maxLength)
                            {
                                maxLength = length;
                                longestSubstring = text.Substring(i, length);
                            }
                            // прекращаем, так как нашли ближайшую гласную с конца строки
                            break;
                        }
                    }
                }
            }

            return longestSubstring;
        }
    }
}

