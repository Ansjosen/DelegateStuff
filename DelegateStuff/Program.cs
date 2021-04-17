using System;

namespace DelegateStuff
{
    delegate double OperatorDelegate(double x, double y);
    delegate int opx (double x, double y);

    class Arithmetic
    {   //Har sat metoderne på en linje, præcis det samme 
        internal double Plus(double m, double n)  {Console.WriteLine($"Addition:       {m} + {n} = {m + n}"); return m + n; }
        internal double Gange(double m, double n) {Console.WriteLine($"Multiplikation: {m} x {n} = {m * n}"); return m * n; }
        internal double Minus(double m, double n) {Console.WriteLine($"Subtraction:    {m} - {n} = {m - n}"); return m - n; }
        internal double Dele(double m, double n)  {Console.WriteLine($"Division:       {m} / {n} = {m / n}"); return m / n; }
    }
        
    class Program
    {
        static void Main()
        {
            //Har soigneret outputtet 

            Arithmetic m = new Arithmetic();

            // Delegate instantiation.
            OperatorDelegate mathOpr = m.Plus;
            mathOpr += m.Gange;
            mathOpr += m.Minus;
            mathOpr += m.Dele;

            // Invoke the delegate object.
            mathOpr(16, 2);  //Multicast -alle 4 regningsarter. Output -> 18, 32, 14, 8
            mathOpr.Invoke(8, 2); //Ingen forskel ift ovenst., bortset fra output --> 10, 16, 6, 4

            Console.WriteLine("Brug af foreach:");
            double nonsenseTotal = 0; //akkumulerer alle resultaterne fra de 4 regningsarter. Hvorfor: for at demonstrere outputtet fra delegate item
            foreach (Delegate item in mathOpr.GetInvocationList())
            {
                //Looper alle delegates i multicast igennem og eksekverer dem
                nonsenseTotal += (double)item.DynamicInvoke(20, 2);
            }
            Console.WriteLine("Nonsense total på alle: " + nonsenseTotal);
            //filtrer: kun addition og multiplikation må eksekveres

            Console.WriteLine("Filtreret på Plus og Gange:");
            nonsenseTotal = 0;
            double arg1 = 20, arg2 = 5;
            foreach (Delegate item in mathOpr.GetInvocationList())
            {
                //Filtrer: kun addition og multiplikation må eksekveres
                if (item.Method.Name == "Plus")
                   nonsenseTotal += (double) item.DynamicInvoke(arg1, arg2);
                
                if (item.Method.Name == "Gange")
                    nonsenseTotal += (double)item.DynamicInvoke(arg1, arg2);  //redundant, burde refaktoreres

            }
            Console.WriteLine("Nonsense total på plus og gange: " + nonsenseTotal);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
