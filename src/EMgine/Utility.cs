/* =======================================================================================================
 * Copyright (c) 2023 G. M. Smith and J. Carette
 * Release under the BSD 3-Clause license
 * .NET Framework 4.8.04084 \ .NET Standard Library 2.0.3 \ .NET Core Platforms 1.1.0 \ 
 * NuGet Package Manager 6.4.0 \ Windows 10.0.19044
 * -------------------------------------------------------------------------------------------------------
 * Utility Library
 * -------------------------------------------------------------------------------------------------------
 * Useful function that are not specific to any part of EMgine.
 * =======================================================================================================
 * Relies on: ---
 * =======================================================================================================
 * STATIC CODE METRICS SUMMARY
 * -------------------------------------------------------------------------------------------------------
 * Maintainability Index: 84
 * Cyclomatic Complexity: 16
 * Depth of Inheritance: 1
 * Class Coupling: 3
 * Lines of Source Code: 63
 * Lines of Executable Code: 7
 * =======================================================================================================
 */

using System;

namespace EMgine
{
    internal static class Utility
    {
        public static double Clamp(double v, double min, double max)
        {
            if (v >= min && v <= max) return v;
            else if (v < min) return min;
            else return max;
        }

        public static double ClampLeftBound(double v, double min)
        {
            if (v >= min) return v;
            else return min;
        }

        public static double ClampRightBound(double v, double max)
        {
            if (v <= max) return v;
            else return max;
        }

        public static void PrintMsg(string code, string[] args)
        {
            /* 
             * 2022-12-05: Updated maximum arg length from 2 to 4
             * Found By: ConstructNegativePADPoint (Test_PAD3DPointT) 
             *           ConstructPositivePADPoint
             *           ConstructMixedPADPoint
             */
            if (args.Length > 4) Console.Out.WriteLine(MessageLookup.GetMessage("W-UTIL_PRINT_MSG"));

            string msg = MessageLookup.GetMessage(code);

            switch (args.Length)
            {
                /* 
                 * 2022-11-28: Added case for when there is no additional information
                 * needed to create the warning/error message
                 * Found By: NormalizeIntensityTest (Test_EmIntensityT)
                 */
                case 0:
                    Console.WriteLine(msg);
                    break;
                case 1:
                    Console.Out.WriteLine(string.Format(msg, args[0]));
                    break;
                case 2:
                    Console.Out.WriteLine(string.Format(msg, args[0], args[1]));
                    break;
                case 3:
                    Console.Out.WriteLine(string.Format(msg, args[0], args[1], args[2]));
                    break;
                case 4:
                    Console.Out.WriteLine(string.Format(msg, args[0], args[1], args[2], args[3]));
                    break;
                default:
                    Console.Out.WriteLine(MessageLookup.GetMessage("W-UTIL_PRINT_MSG"));
                    break;
            }

            return;
        }
    }
}
