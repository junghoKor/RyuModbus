using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using RyuSerial;

namespace Test1
{
    class Program
    {

        static Int64 g_val = 0xFFFFFFFF;

        static void Main(string[] args)
        {

            Console.WriteLine("Hello, World.");


            //var thread1 => { int i = 0; };

            Func<int> f1 = () => { int i = 0; return 0; };

            Thread t1 = new Thread(t1func);
            Thread t2 = new Thread(t2func);

            t1.Start();
            t2.Start();


            RyuModbus  s1 = new RyuModbus();


            s1.InitThread();


            Thread.Sleep(10000);


            s1.EndThread();



            t1.Join();
            t2.Join();

            int k = 0;
        }


        static void t1func()
        {
            for (int i = 0; i < 1000000; i++)
            {
                g_val++;
                g_val--;
            }
        }


        static void t2func()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Int64 val = g_val;
                if ( false == (val == 0xFFFFFFFF || val == 0x100000000))
                {
                    Console.WriteLine("g_val err = {0:x8}", val);
                }
            }
        }
    }
}
