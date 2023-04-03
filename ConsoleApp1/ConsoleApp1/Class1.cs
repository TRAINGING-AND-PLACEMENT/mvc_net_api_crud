using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class A
    {
        public void Adisplay()
        {
            Console.WriteLine("This is method Adisplay in class A");
        }
    }
    public class B : A
    {
        public void Bdisplay()
        {
            Console.WriteLine("This is method Bdisplay in class B");
        }
    }
    internal class Class1
    {
        public static void main(String[] args)
        {
            A a = new A();
            a.Adisplay();

            B b = new B();
            b.Adisplay();
            b.Bdisplay();

            Console.WriteLine(DateTime.Now);

            Console.ReadKey();
        }
    }
}
