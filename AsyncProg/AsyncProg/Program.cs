using System.Threading.Tasks;

namespace AsyncProg;

class SimpleStack<T>
{

    private List<T> data = new List<T>();

    private object lockObject = new object();
    public T Top
    {
        get
        {
            lock (lockObject)
            {
                int idx = this.data.Count - 1;
                if (idx == -1)
                {
                    throw new StackEmptyException();
                }
                return data[idx];
            }
        }
    }


    public bool IsEmpty
    {
        get
        {
            return this.data.Count == 0;
        }
    }


    public void Push(T val)
    {
        lock (lockObject)
        {
            this.data.Add(val);
        }
    }


    public bool TryPop(out T result)
    {
        lock (lockObject)
        {
            if(this.IsEmpty)
            {
                result = default;
                return false;
            }
            int idx = this.data.Count - 1;
            if (idx == -1)
            {
                throw new StackEmptyException();
            }
            T val = this.data[idx];
            this.data.RemoveAt(idx);
            result = val;
            return true;
        }
    }



    public class StackEmptyException : Exception
    {

    }
}
class Program
{

    private static async Task Write()
    {
        using StreamWriter fs = new StreamWriter("test.txt");
        // Task task = fs.WriteAsync("A");
        await fs.WriteAsync("50");
        // return task;
    }

    private static async Task<int> Read()
    {
        using StreamReader sr = new StreamReader("test.txt");
        string txt = await sr.ReadToEndAsync();
        return int.Parse(txt);
    }
    static async Task Main(string[] args)
    {
        await Write();
        Console.WriteLine("Done");
        int result = await Read();
        Console.WriteLine(result);
        // Random rand = new Random();
        // SimpleStack<int> stack = new SimpleStack<int>();
        // object lockObject = new object();
        // Thread thread = new Thread(() =>
        // {
        //     while(true)
        //     {
        //         stack.Push(rand.Next());
        //         lock (lockObject)
        //         {
        //             Monitor.Pulse(lockObject);
        //         }
        //         Thread.Sleep(100);
        //     }
        // });
        // thread.Start();

        // for (int i = 0; i < 5; i++)
        // {
        //     Thread t = new Thread(() =>
        //     {
        //         while (true)
        //         {
        //             if(stack.TryPop(out int result))
        //             {
        //                 Console.WriteLine("Result: " + result + " Thread: " + Thread.CurrentThread.ManagedThreadId);
        //             }else
        //             {
        //                 Console.WriteLine("Stack is empty");
        //                 lock(lockObject)
        //                 {
        //                     Monitor.Wait(lockObject);
        //                 }
        //             }
        //             Thread.Sleep(rand.Next(40, 1000));
        //         }
        //     });
        //     t.Start();
        // }

        // int tid = Thread.CurrentThread.ManagedThreadId;


        // SimpleStack<int> stack = new SimpleStack<int>();
        // Random rand = new Random();

        // for (int i = 0; i < 5; i++)
        // {
        //     Thread t = new Thread(() =>
        //     {
        //         while (true)
        //         {
        //             if (rand.NextDouble() < 0.8)
        //             {
        //                 if (!stack.TryPop(out int result))
        //                 {
        //                     Console.WriteLine(result);
        //                 }
        //                 else
        //                 {
        //                     stack.Push(rand.Next());
        //                 }
        //             }
        //         }
        //     });
        //     t.Start();
        // }
    }
}
