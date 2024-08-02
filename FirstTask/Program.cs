namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
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
