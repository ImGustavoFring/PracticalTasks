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

            var (isValid, invalidCharacters) = BoundariesChecker.Check(input);

            if (!isValid)
            {
                Console.WriteLine($"Ошибка: введены неподходящие символы: {string.Join(", ", invalidCharacters)}");
            }
            else
            {
                string result = StringChanger.Change(input);
                Console.WriteLine($"Обработанная строка: {result}");
            }
        }
    }
}
