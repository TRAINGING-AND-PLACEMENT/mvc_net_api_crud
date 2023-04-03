using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
            public delegate void mdelegate();
            public class md
            {
                static public void display()
                {
                    Console.Write("Display");
                }
                static public void hello()
                {
                    Console.Write("Hello");
                }
            }
            static void Main(String[] args)
            {
                mdelegate m1 = new mdelegate(md.display);
                mdelegate m2 = new mdelegate(md.hello);

                m1();
                m2();

            Console.WriteLine(DateTime.Now);
                Console.ReadKey();
            }

    }
}
