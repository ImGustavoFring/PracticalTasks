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

                Console.WriteLine("Количество символов обработанного текста:");

                foreach (var pair in characterCounts)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
        }
    }
}
