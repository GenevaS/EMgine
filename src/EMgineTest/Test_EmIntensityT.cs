/* =======================================================================================================
 * Copyright (c) 2023 G. M. Smith and J. Carette
 * Release under the BSD 3-Clause license
 * .NET Test SDK 17.4.1 \ NUnit 3 Framework 3.13.3 \ NUnit 3 Analyzers 3.5.0 \ 
 * NUnit 3 Test Adapter 4.3.1 \ Moq 4.18.3 \ Windows 10.0.19044
 * -------------------------------------------------------------------------------------------------------
 * Unit Tests: Emotion Intensity ADTs
 * =======================================================================================================
 * SUMMARY
 * -------------------------------------------------------------------------------------------------------
 * Total Tests: 18
 * Passing: 18
 * Failing: 0
 * Code Coverage: 
 * =======================================================================================================
 * Relies on: EmIntensityT, EmIntensityChgT (EmIntensityT.cs)
 * =======================================================================================================
 */

namespace EMgineTest
{
    [TestFixture]
    public class Test_EmIntensityT
    {
        readonly double tolerance = 0.000000000000001;
        const double minIntensity = 0.0;

        /* Construction Tests */
        [Test]
        public void ConstructorEmIntensityT_GivenPositiveNumber_Returns_NewEmIntensityTObjWithIntensityOfThatNumber()
        {
            EmIntensityT i = new(5);

            Assert.That(i.GremlinGetIntensity(), Is.EqualTo(5).Within(tolerance));
        }

        [Test]
        public void ConstructorEmIntensityT_GivenPositiveNumberWithDecimals_Returns_NewEmIntensityTObjWithIntensityOfThatNumber()
        {
            EmIntensityT i1 = new(5.8);
            EmIntensityT i2 = new(2.000000000000001);

            Assert.Multiple(() =>
            {
                Assert.That(i1.GremlinGetIntensity(), Is.EqualTo(5.8).Within(tolerance));
                Assert.That(i2.GremlinGetIntensity(), Is.EqualTo(2.000000000000001).Within(tolerance));
            });
        }

        [Test]
        public void ConstructorEmIntensityT_GivenZero_Returns_NewEmIntensityTObjWithIntensityOfZero()
        {
            EmIntensityT i = new(0);

            Assert.That(i.GremlinGetIntensity(), Is.EqualTo(0).Within(tolerance));
        }

        [Test]
        public void ConstructorEmIntensityT_GivenNegativeNumber_Returns_NewEmIntensityTObjWithIntensityOfZero_Throws_Warning()
        {
            EmIntensityT i = new(-5);

            Assert.That(i.GremlinGetIntensity(), Is.EqualTo(0).Within(tolerance)); // W-EIT_OUT_OF_BOUNDS
        }

        [Test]
        public void ConstructorEmIntensityChgT_GivenPositiveNumber_Returns_NewEmIntensityChgTObjWithIntensityOfThatNumber()
        {
            EmIntensityT.EmIntensityChgT i = new(5);

            Assert.That(i.GremlinGetDelta(), Is.EqualTo(5).Within(tolerance));
        }

        [Test]
        public void ConstructorEmIntensityChgT_GivenPositiveNumberWithDecimals_Returns_NewEmIntensityChgTObjWithIntensityOfThatNumber()
        {
            EmIntensityT.EmIntensityChgT i1 = new(5.8);
            EmIntensityT.EmIntensityChgT i2 = new(2.000000000000001);

            Assert.Multiple(() =>
            {
                Assert.That(i1.GremlinGetDelta(), Is.EqualTo(5.8).Within(tolerance));
                Assert.That(i2.GremlinGetDelta(), Is.EqualTo(2.000000000000001).Within(tolerance));
            });
        }

        [Test]
        public void ConstructorEmIntensityChgT_GivenZero_Returns_NewEmIntensityChgTObjWithIntensityOfThatNumber()
        {
            EmIntensityT.EmIntensityChgT i = new(0);
            
            Assert.That(i.GremlinGetDelta(), Is.EqualTo(0).Within(tolerance));
        }

        [Test]
        public void ConstructorEmIntensityChgT_GivenNegativeNumber_Returns_NewEmIntensityChgTObjWithIntensityOfThatNumber()
        {
            EmIntensityT.EmIntensityChgT i = new(-5);

            Assert.That(i.GremlinGetDelta(), Is.EqualTo(-5).Within(tolerance));
        }

