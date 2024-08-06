using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Run();
        }

        private static async Task Run()
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
                return;
            }

            string result = StringChanger.Change(input);
            Console.WriteLine($"Обработанная строка: {result}");

            DisplayCharacterCounts(result);

            string longestSubstring = SubstringFinder.FindLongestVowelSubstring(result);
            DisplayLongestVowelSubstring(longestSubstring);

            string sortedResult = await GetSortedResult(result);
            Console.WriteLine($"Отсортированная строка: {sortedResult}");

            int randomIndex = await RandomNumberGenerator.GetRandomNumberAsync(
                "https://www.randomnumberapi.com/api/v1.0/", result.Length);

            string truncatedResult = RemoveCharacterAtIndex(result, randomIndex);
            Console.WriteLine($"Урезанная обработанная строка: {truncatedResult}");
        }

        private static void DisplayCharacterCounts(string result)
        {
            var characterCounts = CharacterCounter.CountCharacters(result);
            Console.WriteLine("Количество повторений каждого символа:");

            foreach (var pair in characterCounts)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }

        private static void DisplayLongestVowelSubstring(string longestSubstring)
        {
            if (longestSubstring != string.Empty)
            {
                Console.WriteLine($"Самая длинная подстрока, начинающаяся и заканчивающаяся на гласную: {longestSubstring}");
            }
            else
            {
                Console.WriteLine("В обработанной строке нет подстрок, начинающихся и заканчивающихся на гласную.");
            }
        }

        private static async Task<string> GetSortedResult(string result)
        {
            Console.WriteLine("Выберите алгоритм сортировки (1 - Быстрая сортировка, 2 - Сортировка деревом):");
            string? choice = Console.ReadLine();

            return choice switch
            {
                "1" => Quicksort.Sort(result),
                "2" => TreeSort.Sort(result),
                _ => throw new InvalidOperationException("Неверный выбор алгоритма.")
            };
        }

        private static string RemoveCharacterAtIndex(string text, int index)
        {
            if (index < 0 || index >= text.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне допустимого диапазона.");
            }

            return text.Remove(index, 1);
        }
    }
}
