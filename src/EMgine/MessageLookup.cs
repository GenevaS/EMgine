/* =======================================================================================================
 * Copyright (c) 2023 G. M. Smith and J. Carette
 * Release under the BSD 3-Clause license
 * .NET Framework 4.8.04084 \ .NET Standard Library 2.0.3 \ .NET Core Platforms 1.1.0 \ 
 * NuGet Package Manager 6.4.0 \ Windows 10.0.19044
 * -------------------------------------------------------------------------------------------------------
 * MessageLookup Library
 * -------------------------------------------------------------------------------------------------------
 * EMgine's Error and Warning message strings. Lookup codes match those from the Module Interface
 * Specification (MIS).
 * =======================================================================================================
 * Relies on: ---
 * =======================================================================================================
 * STATIC CODE METRICS SUMMARY
 * -------------------------------------------------------------------------------------------------------
 * Maintainability Index: 83
 * Cyclomatic Complexity: 2
 * Depth of Inheritance: 1
 * Class Coupling: 1
 * Lines of Source Code: 85
 * Lines of Executable Code: 2
 * =======================================================================================================
 */
using System.Collections.Generic;

namespace EMgine
{
    internal static class MessageLookup
    {
        private static readonly Dictionary<string, string> messages = new Dictionary<string, string>()
        {
            /* 
             * 2022-11-28: Changed formatting string from %s to {0} because %s was being printed as-is
             * (incorrect format string)
             * Found By: ConstructIntensity (Test_EmIntensityT) 
             *           SumIntensityWithChg
             *           SumChgWithIntensity
             */
            // EmIntensityT
            { "E-EIT_INTENSITY_CANNOT_BE_ZERO", "Error: Reference intensity has a value of zero. Cannot complete normalization." },
            { "W-EIT_INTENSITY_TOO_SMALL", "Warning: Value provided is smaller than the minimum emotion intensity. Clamping to the range [{0}, +infty)." },

            // EmotionIntensityCalcLib
            { "E-EIG_BAD_RATIO", "Error: Value for ``ratio'' must in [0, 1]. Cannot complete evaluation." },
            { "E-EIG_BAD_TOLERANCE", "Error: 'Surprise' tolerance must be greater than zero. Cannot complete evaluation." },
            { "W-EIG_NO_RELATION", "Warning: Social attachment object empty. Intensity is zero." },

            // EmStateT
            { "E-EST_TOO_FEW_VALUES_FOR_STATE", "Error: Data provided for {0} emotion kinds, which is less than the {1} types required by the emotion state. No changes made." },
            { "E-EST_TOO_MANY_VALUES_FOR_STATE", "Error: Data provided for {0} emotion kinds, which is more than the {1} types required by the emotion state. No changes made." },
            { "W-EST_INTENSITY_TOO_LARGE", "Warning: Emotion intensity for {0} is larger than the maximum intensity. Clamping to the range [{1}, {2}]." },

            // EmDecayRateT
            { "W-EDR_ILLEGAL_DECAY_RATE", "Warning: Emotion cannot decay at a rate of zero or less. Setting to default decay rate of {0}." },
            { "E-EDR_ILLEGAL_DECAY_CONST", "Error: Decay constant must be greater than zero. Cannot complete evaluation." },
            { "E-EDR_ILLEGAL_ZETA", "Error: Zeta must be greater than zero. Cannot complete evaluation."},
            { "W-EDR_NEGATIVE_TIME_DELTA", "Warning: Cannot evaluate emotion decay prior to time = 0. Setting time = 0."},

            // EmotionDecayStateT
            { "E-EDS_BAD_COUNT_DECAY_RATES", "Error: {0} decay rates given, but there are {1} types in the emotion state. Emotion decay state is unchanged." },
            { "E-EDS_BAD_COUNT_EQUIL_INTST", "Error: {0} intensity values given, but there are {1} types in the emotion state. Emotion decay state is unchanged." },
            { "E-EDS_EQ_SUM_ZERO", "Error: Sum of equilibrium intensities is zero or less when it must be strictly greater than zero. Emotion decay state is unchanged." },
            { "E-EDS_NO_ASSOC_EM_STATE", "Error: Emotion decay states must be associated with an emotion state and no emotion state is given. Cannot create a valid emotion decay state." },
            { "W-EDS_EQ_OUT_OF_BOUNDS", "Warning: Value for equilibrium intensity is out of bounds. Clamping to the range [{0}, {1}]." },

            // PAD3DPointT
            { "W-PADPNT_OUT_OF_BOUNDS", "Warning: Value for {0} dimension ({1}) is out of bounds. Clamping to the range [{2}, {3}]." },

            // EmotionT
            { "E-E_STATE_DNE", "Error: No emotion state exists at time {0}. No changes made." },
            { "E-E_TIME_OCCUPIED", "Error: Emotion state already exists at time {0}. Emotion is unchanged at time {1}." },
            { "W-E_STATE_DNE", "Warning: No emotion state exists at time {0}. Inserting new emotion state at {1}." },

            // EmotionFunLib
            { "W-EFL_BAD_TIME_ORDER", "Warning: Times given are ordered most recent to least recent. Reversing their order so that it is least recent to most recent." },

            // GoalT
            { "W-G_LT_ZERO", "Warning: Value for goal importance cannot be less than zero. Setting to zero." },

            // PlanT
            { "E-P_OUT_OF_BOUNDS", "Error: Requested plan state {0}, but plan only has {1} elements. Plan is unchanged." },
            { "E-P_TOO_FEW_EVENTS", "Error: There are not enough events to reach every plan state." },

            // AttentionT
            { "E-A_NULL_STEPSIZE", "Error: Null reference given for step size. No changes made." },
            { "W-A_NEGATIVE_ATTENTION", "Warning: Cannot allocate fewer than zero steps of attention. Setting to zero." },
            { "W-A_NEGATIVE_STEPSIZE", "Warning: Step size cannot be negative. Setting to zero." },
            { "W-A_OVERFLOW", "Warning: Attention steps overflow. Setting to last allowable value." },

            // SocialAttachmentT
            { "W-SA_NEGATIVE_LEVELSIZE", "Warning: Cannot move fewer than zero levels. Setting to zero." },
            { "W-SA_OVERFLOW", "Warning: Social attachment level overflow. Setting to last allowable value." },

            // Utility
            { "W-UTIL_PRINT_MSG", "Error: Unknown number of arguments to Print Message." },
            { "E-UTIL_UNKNOWN_MSG_CODE", "Error: Unknown message code." }
        };

        public static string GetMessage(string code)
        {
            if (messages.TryGetValue(code, out string value))
                return value;
            else
                return messages["E-UTIL_UNKNOWN_MSG_CODE"];
        }
    }
}