        [Test]
        public void ConstructorEmIntensityChgT_GivenNegativeNumberWithDecimals_Returns_NewEmIntensityChgTObjWithIntensityOfThatNumber()
        {
            EmIntensityT.EmIntensityChgT i = new(-1.000000000000007);

            Assert.That(i.GremlinGetDelta(), Is.EqualTo(-1.000000000000007).Within(tolerance));
        }

        /* Compare Intensity Function Tests */
        [Test]
        public void CompareToIntensity_FirstIsLarger_Returns_One()
        {
            EmIntensityT i1 = new(5);
            EmIntensityT i2 = new(1);
            EmIntensityT i3 = new(2.000000000000001);

            Assert.Multiple(() =>
            {
                Assert.That(i1.CompareToIntensity(i2), Is.EqualTo(1));
                Assert.That(i1.CompareToIntensity(i3), Is.EqualTo(1));
            });
        }

        [Test]
        public void CompareToIntensity_FirstIsSmaller_Returns_NegativeOne()
        {
            EmIntensityT i1 = new(5);
            EmIntensityT i2 = new(1);
            EmIntensityT i3 = new(5.8);
            EmIntensityT i4 = new(2);
            EmIntensityT i5 = new(2.000000000000001);

            Assert.Multiple(() =>
            {
                Assert.That(i2.CompareToIntensity(i1), Is.EqualTo(-1));
                Assert.That(i1.CompareToIntensity(i3), Is.EqualTo(-1));
                Assert.That(i4.CompareToIntensity(i5), Is.EqualTo(-1));
            });
        }

        [Test]
        public void CompareToIntensity_EqualValues_Returns_Zero()
        {
            EmIntensityT i1 = new(5);
            EmIntensityT i2 = new(5);

            Assert.That(i1.CompareToIntensity(i2), Is.EqualTo(0));
        }

        [Test]
        public void EqualsMinIntensity_IntensityIsLarger_Returns_False()
        {
            EmIntensityT i1 = new(5);
            EmIntensityT i2 = new(2.3);
            EmIntensityT i3 = new(tolerance);

            Assert.Multiple(() =>
            {
                Assert.That(i1.EqualsMinIntensity(), Is.False);
                Assert.That(i2.EqualsMinIntensity(), Is.False);
                Assert.That(i3.EqualsMinIntensity(), Is.False);
            });
        }

        [Test]
        public void EqualsToMinIntensity_IntensityIsLarger_Returns_True()
        {
            EmIntensityT i = new(0);

            Assert.That(i.EqualsMinIntensity(), Is.True);
        }

        [Test]
        public void CompareToIntensityChg_FirstIsLarger_Returns_One()
        {
            EmIntensityT.EmIntensityChgT i1 = new(5);
            EmIntensityT.EmIntensityChgT i2 = new(0);
            EmIntensityT.EmIntensityChgT i3 = new(5.8);
            EmIntensityT.EmIntensityChgT i4 = new(2.1);
            EmIntensityT.EmIntensityChgT i5 = new(-1.7);

            Assert.Multiple(() =>
            {
                Assert.That(i1.CompareToIntensityChg(i2), Is.EqualTo(1));
                Assert.That(i3.CompareToIntensityChg(i4), Is.EqualTo(1));
                Assert.That(i3.CompareToIntensityChg(i5), Is.EqualTo(1));
            });
        }

        [Test]
        public void CompareToIntensityChg_FirstIsSmaller_Returns_NegativeOne()
        {
            EmIntensityT.EmIntensityChgT i1 = new(5);
            EmIntensityT.EmIntensityChgT i2 = new(0);
            EmIntensityT.EmIntensityChgT i3 = new(5.8);
            EmIntensityT.EmIntensityChgT i4 = new(-1.7);
            EmIntensityT.EmIntensityChgT i5 = new(5.000000000000001);

            Assert.Multiple(() =>
            {
                Assert.That(i2.CompareToIntensityChg(i1), Is.EqualTo(-1));
                Assert.That(i1.CompareToIntensityChg(i3), Is.EqualTo(-1));
                Assert.That(i4.CompareToIntensityChg(i3), Is.EqualTo(-1));
                Assert.That(i4.CompareToIntensityChg(i2), Is.EqualTo(-1));
                Assert.That(i1.CompareToIntensityChg(i5), Is.EqualTo(-1));
            });
        }

        [Test]
        public void CompareToIntensityChg_EqualValues_Returns_Zero()
        {
            EmIntensityT.EmIntensityChgT i1 = new(5);
            EmIntensityT.EmIntensityChgT i2 = new(5);

            Assert.That(i1.CompareToIntensityChg(i2), Is.EqualTo(0));
        }

