/* =======================================================================================================
 * Copyright (c) 2023 G. M. Smith and J. Carette
 * Release under the BSD 3-Clause license
 * .NET Framework 4.8.04084 \ .NET Standard Library 2.0.3 \ .NET Core Platforms 1.1.0 \ 
 * NuGet Package Manager 6.4.0 \ Windows 10.0.19044
 * -------------------------------------------------------------------------------------------------------
 * Emotion Intensity ADT
 * -------------------------------------------------------------------------------------------------------
 * The Emotion Intensity Type Module is an ADT for defining emotion intensity and emotion intensity change 
 * variables. Emotion intensity change depends on the existance of emotion intensity, so it is defined as
 * a nested class with emotion intensity as the parent.
 * Emotion intensity is bounded by [MIN_INTENSITY, +Infinity), but emotion intensity change is unbounded. 
 * 
 * This design is application-agnostic and can exist independently of EMgine.
 * 
 * EmIntensityT and EmIntensityChgT should not have any child classes (marked as sealed classes).
 * =======================================================================================================
 * Relies on: ---
 * =======================================================================================================
 * STATIC CODE METRICS SUMMARY
 * -------------------------------------------------------------------------------------------------------
 * EmIntensityT
 * -------------------------------------------------------------------------------------------------------
 * Maintainability Index: 88
 * Cyclomatic Complexity: 20
 * Depth of Inheritance: 1
 * Class Coupling: 4
 * Lines of Source Code: 155
 * Lines of Executable Code: 29
 * -------------------------------------------------------------------------------------------------------
 * EmIntensityT.EmIntensityChgT
 * -------------------------------------------------------------------------------------------------------
 * Maintainability Index: 92
 * Cyclomatic Complexity: 9
 * Depth of Inheritance: 1
 * Class Coupling: 0
 * Lines of Source Code: 45
 * Lines of Executable Code: 9
 * =======================================================================================================
 */

using System;

namespace EMgine
{
    /* ===================================================
     * Emotion Intensity Type
     * ===================================================
     */
    public sealed class EmIntensityT
    {
        /* ---------------------------------------------------
         * VARIABLES
         * ---------------------------------------------------
         */

        /* Constants */
        private const double _MIN_INTENSITY = 0.0;

        /* State Variables */
        private double _intensity;

        /* ---------------------------------------------------
         * METHODS
         * ---------------------------------------------------
         */

        /* Constructor */
        public EmIntensityT(double i) => SetIntensity(i);

        /* Accessors */
        public int CompareToIntensity(EmIntensityT i) => _intensity.CompareTo(i.GetIntensity());
        public bool EqualsMinIntensity() => _intensity == _MIN_INTENSITY ;

        public EmIntensityT Normalize(EmIntensityT scale)
        {
            EmIntensityT unit = null;

            if (scale.GetIntensity() == 0) Utility.PrintMsg("E-EIT_INTENSITY_CANNOT_BE_ZERO", Array.Empty<string>());
            else unit = new EmIntensityT(_intensity / scale.GetIntensity());

            return unit;
        }

        public EmIntensityT UpdateWithChg(EmIntensityChgT chg)
        {
            if (chg.CompareToMinIntensity() > 0)
            {
                return new EmIntensityT(
                    0.1 * Math.Log(Math.Pow(2.0, 10.0 * _intensity)
                    + Math.Pow(2.0, 10.0 * chg.ToReal()), 2.0)
                );
            }
            else if (chg.CompareToMinIntensity() < 0)
            {
                /* 
                 * 2022-12-23: If chg >= i, the function tries to take the log of a
                 * zero/negative number. It should return 0 : I instead.
                 * Found By: EmStateTUpdateAllIntensitiesWithChg
                 */
                double newI = 0.0;
                EmIntensityChgT absChg = chg.Copy().ScaleByValue(-1);
                if (_intensity > absChg.ToReal())
                {
                    newI = 0.1 * Math.Log(Math.Pow(2.0, 10.0 * _intensity)
                        - Math.Pow(2.0, 10.0 * absChg.ToReal()), 2.0);
                }
                return new EmIntensityT(newI);
            }
            else
            {
                return new EmIntensityT(_intensity);
            }
        }

        public double ToReal() => _intensity;

        /* 
         * 2022-12-23: Classes are reference types, so it can be dangerous
         * to manipulate them safely. Adding a Copy() function to create a
         * new, identical EmIntensityT.
         * Found By: EmStateTUpdateAllIntensitiesWithChg (Test_EmStateT)
         */
        public EmIntensityT Copy() => new EmIntensityT(_intensity);

        /* Unsafe/Debugging */
        public double GremlinGetIntensity() => GetIntensity();
        public static double GremlinGetMinIntensity() => GetMinIntensity();
        public void GremlinSetIntensity(double i) => SetIntensity(i);
        public string GremlinIntensityToString() => _intensity.ToString();
        public static string GremlinMinIntensityToString() => _MIN_INTENSITY.ToString();

        /* Utility */
        private double GetIntensity() => _intensity;
        private static double GetMinIntensity() => _MIN_INTENSITY;
        private void SetIntensity(double i)
        {
            /* 
             * 2022-12-15: Using Math.Max can cause issues when there are many 
             * decimal places in lambda. Switching to if-else structure.
             * Found By: ConstructDecayRate (Test_EmIntensityDecayT)
             *           CompareToDecayRate (Test_EmIntensityDecayT)
             */
            if (i < _MIN_INTENSITY)
            {
                Utility.PrintMsg("W-EIT_INTENSITY_TOO_SMALL", new string[] { GremlinMinIntensityToString() });
                _intensity = _MIN_INTENSITY;
            }
            else
            {
                _intensity = i;
            }
        }

        /* ===================================================
         * Emotion Intensity Change Type (Nested Class)
         * ===================================================
         */
        public sealed class EmIntensityChgT
        {
            /* ---------------------------------------------------
             * VARIABLES
             * ---------------------------------------------------
             */

            /* State Variables */
            private readonly double _iDelta;

           /* ---------------------------------------------------
            * METHODS
            * ---------------------------------------------------
            */

            /* Constructor */
            public EmIntensityChgT(double delta) => _iDelta = delta;

            /* Accessors */
            public int CompareToIntensityChg(EmIntensityChgT d) => _iDelta.CompareTo(d.GetDelta());
            public int CompareToMinIntensity() => _iDelta.CompareTo(GetMinIntensity());

            public EmIntensityChgT ScaleByValue(double v) => new EmIntensityChgT(_iDelta * v);

            public double ToReal() => _iDelta;

            /* 
             * 2022-12-23: Classes are reference types, so it can be dangerous
             * to manipulate them safely. Adding a Copy() function to create a
             * new, identical EmIntensityChgT.
             * Found By: EmStateTUpdateAllIntensitiesWithChg (Test_EmStateT)
             */
            public EmIntensityChgT Copy() => new EmIntensityChgT(_iDelta);

            /* Unsafe/Debugging */
            public double GremlinGetDelta() => GetDelta();
            public string GremlinDeltaIntensityToString() => _iDelta.ToString();

            /* Utility */
            private double GetDelta() => _iDelta;
        }
    }
}
