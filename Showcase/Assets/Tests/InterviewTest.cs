using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace Tests
{
    public class InterviewTest
    {
        [UnityTest]
        public IEnumerator Test_Interview_SetUp()
        {
            Assert.IsNotNull(Object.Instantiate(
                Resources.Load<GameObject>("Prefabs/QuestionSpace")));

            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_File_Read()
        {
            Assert.IsNotNull(CSVLoader.LoadQuestionData("IQuestions"));

            System.Collections.Generic.List<PlayerQuestion> test;
            CSVLoader.LoadInPlayerQuestions("PQuestions", out test);
            Assert.IsNotNull(test);

            Assert.IsNotNull(CSVLoader.LoadIntroText());
            Assert.IsNotNull(CSVLoader.LoadOutroText());

            System.Collections.Generic.Dictionary<e_tipCategories, string> temp;
            CSVLoader.LoadTips(out temp);
            Assert.IsNotNull(temp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_Page_Creation()
        {
            yield return null;
        }
    }
}
