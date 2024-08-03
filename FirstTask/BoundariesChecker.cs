using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class BoundariesChecker
    {
        private static readonly int firstLetterFromAbc = 'a';
        private static readonly int lastLetterFromAbc = 'z';

        public static (bool isValid, List<char> invalidCharacters) Check(string text)
        {
            List<char> invalidCharacters = new List<char>();

            foreach (var c in text)
            {
                if (!CheckCharacterBoundaries(c))
                {
                    invalidCharacters.Add(c);
                }
            }

            return (invalidCharacters.Count == 0, invalidCharacters);
        }

        private static bool CheckCharacterBoundaries(char symbol)
        {
            return (symbol >= firstLetterFromAbc) && (symbol <= lastLetterFromAbc);
        }
    }
}
