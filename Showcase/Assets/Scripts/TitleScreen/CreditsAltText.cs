using UnityEngine;
using UnityEngine.UI;

public class CreditsAltText : MonoBehaviour
{
    [SerializeField] GameObject m_bert;
    float timer = 3.0f;

    // Update is called once per frame
    void Update()
    {
        if (!m_bert.activeSelf)
        {
            if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.O)
            && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.A)
            && Input.GetKey(KeyCode.LeftControl))
            {
                if (timer > 0)
                {
                    Debug.Log(timer);
                    timer -= 0.05f;
                }
                else
                {
                    Debug.Log("Secret credits, big oof");
                        GetComponent<Text>().text = "BIG OOF\n Sean Lauder " +
                        "- permanent caffeine high \n" +
                        "Sean Rosa -unwilling participant \n" +
                        "Sean Bobbert420UpInThis - Destroyer of Popchips \n" +
                        "Sean Fairweather -Straight up gangsta \n " +
                        "Sean - Sean Docherty - The Lord \n " +
                        "Sean - Sean Walker - known head \n" +
                        "Sean Sienkiewicz - DJ, Plasterer, Carpenter, " +
                        "after hours Sound Designer \n" +
                        "Sean Greer \n" +
                        "Sean Quénel \n " +
                        "Sean Lampard-France \n" +
                        "Sean Marshall";
                    m_bert.SetActive(true);
                }
            }
        }   
    }
}