        [Test]
        public void CompareToMinIntensity_ChgIsLarger_Returns_One()
        {
            EmIntensityT.EmIntensityChgT i1 = new(5);
            EmIntensityT.EmIntensityChgT i2 = new(5.8);
            EmIntensityT.EmIntensityChgT i3 = new(2.1);

            Assert.Multiple(() =>
            {
                Assert.That(i1.CompareToMinIntensity(), Is.EqualTo(1));
                Assert.That(i2.CompareToMinIntensity(), Is.EqualTo(1));
                Assert.That(i3.CompareToMinIntensity(), Is.EqualTo(1));
            });
        }

        [Test]
        public void CompareToMinIntensity_ChgIsSmaller_Returns_NegativeOne()
        {
            EmIntensityT.EmIntensityChgT i1 = new(-5);
            EmIntensityT.EmIntensityChgT i2 = new(-1.7);
            EmIntensityT.EmIntensityChgT i3 = new(-0.000000000000001);

            Assert.Multiple(() =>
            {
                Assert.That(i1.CompareToMinIntensity(), Is.EqualTo(-1));
                Assert.That(i2.CompareToMinIntensity(), Is.EqualTo(-1));
                Assert.That(i3.CompareToMinIntensity(), Is.EqualTo(-1));
            });
        }

        [Test]
        public void CompareToMinIntensity_ChgIsEqual_Returns_Zero()
        {
            EmIntensityT.EmIntensityChgT i = new(0);

            Assert.That(i.CompareToMinIntensity(), Is.EqualTo(0));
        }

        /* Arithmetic Function Tests */
        [Test]
        public void ScaleByValue_GivenNegativeValue_Returns_EmIntensityChgObjValueMultipliedByValueGiven()
        {
            EmIntensityT.EmIntensityChgT i1 = new(0);
            EmIntensityT.EmIntensityChgT i2 = new(-1.5);
            EmIntensityT.EmIntensityChgT i3 = new(2.8);

            double v = -1;

            Assert.Multiple(() =>
            {
                Assert.That(i1.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(0).Within(tolerance));
                Assert.That(i2.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(1.5).Within(tolerance));
                Assert.That(i3.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(-2.8).Within(tolerance));
            });
        }

        [Test]
        public void ScaleByValue_GivenPositiveValue_Returns_EmIntensityChgObjValueMultipliedByValueGiven()
        {
            EmIntensityT.EmIntensityChgT i1 = new(0);
            EmIntensityT.EmIntensityChgT i2 = new(-1.5);
            EmIntensityT.EmIntensityChgT i3 = new(2.8);

            double v = 4.5;

            Assert.Multiple(() =>
            {
                Assert.That(i1.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(0).Within(tolerance));
                Assert.That(i2.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(-6.75).Within(tolerance));
                Assert.That(i3.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(12.6).Within(tolerance));
            });
        }

        [Test]
        public void ScaleByValue_GivenZero_Returns_EmIntensityChgObjValueWithValueZero()
        {
            EmIntensityT.EmIntensityChgT i1 = new(0);
            EmIntensityT.EmIntensityChgT i2 = new(-1.5);
            EmIntensityT.EmIntensityChgT i3 = new(2.8);

            double v = 0;

            Assert.Multiple(() =>
            {
                Assert.That(i1.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(0).Within(tolerance));
                Assert.That(i2.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(0).Within(tolerance));
                Assert.That(i3.ScaleByValue(v).GremlinGetDelta(), Is.EqualTo(0).Within(tolerance));
            });
        }

        /* Normalize Intensity Tests */
        [Test]
        public void Normalize_GivenPositiveIntensity_Returns_NewEmIntensityTObj()
        {
            EmIntensityT i1 = new(5);
            EmIntensityT i2 = new(2.5);
            EmIntensityT i3 = new(1.6);

            Assert.Multiple(() =>
            {
                Assert.That(i1.Normalize(i2).GremlinGetIntensity(), Is.EqualTo(2).Within(tolerance));
                Assert.That(i1.Normalize(i3).GremlinGetIntensity(), Is.EqualTo(3.125).Within(tolerance));
                Assert.That(i2.Normalize(i1).GremlinGetIntensity(), Is.EqualTo(0.5).Within(tolerance));
                Assert.That(i2.Normalize(i3).GremlinGetIntensity(), Is.EqualTo(1.5625).Within(tolerance));
                Assert.That(i2.Normalize(i2).GremlinGetIntensity(), Is.EqualTo(1).Within(tolerance));
            });
        }

