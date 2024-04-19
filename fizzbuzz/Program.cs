namespace fizzbuzz;

class Program
{
    static void Main(string[] args)
    {
        for (int i = 1; i <= 100; i++)
        {
            bool fizz = i%3 == 0;
            bool buzz = i%5 == 0;
            switch (fizz, buzz)
            {
                case (true, true):
                    Console.WriteLine("fizzbuzz");
                    break;
                case (true, false):
                    Console.WriteLine("fizz");
                    break;
                case (false, true):
                    Console.WriteLine("buzz");
                    break;
                default: 
                    Console.WriteLine(i);
                    break;
            }
        }


    }
}
