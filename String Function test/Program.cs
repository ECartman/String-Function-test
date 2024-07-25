using System;
using System.Collections.Generic;
using System.Text;

namespace String_Function_test
{
    internal class Program
    {
        static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        static TypewritterEmulator emulator = new TypewritterEmulator();
        static string nonprinted = null;

        static void Main(string[] args)
        {
            

            string test =
                "short no new lineshort no new lineshort no new lineshort no new lineshort no new line";
            testStrings(test, "no LF");
            test =
                "short and sweet \r\n";
            testStrings(test, "short text");
            //TEST 1 done
            test = "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "cater, consectetur adipiscing elit, sed do eiusmod\r\nLorem\r cater dolor sit \r\n ";
            testStrings(test, "Some CR");
            //TEST 2 DONE
            test = "amet, consectetur adipiscing elit, sed do eiusmod\rLorem ipsum dolor sit\r\n";
            testStrings(test, "Some 2");
            //TEST 3 DONE
            //the following string is "long" the result however likely not due deletion of characters due CR
            test = "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                 "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                 "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                 "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                 "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                 "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                 "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                 "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r " +
                "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r ";
            testStrings(test, "long?");
            nonprinted = emulator.reset();
             test = "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r ";
            emulator.Output(test);
            test ="amet, consectetur adipiscing elit este texto es el tercero" +
                "\rboooor" +
                "\r boor " +
                "\reste es el segundo texto visible " +
                "\rnada" +
                "\r Primero " +
                "\r\n ";
            nonprinted = emulator.reset();
            emulator.Output(test);
            nonprinted = emulator.reset();
            test = "amet, consectetur adipiscing elit, sed do eiusmod\rLorem\r ipsum dolor sit \r ";
            emulator.OutputOriginal( test);
            nonprinted = emulator.reset();
            test = "amet, consectetur adipiscing elit este texto es el tercero" +
                "\rboooor" +
                "\r boor " +
                "\reste es el segundo texto visible " +
                "\rnada" +
                "\r Primero " +
                "\r\n ";
            emulator.OutputOriginal(test);
            nonprinted = emulator.reset();
            Console.ReadLine();


        }

        private static void testStrings(string test, string title = "test")
        {
            Console.WriteLine($"---------{title}---------------------------");
            nonprinted = emulator.reset();
            sw.Reset();
            sw.Start();
            emulator.OutputOriginal(test);
            sw.Stop();
            string result1 = (sw.Elapsed.TotalMilliseconds).ToString();
           nonprinted = emulator.reset();
            if (!String.IsNullOrEmpty(nonprinted))
            {
                Console.WriteLine($"OutputOriginal residual: {nonprinted}");
            }
            sw.Reset();
            sw.Start();
            emulator.OutputOriginal(test);
            sw.Stop();
            string result2 = (sw.Elapsed.TotalMilliseconds).ToString();
            nonprinted = emulator.reset();
            if (!String.IsNullOrEmpty(nonprinted))
            {
                Console.WriteLine($"OutputOriginal residual: {nonprinted}");
            }
            Console.WriteLine($"---------{title}---------");            
            sw.Reset();
            sw.Start();
            emulator.Output(test);
            sw.Stop();
            string result3 = (sw.Elapsed.TotalMilliseconds).ToString();
            nonprinted = emulator.reset();
            if (!String.IsNullOrEmpty(nonprinted))
            {
                Console.WriteLine($"Output residual: {nonprinted}");
            }
            sw.Reset();
            sw.Start();
            emulator.Output(test);
            sw.Stop();
            nonprinted = emulator.reset();
            if (!String.IsNullOrEmpty(nonprinted))
            {
                Console.WriteLine($"Output residual: {nonprinted}");
            }
            Console.WriteLine($"Results are in:");
            Console.WriteLine($"OutputOriginal: {result1}");
            Console.WriteLine($"OutputOriginal: {result2}");
            Console.WriteLine($"Output(test): {result3} ");
            Console.WriteLine($"Output(test): {sw.Elapsed.TotalMilliseconds} ");
            Console.WriteLine();
        }

    }
}
