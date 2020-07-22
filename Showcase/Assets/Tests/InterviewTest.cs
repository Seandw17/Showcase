using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class InterviewTest
    {
        [UnityTest]
        public IEnumerator TestInterviewSetUp()
        {
            Object.Instantiate(
                Resources.Load<GameObject>("Prefabs/QuestionSpace"));

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestFileRead()
        {
            //CSVLoader.LoadInPlayerQuestions();
            CSVLoader.LoadIntroText();
            CSVLoader.LoadOutroText();
            //CSVLoader.LoadTips();
            yield return null;
        }
    }
}
