using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineController : MonoBehaviour
{

    [SerializeField] private List<GameObject> m_availablePages;
    [SerializeField] private GameObject ig_nextButton;
    [SerializeField] private GameObject ig_previousButton;

    private int m_currentPage;
    // Start is called before the first frame update
    void Start()
    {
        m_currentPage = 0;
        UpdatePage();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAvailableButtons();
    }


    /// <summary>
    /// Function to check the current page and hide and show next and previous button
    /// </summary>
    void CheckAvailableButtons()
    {
        if(m_currentPage>=0 && m_currentPage< m_availablePages.Count-1)
        {
            ig_nextButton.SetActive(true);
        }
        else
        {
            ig_nextButton.SetActive(false);
        }

        if(m_currentPage>0 && m_currentPage <= m_availablePages.Count-1)
        {
            ig_previousButton.SetActive(true);
        }
        else
        {
            ig_previousButton.SetActive(false);
        }
    }


    /// <summary>
    /// change the current page
    /// </summary>
    void UpdatePage()
    {
        for(int i = 0; i<m_availablePages.Count;i++)
        {
            m_availablePages[i].SetActive(false);
        }

        m_availablePages[m_currentPage].SetActive(true);
    }

    public void NextPageButton()
    {
        m_currentPage += 1;
        UpdatePage();
    }

    public void PreviousPageButton()
    {
        m_currentPage -= 1;
        UpdatePage();
    }


  
}
