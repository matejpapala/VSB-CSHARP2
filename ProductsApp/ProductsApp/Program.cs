namespace ProductsApp
{
    public delegate int Operation(int firstNum, int secondNum);
    
    public static class MyMath
    {
        public static int Pow(this int number, int n)
        {
            return (int)Math.Pow(number, n);
        }

        public static IEnumerable<int> EvenNums(this IEnumerable<int> list) {
            int i = 0;
            foreach(int item in list)
            {
                if(i % 2 == 0)
                {
                    yield return item;
                }
                i++;
            }
        }

        public static IEnumerable<int> Filter(this IEnumerable<int> list, Condition cond) {
            int i = 0;
            foreach(int item in list)
            {
                if(cond(item))
                {
                    yield return item;
                }
                i++;
            }
        }
    }

    public delegate bool Condition(int n);
    internal class Program
    {
        
        private static int Sum(int x, int y)
        {
            return x + y;
        }
        static void Main(string[] args)
        {
            IEnumerable<Product> products = GetProducts();
            Calculator calculator = new Calculator();
            calculator.OnSetXY += (calculator, args) => Console.WriteLine("X, Y nastaveno");
            // calculator.SetXY(1, 3);
            calculator.OnCompute += (calculator, args) => Console.WriteLine("Vysledek: " + args.Result);
            // calculator.Execute((x, y) => x + y);


            int x = 3;
            int result = x.Pow(2);

            // Console.WriteLine(result);

            List<int> MyList = new List<int>();
            for(int i = 0; i < 10;i++)
            {
                MyList.Add(i);
            }
            // foreach(int item in MyList.Filter(x => x < 5))
            // {
            //     Console.WriteLine(item);
            // }


            foreach(Product p in products.Where(x => x.Price > 20000))
            {
                Console.WriteLine(p.Name);
            }

            double avg = products.Average(x => x.Price.Value);
            // double avg = products.Select(x => x.Price.Value).Average();
            Console.WriteLine(avg);

            double avgIfNotZero = products.Where(x => x.Quantity != 0).Average(x => x.Price.Value);

            foreach(string item in products.Select(x => x.Name))
            {
                Console.WriteLine(item);
            }

            Product first = products.First();
            Product last = products.Last();
            
        }



        private static IEnumerable<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product(){ Id = 1, Name = "Auto", Price = 700_000, Quantity = 10 },
                new Product(){ Id = 1, Name = "Slon", Price = 1_500_000, Quantity = 0 },
                new Product(){ Id = 1, Name = "Kolo", Price = 26_000, Quantity = 5 },
                new Product(){ Id = 1, Name = "Brusle", Price = 2_800, Quantity = 30 },
                new Product(){ Id = 1, Name = "Hodinky", Price = 18_500, Quantity = 2 },
                new Product(){ Id = 1, Name = "Mobil", Price = 24_000, Quantity = 0 }
            };
        }
    }
}