        [Test]
        public void Normalize_GivenZeroIntensity_Returns_Null()
        {
            EmIntensityT zero = new(0);
            EmIntensityT i = new(5);

            Assert.That(i.Normalize(zero), Is.Null); // E-EIT_INTENSITY_SCALE_ZERO
        }

        [Test]
        public void Normalize_IntensityToNormalizeIsZero_Returns_NewEmIntensityTObjWithValueZero()
        {
            EmIntensityT zero = new(0);
            EmIntensityT i = new(1.6);

            Assert.That(zero.Normalize(i).GremlinGetIntensity(), Is.EqualTo(0).Within(tolerance));
        }

        /* Convert to Value Tests */
        [Test]
        public void ToReal_Returns_EmIntensityTPrivateVariable_intensityValueAsDouble()
        {
            EmIntensityT i1 = new(0);
            EmIntensityT i2 = new(2.3);
            EmIntensityT i3 = new(5);

            Assert.Multiple(() =>
            {
                Assert.That(i1.ToReal(), Is.EqualTo(0).Within(tolerance));
                Assert.That(i2.ToReal(), Is.EqualTo(2.3).Within(tolerance));
                Assert.That(i3.ToReal(), Is.EqualTo(5).Within(tolerance));
            });
        }

        [Test]
        public void ToReal_Returns_EmIntensityChgTPrivateVariable_deltaValueAsDouble()
        {
            EmIntensityT.EmIntensityChgT i1 = new(0);
            EmIntensityT.EmIntensityChgT i2 = new(-2.3);
            EmIntensityT.EmIntensityChgT i3 = new(5);

            Assert.Multiple(() =>
            {
                Assert.That(i1.ToReal(), Is.EqualTo(0).Within(tolerance));
                Assert.That(i2.ToReal(), Is.EqualTo(-2.3).Within(tolerance));
                Assert.That(i3.ToReal(), Is.EqualTo(5).Within(tolerance));
            });
        }

        /* Convert to String Tests */
        [Test]
        public void GremlinIntensityToString_Returns_EmIntensityTPrivateVariable_intensityValueAsString()
        {
            EmIntensityT i1 = new(0);
            EmIntensityT i2 = new(2.3);
            EmIntensityT i3 = new(5);

            Assert.Multiple(() =>
            {
                Assert.That(i1.GremlinIntensityToString(), Is.EqualTo("0").Within(tolerance));
                Assert.That(i2.GremlinIntensityToString(), Is.EqualTo("2.3").Within(tolerance));
                Assert.That(i3.GremlinIntensityToString(), Is.EqualTo("5").Within(tolerance));
                Assert.That(EmIntensityT.GremlinMinIntensityToString(), Is.EqualTo("0").Within(tolerance));
            });
        }

        [Test]
        public void GremlinIntensityToString_Returns_EmIntensityChgTPrivateVariable_deltaValueAsString()
        {
            EmIntensityT.EmIntensityChgT i1 = new(0);
            EmIntensityT.EmIntensityChgT i2 = new(-2.3);
            EmIntensityT.EmIntensityChgT i3 = new(5);

            Assert.Multiple(() =>
            {
                Assert.That(i1.GremlinDeltaIntensityToString(), Is.EqualTo("0").Within(tolerance));
                Assert.That(i2.GremlinDeltaIntensityToString(), Is.EqualTo("-2.3").Within(tolerance));
                Assert.That(i3.GremlinDeltaIntensityToString(), Is.EqualTo("5").Within(tolerance));
            });
        }

        /* Copy Tests */
        [Test]
        public void Copy_EmIntensityTObjects_ReturnsDifferentObjsWithEqualValuesForPrivateVariables([Random(minIntensity, double.MaxValue, 5)] double i)
        {
            EmIntensityT I = new(i);
            EmIntensityT ICopy = I.Copy();

            Assert.Multiple(() =>
            {
                Assert.That(I, Is.Not.EqualTo(ICopy));
                Assert.That(I.GremlinGetIntensity(), Is.EqualTo(ICopy.GremlinGetIntensity()));
            });
        }

