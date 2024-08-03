using System;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            Console.WriteLine("Введите строку:");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Ошибка: строка не должна быть пустой.");
                return;
            }

            var (isValid, invalidCharacters) = BoundariesChecker.Check(input);

            if (!isValid)
            {
                Console.WriteLine($"Ошибка: введены неподходящие символы: {string.Join(", ", invalidCharacters)}");
            }
            else
            {
                string result = StringChanger.Change(input);
                Console.WriteLine($"Обработанная строка: {result}");

                var characterCounts = CharacterCounter.CountCharacters(result);
                Console.WriteLine("Количество повторений каждого символа:");

                foreach (var pair in characterCounts)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }

                string longestSubstring = SubstringFinder.FindLongestVowelSubstring(result);

                if (longestSubstring != string.Empty)
                {
                    Console.WriteLine($"Самая длинная подстрока, начинающаяся и заканчивающаяся на гласную: {longestSubstring}");
                }
                else
                {
                    Console.WriteLine("В обработанной строке нет подстрок, начинающихся и заканчивающихся на гласную.");
                }
            }
        }
    }
}
