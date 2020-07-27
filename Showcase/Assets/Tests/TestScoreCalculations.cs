using NUnit.Framework;

namespace Tests
{
    public class TestScoreCalculations
    {
        [Test]
        public void TestCalculateScore()
        {
            Assert.GreaterOrEqual(FinalResult.CalculatePassingGrade(5, 0.6f), 15);
            Assert.GreaterOrEqual(FinalResult.CalculatePassingGrade(10, 0.9f), 45);
            Assert.GreaterOrEqual(FinalResult.CalculatePassingGrade(25, 0.6f), 75);
        }

        [Test]
        public void TestPassCalculation()
        {
            Assert.IsFalse(FinalResult.CalculatePass(
                FinalResult.CalculatePassingGrade(5, 0.6f), 12));
            Assert.IsTrue(FinalResult.CalculatePass(
                FinalResult.CalculatePassingGrade(5, 0.6f), 17));
            Assert.IsTrue(FinalResult.CalculatePass(
                FinalResult.CalculatePassingGrade(5, 0.6f), 17.5f));
            Assert.IsFalse(FinalResult.CalculatePass(
                FinalResult.CalculatePassingGrade(5, 0.6f), 0.1f));

        }
    }
}
