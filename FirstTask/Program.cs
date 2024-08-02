namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // мне объяснили сделать какое-то изменение, чтобы реализовать merge request, которого, как опции не существует
            Run();  
        }
        
        private static void Run ()
        {
            Console.WriteLine(StringChanger.Change("a"));
            Console.WriteLine(StringChanger.Change("abcdef"));
            Console.WriteLine(StringChanger.Change("abcde"));
        }
    }
}
