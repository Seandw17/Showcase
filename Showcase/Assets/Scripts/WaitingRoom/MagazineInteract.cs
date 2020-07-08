using UnityEngine.UI;
using static GameManagerScript;
using UnityEngine;

public class MagazineInteract : InteractableObjectBase
{
    [SerializeField] GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
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
        //SetCurrentHUD(ReturnPanel(e_PanelTypes.MAGAZINE));
        SetNewHUD(panel);
        m_playerscript.SetCanCameraMove(false);
        GetCursor().EnableCursor();
        m_playerscript.SetCanInteract(false);
    }

    public void ResetMagazineView()
    {
        //SetCurrentHUD(ReturnPanel(e_PanelTypes.PLAYER));
        SetHUDBack();
        m_playerscript.SetCanCameraMove(true);
        GetCursor().DisableCursor();
        m_playerscript.SetCanInteract(true);
    }
}
