using UnityEngine.UI;
using static GameManagerScript;
using UnityEngine;

public class MagazineInteract : InteractableObjectBase
{
    [SerializeField] GameObject m_panel;

    Vector3 m_startingScalers;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        m_startingScalers = m_panel.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(Button m_exit)
    {
        m_exit.onClick.AddListener(ResetMagazineView);
    }

    public override void Interact()
    {
        m_panel.transform.localScale = m_startingScalers;
        //SetCurrentHUD(ReturnPanel(e_PanelTypes.MAGAZINE));
        SetNewHUD(m_panel);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SpotEffects/paper_magazine_collect", GetComponent<Transform>().position);
        m_playerscript.SetCanCameraMove(false);
        GetCursor().EnableCursor();
        m_playerscript.SetCanInteract(false);
        //FMODUnity.RuntimeManager.PlayOneShot("event:/SpotEffects/paper_magazine_collect"); -play sound when player collect paper magazine

    }

    public void ResetMagazineView()
    {
        //SetCurrentHUD(ReturnPanel(e_PanelTypes.PLAYER));
        Debug.Log(" exit pls");
        SetHUDBack();
        m_playerscript.SetCanCameraMove(true);
        GetCursor().DisableCursor();
        m_playerscript.SetCanInteract(true);
    }
}
