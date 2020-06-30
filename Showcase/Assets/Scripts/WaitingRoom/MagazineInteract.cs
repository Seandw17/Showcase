using static GameManagerScript;

public class MagazineInteract : InteractableObjectBase
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        SetCurrentHUD(
            ReturnPanel(e_PanelTypes.MAGAZINE));
        m_playerscript.SetCanCameraMove(false);
        GetCursor().EnableCursor();
        m_playerscript.SetCanInteract(false);
    }

    public void ResetMagazineView()
    {
        SetCurrentHUD(
            ReturnPanel(e_PanelTypes.PLAYER));
        m_playerscript.SetCanCameraMove(true);
        GetCursor().DisableCursor();
        m_playerscript.SetCanInteract(true);
    }
}
