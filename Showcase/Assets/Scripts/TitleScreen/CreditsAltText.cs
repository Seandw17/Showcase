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
                Debug.Log("Secret credits, big oof");
                GetComponent<Text>().text = "BIG OOF\n Rita Rosa" +
                    " - unwilling participant " +
                    "\n Alec Lauder - permanent caffeine high\n " +
                    "Bobbert420UpInThis - Destroyer of Popchips\n Big Lee - " +
                    "Straight up gangsta \n Sean Docherty - The Lord \n " +
                    "Lord Aswell - known head";
                m_bert.SetActive(true);
            }
        }   
    }
}
