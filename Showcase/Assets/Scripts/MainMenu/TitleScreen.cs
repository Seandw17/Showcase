using UnityEngine;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] KeyCode m_startKey;

    [SerializeField] TextMeshPro _Starttext;

    // Start is called before the first frame update
    void Start()
    {
        _Starttext.SetText("Press " + m_startKey.ToString() + "To start");

        //TODO load in positions
    }

    // Update is called once per frame
    void Update()
    {
        // TODO LERP

        // TODO ONCE HERE GO TO THE NEXT

        // TODO ONCE DONE FOR ALL GIVEN AREAS, GO TO NEXT
    }
}
