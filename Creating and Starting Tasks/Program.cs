using System.Threading.Tasks;
using static System.Console;

namespace Creating_and_Starting_Tasks
{
    class Program
    {
        public static void Write(char c)
        {
            int i =1000;
            while (i --> 0)
            {
                System.Console.Write(c);
            }
        }
        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                System.Console.Write(o);
            }
        }

        public static int GetTextLength(object o)
        {
            WriteLine($"\nTask with id: {Task.CurrentId} processing object {o}... ");
            return o.ToString().Length;
        }
        static void Main()
        {
            // way 1: make task and start
            Task.Factory.StartNew(() => Write('.'));

            //way 2 : crating task by constructor
            var t = new Task(() => Write('?'));
            t.Start();

            //main thread
            Write('-');

            // new overload of Task constructor
            Task t2 = new Task(Write, "hello");
            t2.Start();

            Task.Factory.StartNew(Write, 123);

            //task with result
            string text1 = "testing", text2 = "this";
            var t3 = new Task<int>(GetTextLength, text1);
            t3.Start();

            Task<int> t4 = Task.Factory.StartNew(GetTextLength, text2);

            WriteLine($"Length of {text1} is {t3.Result}");
            WriteLine($"Length of {text2} is {t4.Result}");

            WriteLine("Main program done");
            ReadLine();


        }
    }
}
