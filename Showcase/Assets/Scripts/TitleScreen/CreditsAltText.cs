using UnityEngine;
using UnityEngine.UI;

public class CreditsAltText : MonoBehaviour
{
    [SerializeField] GameObject m_bert;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.O)
            && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.A))
        {
            if (!m_bert.activeSelf)
            {
                // TODO alt credit text
                Debug.Log("Secret credits, big oof");
                GetComponent<Text>().text = "BIG OOF";
                m_bert.SetActive(true);
            }
        }   
    }
}
