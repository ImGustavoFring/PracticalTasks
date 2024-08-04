using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public static class CharacterCounter
    {
        public static Dictionary<char, int> CountCharacters(string text)
        {
            var characterCounts = new Dictionary<char, int>();

            foreach (var symbol in text)
            {
                if (characterCounts.ContainsKey(symbol))
                {
                    characterCounts[symbol]++;
                }
                else
                {
                    characterCounts[symbol] = 1;
                }
            }

            return characterCounts;
        }
    }
}
