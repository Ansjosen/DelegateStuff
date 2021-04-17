using System;

namespace DelegateStuff
{
    delegate double OperatorDelegate(double x, double y);

    class Arithmetic
    {
        internal double Plus(double m, double n) { Console.WriteLine("Plus: " + (m + n)); return m + n; }
        internal double Gange(double m, double n) { Console.WriteLine("Mul: " + (m * n)); return m * n; }
        internal double Minus(double m, double n) { Console.WriteLine("Sub: " + (m - n)); return m - n; }
        internal double Dele(double m, double n) { Console.WriteLine("Div: " + (m / n)); return m / n; }
    }

    class Program
    {
        static void Main()
        {
            Arithmetic m = new Arithmetic();

            // Delegate instantiation.
            OperatorDelegate mathOpr = m.Plus;
            mathOpr += m.Gange;
            mathOpr += m.Minus;
            mathOpr += m.Dele;

            // Invoke the delegate object.
            mathOpr(16, 2);  //Multicast -alle 4 regningsarter. Output -> 18, 32, 14, 8
            mathOpr.Invoke(8, 2); //Ingen forskel ift ovenst., bortset fra output --> 10, 16, 6, 4

            foreach (Delegate item in mathOpr.GetInvocationList())
            {
                //Looper alle delegates i multicast igennem og eksekverer dem
                Console.WriteLine(item.DynamicInvoke(20, 2) + ": " + item.Method.Name);
            }
            
            //Filtrer: kun addition og multiplikation må eksekveres
            foreach (Delegate item in mathOpr.GetInvocationList())
            {
                if (item.Method.Name == "Plus")
                    Console.WriteLine(item.DynamicInvoke(20, 5) + ": " + item.Method.Name);
                if (item.Method.Name == "Gange")
                    Console.WriteLine(item.DynamicInvoke(20, 5) + ": " + item.Method.Name);
            }
            
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