        [Test]
        public void Copy_EmIntensityChgTObjects_ReturnsDifferentObjsWithEqualValuesForPrivateVariables([Random(double.MinValue, double.MaxValue, 5)] double chg)
        {
            EmIntensityT.EmIntensityChgT Chg = new(chg);
            EmIntensityT.EmIntensityChgT chgCopy = Chg.Copy();

            Assert.Multiple(() =>
            {
                Assert.That(Chg, Is.Not.EqualTo(chgCopy));
                Assert.That(Chg.GremlinGetDelta(), Is.EqualTo(chgCopy.GremlinGetDelta()));
            });
        }

        /* Update Intensity Tests */
        [Test]
        public void UpdateWithChg_GivenPositiveEmIntensityChgT_Returns_NewEmIntensityTObjWithUpdatedPrivate_intensityValue()
        {
            EmIntensityT i1 = new(2);
            EmIntensityT i2 = new(40.89);

            EmIntensityT.EmIntensityChgT di = new(1);

            EmIntensityT updateI1 = i1.UpdateWithChg(di);
            EmIntensityT updateI2 = i2.UpdateWithChg(di);

            Assert.Multiple(() =>
            {
                Assert.That(updateI1.GremlinGetIntensity(),
                            Is.EqualTo(0.1 * Math.Log2(Math.Pow(2.0, 20) + Math.Pow(2.0, 10))).Within(tolerance));
                Assert.That(updateI1, Is.Not.EqualTo(i1));

                Assert.That(updateI2.GremlinGetIntensity(),
                            Is.EqualTo(0.1 * Math.Log2(Math.Pow(2.0, 408.9) + Math.Pow(2.0, 10))).Within(tolerance));
                Assert.That(updateI2, Is.Not.EqualTo(i2));
            });
        
        }

        [Test]
        public void UpdateWithChg_GivenNegativeEmIntensityChgT_Returns_NewEmIntensityTObjWithUpdatedPrivate_intensityValue()
        {
            EmIntensityT i1 = new(2);
            EmIntensityT i2 = new(40.89);

            EmIntensityT.EmIntensityChgT di = new(-1);

            EmIntensityT updateI1 = i1.UpdateWithChg(di);
            EmIntensityT updateI2 = i2.UpdateWithChg(di);
            Assert.Multiple(() =>
            {
                Assert.That(updateI1.GremlinGetIntensity(),
                    Is.EqualTo(0.1 * Math.Log2(Math.Pow(2.0, 20) - Math.Pow(2.0, 10))).Within(tolerance));
                Assert.That(updateI1, Is.Not.EqualTo(i1));
                Assert.That(updateI2.GremlinGetIntensity(),
                    Is.EqualTo(0.1 * Math.Log2(Math.Pow(2.0, 408.9) - Math.Pow(2.0, 10))).Within(tolerance));
                Assert.That(updateI2, Is.Not.EqualTo(i2));
            });
        }

        [Test]
        public void UpdateIntensityWithZeroChg_Returns_NewEmIntensityTWithEqualValue()
        {
            EmIntensityT i1 = new(2);
            EmIntensityT i2 = new(40.89);

            EmIntensityT.EmIntensityChgT di = new(0);

            EmIntensityT updateI1 = i1.UpdateWithChg(di);
            EmIntensityT updateI2 = i2.UpdateWithChg(di);

            Assert.Multiple(() =>
            {
                Assert.That(updateI1.GremlinGetIntensity(),
                    Is.EqualTo(i1.GremlinGetIntensity()).Within(tolerance));
                Assert.That(updateI1, Is.Not.EqualTo(i1));

                Assert.That(updateI2.GremlinGetIntensity(),
                    Is.EqualTo(i2.GremlinGetIntensity()).Within(tolerance));
                Assert.That(updateI2, Is.Not.EqualTo(i2));
            });
        }

        [Test]
        public void UpdateIntensityOfValueZeroWithPositiveChg_Returns_NewEmIntensityTWithUpdatedValue()
        {
            EmIntensityT i = new(0);

            EmIntensityT.EmIntensityChgT di = new(1);

            EmIntensityT updateI = i.UpdateWithChg(di);

            Assert.Multiple(() =>
            {
                Assert.That(updateI.GremlinGetIntensity(),
                    Is.EqualTo(0.1 * Math.Log2(1 + Math.Pow(2.0, 10))).Within(tolerance));
                Assert.That(updateI, Is.Not.EqualTo(i));
            });
        }

        /* Additional Tests */
        [Test]
        public void MinIntensityGremlin_Returns_MiniumumIntensityAsDouble()
        {
            Assert.That(EmIntensityT.GremlinGetMinIntensity(), Is.EqualTo(minIntensity).Within(tolerance));
        }
    }
}