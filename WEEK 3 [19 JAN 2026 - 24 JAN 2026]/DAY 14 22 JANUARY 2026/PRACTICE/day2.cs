#define TEST
using System;
using System.Diagnostics;

class day2
{
    static void Main()
    {
        Console.WriteLine("=== 1. Parse vs TryParse vs Convert ===");

        // Parse()
        string numStr = "123";
        int num1 = int.Parse(numStr);
        Console.WriteLine("Parse: " + num1);

        // TryParse()
        string input = "ABC";
        bool success = int.TryParse(input, out int num2);
        Console.WriteLine("TryParse Success: " + success);
        Console.WriteLine("TryParse Value: " + num2);

        // Convert
        string nullStr = null;
        int num3 = Convert.ToInt32(nullStr); // returns 0
        double d = Convert.ToDouble("12.34");
        Console.WriteLine("Convert (null to int): " + num3);
        Console.WriteLine("Convert (string to double): " + d);

        Console.WriteLine("\n=== 2. Debugging Demo ===");

        int x = 10;
        int y = 20;
        int z = x + y; // Put breakpoint here

        Debug.WriteLine("Debugging value of z: " + z);
        Trace.WriteLine("Tracing value of z: " + z);

        Console.WriteLine("z = " + z);

        Console.WriteLine("\n=== 3. Project Type Examples (Conceptual) ===");
        Console.WriteLine("Console App Example: Hello World");

        Console.WriteLine("\n=== 4. Trace vs Debug vs Build ===");
        Debug.WriteLine("This appears only in Debug build");
        Trace.WriteLine("This appears in Debug and Release build");

        Console.WriteLine("\n=== 5. Compile Options / Conditional Compilation ===");

#if DEBUG
        Console.WriteLine("Running in DEBUG mode");
#else
        Console.WriteLine("Running in RELEASE mode");
#endif

#if TEST
        Console.WriteLine("TEST mode enabled");
#endif
    }
}
