using UnityEngine;

public class InteractableObjectBase : MonoBehaviour
{
    GameObject ig_GameManager;
    GameObject ig_Player;

    protected GameManagerScript m_gmscript;
    protected PlayerController m_playerscript;

    Outline m_outline;

    // Start is called before the first frame update
    public void Start()
    {
        /*
        ig_GameManager = GameObject.Find("GameManager");
        m_gmscript = ig_GameManager.GetComponent<GameManagerScript>();

        ig_Player = GameObject.Find("Player");
        m_playerscript = ig_Player.GetComponent<PlayerController>();
        */

        //Proposed change
        m_gmscript = FindObjectOfType<GameManagerScript>();
        m_playerscript = FindObjectOfType<PlayerController>();

        m_outline = gameObject.transform.root.
            gameObject.AddComponent<Outline>();
        m_outline.OutlineColor = Color.blue;
        m_outline.OutlineWidth = 10.0f;
        m_outline.enabled = false;

        AddToList();
    }

    void AddToList()
    {
        m_playerscript.ig_interactable.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        //TODO - Add code for whatever you want the user to do once they click on an object   
    }

    /// <summary>
    /// Function to return the outline component
    /// </summary>
    /// <returns> the outline component </returns>
    public Outline GetObjectOutline() { return m_outline; }
}
