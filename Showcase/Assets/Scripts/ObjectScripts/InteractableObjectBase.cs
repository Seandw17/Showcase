using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectBase : MonoBehaviour
{
    GameObject ig_GameManager;

    protected GameManagerScript m_gmscript;
    // Start is called before the first frame update
    public void Start()
    {
        ig_GameManager = GameObject.Find("GameManager");
        m_gmscript = ig_GameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        //TODO - Add code for whatever you want the user to do once they click on an object
        m_gmscript.ig_PlayerPanel.SetActive(false);
        
    }
}
