using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//TODO implement waiting text

public class ScoreCard : MonoBehaviour
{
    List<s_playerResponse> m_responses;
    int m_currentPage;
    List<Page> m_pages;

    /// <summary>
    /// GameObject of waiting text
    /// </summary>
    [SerializeField] GameObject m_watitingText;

    PageMoveObject m_lftButton, m_rgtButton;

    void Start()
    {
        //m_responses = ConversationStore.ReturnFinalChosenResults();
        m_responses = ConversationStore.ReturnTestData();
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
        //m_watitingText.SetActive(true);

        m_rgtButton = Instantiate(Resources.Load<GameObject>
            ("Prefabs/PageMoveButton")).GetComponent<PageMoveObject>();
        m_rgtButton.Set(PageMoveObject.e_direction.RIGHT);
        m_lftButton = Instantiate(Resources.Load<GameObject>
            ("Prefabs/PageMoveButton")).GetComponent<PageMoveObject>();
        m_lftButton.Set(PageMoveObject.e_direction.LEFT);

        m_lftButton.transform.position = new Vector3(-0.9f, 0, 0);
        m_rgtButton.transform.position = new Vector3(0.9f, 0, 0);

        // Load in pages
        s_playerResponse[] TempResponses = new s_playerResponse[3];
        int externalIndexer = 0;
        int finalScore = 0;

        foreach (s_playerResponse response in m_responses)
        {
            // add response to the array
            TempResponses[externalIndexer] = response;
            externalIndexer++;
            // if we've hit the limit, make a page
            if (externalIndexer == 2)
            {
                GenerateAnswerPage(TempResponses);
                TempResponses = new s_playerResponse[3];
                externalIndexer = 0;
            }
            // add to final score
            finalScore += (int)response.playerResponse.rating;
            if(Equals(response, m_responses[m_responses.Count - 1])
                && externalIndexer != 2)
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
            if (Equals(tip, tips[tips.Count - 1]) && externalIndexer != 2){
                GenerateTipsPage(tipsToPass);
            }

            yield return null;
        }

        m_responses.Clear();
        m_lftButton.transform.parent = m_rgtButton.transform.parent = transform;
        //Destroy(m_watitingText);
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
        //m_pages.Add(finalResultPage);
        m_pages.Insert(0, finalResultPage);
    }

    /// <summary>
    /// Create an answer page
    /// </summary>
    /// <param name="_reponses"> the player reponses for this page</param>
    void GenerateAnswerPage(s_playerResponse[] _reponses)
    {
        AnswerPage answerPage = Instantiate(Resources.Load<GameObject>
            ("Prefabs/AnswerPage").GetComponent<AnswerPage>());
        answerPage.SetValue(_reponses);
        answerPage.gameObject.transform.parent = transform;
        answerPage.gameObject.SetActive(false);
        m_pages.Add(answerPage);
    }

    void GenerateTipsPage(string[] _tips)
    {
        TipsPages tipsPage = Instantiate(Resources.Load<GameObject>
            ("Prefabs/TipsPage").GetComponent<TipsPages>());
        tipsPage.SetValue(_tips);
        tipsPage.gameObject.transform.parent = transform;
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
            m_lftButton.SetInteractable(false);
        }
        else if (m_currentPage == m_pages.Count - 1)
        {
            m_rgtButton.SetInteractable(false);
        }
        else
        {
            m_lftButton.SetInteractable(true);
            m_rgtButton.SetInteractable(true);
        }
    }
}
