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
        public IEnumerator Test_Final_Result_Page_Creation()
        {
            FinalResult page = Object.Instantiate
                (Resources.Load<GameObject>("Prefabs/FinalResultPage"))
                .GetComponent<FinalResult>();

            Assert.IsNotNull(page);

            page.SetValue(5, 10);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_Tip_Page_Creation()
        {
            TipsPages page = Object.Instantiate
                (Resources.Load<GameObject>("Prefabs/TipsPage"))
                .GetComponent<TipsPages>();

            Assert.IsNotNull(page);

            string[] testData = { "Test", "Test", "Test" };

            page.SetValue(testData);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_Answer_Page_Creation()
        {
            AnswerPage page = Object.Instantiate
                (Resources.Load<GameObject>("Prefabs/AnswerPage"))
                .GetComponent<AnswerPage>();

            Assert.IsNotNull(page);

            s_playerResponse[] testData = new s_playerResponse[3];

            for (int index = 0; index < testData.Length; index++)
            {
                testData[index] = new s_playerResponse
                {
                    question = "Test",
                    playerResponse = new Questionresponse
                    {
                        rating = e_rating.AWFUL,
                        response = "Test",
                        unlockCriteria = e_unlockFlag.FIRST,
                        tip = e_tipCategories.CRITICISM
                    }
                };
            }

            page.SetValue(testData);

            yield return null;
        }
    }
}
