using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ScoreCard : MonoBehaviour
{
    List<s_playerResponse> m_responses;
    int m_currentPage;
    List<Page> m_pages;

    PageMoveObject m_rightButton, m_leftButton;

    void Start()
    {
        m_responses = ConversationStore.ReturnFinalChosenResults();
        m_pages = new List<Page>();
        PageMoveObject.Register(this);
        StartCoroutine(CalculateResult());
    }

    /// <summary>
    /// Load in the pages and calculate the result
    /// </summary>
    /// <returns></returns>
    IEnumerator CalculateResult()
    {
        m_leftButton = Instantiate(Resources.Load<GameObject>
            ("Prefabs/PageMoveButton")).GetComponent<PageMoveObject>();
        m_leftButton.Set(PageMoveObject.e_direction.LEFT);
        m_leftButton.SetInteractable(false);
        m_rightButton = Instantiate(Resources.Load<GameObject>
            ("Prefabs/PageMoveButton")).GetComponent<PageMoveObject>();
        m_rightButton.Set(PageMoveObject.e_direction.RIGHT);

        yield return null;

        // Load in pages
        s_playerResponse[] TempResponses = new s_playerResponse[3];
        int externalIndexer = 0;
        int finalScore = OutfitManager.GetOutfitScore();

        foreach (s_playerResponse response in m_responses)
        {
            // add response to the array
            TempResponses[externalIndexer] = response;
            externalIndexer++;
            // if we've hit the limit, make a page
            if (externalIndexer == 3)
            {
                GenerateAnswerPage(TempResponses);
                TempResponses = new s_playerResponse[3];
                externalIndexer = 0;
            }
            // add to final score
            finalScore += (int)response.playerResponse.rating;
            if(Equals(response, m_responses[m_responses.Count - 1])
                && externalIndexer < 3 && externalIndexer != 0)
            {
                GenerateAnswerPage(TempResponses);
            }

            yield return null;
        }

        GenerateResultPage(finalScore);

        // creating the tips
        List<string> tips = TipParser.GenerateTips();
        string[] tipsToPass = new string[3];
        externalIndexer = 0;
        foreach(string tip in tips)
        {
            tipsToPass[externalIndexer] = tip;
            externalIndexer++;

            if (externalIndexer == 3)
            {
                GenerateTipsPage(tipsToPass);
                tipsToPass = new string[3];
                externalIndexer = 0;
            }

            // if any fall out of the loop at the end
            if (Equals(tip, tips[tips.Count - 1]) && externalIndexer < 3
                && externalIndexer != 0){
                GenerateTipsPage(tipsToPass);
            }

            yield return null;
        }

        m_responses.Clear();

        // setting positions and rotations for left and right buttons
        m_rightButton.transform.parent = m_leftButton.transform.parent = transform;
        m_rightButton.transform.localPosition = new Vector3(-0.4f, -0.25f, 0);
        m_leftButton.transform.localPosition = new Vector3(0.4f, -0.25f, 0);

        yield return null;
    }

    /// <summary>
    /// Generate the final result prefab
    /// </summary>
    /// <param name="_finalScore">the score of the player</param>
    void GenerateResultPage(int _finalScore)
    {
        FinalResult finalResultPage = Instantiate(Resources.Load<GameObject>
            ("Prefabs/FinalResultPage").GetComponent<FinalResult>());
        finalResultPage.SetValue(_finalScore, m_responses.Count);
        finalResultPage.gameObject.transform.parent = transform;
        finalResultPage.gameObject.transform.localPosition = Vector3.zero;
        finalResultPage.gameObject.SetActive(true);
        m_pages.Insert(0, finalResultPage);
    }

    /// <summary>
    /// Create an answer page
    /// </summary>
    /// <param name="_reponses"> the player reponses for this page</param>
    void GenerateAnswerPage(s_playerResponse[] _reponses)
    {
        Debug.Assert(!_reponses[0].question.Equals(""),
            "A null page has been created");
        AnswerPage answerPage = Instantiate(Resources.Load<GameObject>
            ("Prefabs/AnswerPage").GetComponent<AnswerPage>());
        answerPage.SetValue(_reponses);
        answerPage.gameObject.transform.parent = transform;
        answerPage.gameObject.transform.localPosition = Vector3.zero;
        answerPage.gameObject.SetActive(false);
        m_pages.Add(answerPage);
    }

    void GenerateTipsPage(string[] _tips)
    {
        Debug.Assert(!_tips[0].Equals(""),
            "A null page has been created");
        TipsPages tipsPage = Instantiate(Resources.Load<GameObject>
            ("Prefabs/TipsPage").GetComponent<TipsPages>());
        tipsPage.SetValue(_tips);
        tipsPage.gameObject.transform.parent = transform;
        tipsPage.gameObject.transform.localPosition = Vector3.zero;
        tipsPage.gameObject.SetActive(false);
        m_pages.Add(tipsPage);
    }

    /// <summary>
    /// Move currently disaplyed page left
    /// </summary>
    public void GoPageLeft()
    {
        Debug.Log("Going page left");
        m_pages[m_currentPage].gameObject.SetActive(false);
        m_currentPage--;
        m_pages[m_currentPage].gameObject.SetActive(true);
        CheckPages();
    }

    /// <summary>
    /// Move Currently displayed page right
    /// </summary>
    public void GoPageRight()
    {
        Debug.Log("Going page right");
        m_pages[m_currentPage].gameObject.SetActive(false);
        m_currentPage++;
        m_pages[m_currentPage].gameObject.SetActive(true);
        CheckPages();
    }

    /// <summary>
    /// check the current status of the pages and adjust accordingly
    /// </summary>
    void CheckPages()
    {
        if (m_currentPage == 0)
        {
            m_leftButton.SetInteractable(false);
        }
        else if (m_currentPage == m_pages.Count - 1)
        {
            m_rightButton.SetInteractable(false);
        }
        else
        {
            m_rightButton.SetInteractable(true);
            m_leftButton.SetInteractable(true);
        }
    }

    public void TurnOn()
    {
        m_rightButton.gameObject.SetActive(true);
        m_leftButton.gameObject.SetActive(true);
    }
}
