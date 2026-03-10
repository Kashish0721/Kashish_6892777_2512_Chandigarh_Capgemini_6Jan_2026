using System;
using System.Collections.Generic;
using System.Text;

namespace INTERFACE_PRACTICE
{
    internal class Interface
    {
        interface Inter1
        {
            void f1();
        }
        interface Inter2
        {
            void f1();
        }
        class C3 : Inter1, Inter2
        {
            public void f1()
            {
                Console.WriteLine("this is overriding function of inter1 and inter2 interfaces");
            }

        }


        class ClsInterface1
        {
            static void Main(string[] args)
            {
                C3 obj1 = new C3();
                obj1.f1();
                Console.Read();
            }
        }

    }
}
