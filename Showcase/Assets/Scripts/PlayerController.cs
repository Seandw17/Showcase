using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float m_playerSpeed = 5.0f;

    //Camera Variables
    Vector2 m_mouseLook;
    Vector2 m_smoothV;
    Vector3 m_campos;
    float m_mouseSensitivity = 2.0f;
    float m_smoothing = 2.0f;
    public float m_Translation;
    public float m_Straffe;


    public Camera m_camera;

    //Interactable Object - ref
    //public List<InteractableObjectBase> ig_interactable;

    //Bool to handle player movement
    bool m_canmove = true;

    //Bool to hangle player camera movement
    bool m_cancameramove = true;

    //Bool to check if the player is able to interact
    bool m_caninteract = true;

    //Bool to check if the player is in the interview
    bool m_isininterview = false;

    // the currently selected interactible
    InteractableObjectBase m_currentlySelected;

    int m_layerMask;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //m_camera = FindObjectOfType<Camera>(); //Find the camera which is a child of the Player 
        m_camera = this.gameObject.GetComponentInChildren<Camera>();
        m_campos = m_camera.transform.position;
        m_layerMask = LayerMask.GetMask("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused())
        {
            if (m_canmove == true)
            {
                PlayerMovement();
            }
            CameraMovement();
            OnInteract(); 
        }
    }

    //Seting up the player movements
    void PlayerMovement()
    {
        m_Translation = Input.GetAxis("Vertical") * m_playerSpeed;
        m_Straffe = Input.GetAxis("Horizontal") * m_playerSpeed;
        m_Translation *= Time.deltaTime;
        m_Straffe *= Time.deltaTime;

        transform.Translate(m_Straffe, 0, m_Translation);
    }

    //Setting up the camera movement to move with the mouse
    void CameraMovement()
    {
        var m_mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        m_mouseDirection = Vector2.Scale(m_mouseDirection, new Vector2(m_mouseSensitivity * m_smoothing, m_mouseSensitivity * m_smoothing));
        m_smoothV.x = Mathf.Lerp(m_smoothV.x, m_mouseDirection.x, 1f / m_smoothing);
        m_smoothV.y = Mathf.Lerp(m_smoothV.y, m_mouseDirection.y, 1f / m_smoothing);
        m_mouseLook += m_smoothV;
        if (m_isininterview == true)
        {
            m_mouseLook.x = Mathf.Clamp(m_mouseLook.x, -90.0f, 90.0f);
            m_mouseLook.y = Mathf.Clamp(m_mouseLook.y, -45.0f, 90.0f);
           
        }
        else
        {
            m_mouseLook.y = Mathf.Clamp(m_mouseLook.y, -90.0f, 90.0f);
        }
        if (m_cancameramove == true)
        {
            m_camera.transform.localRotation = Quaternion.AngleAxis(-m_mouseLook.y, Vector3.right);
            this.transform.localRotation = Quaternion.AngleAxis(m_mouseLook.x, this.transform.up);
        }
        else
        {
            this.transform.localRotation = Quaternion.AngleAxis(m_mouseLook.x, Vector3.zero);
        }
        
    }

    //Setting up the interaction with left mouse button click
    void OnInteract()
    {
        if (m_caninteract == true)
        {
            RaycastHit m_hit;
            Ray m_ray = m_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(m_ray, out m_hit, 10.0f, m_layerMask))
            {
                InteractableObjectBase hitObject = m_hit.transform.gameObject
                    .GetComponent<InteractableObjectBase>();

                if (m_currentlySelected == null)
                {
                    m_currentlySelected = hitObject;
                }
                else if (m_currentlySelected != hitObject)
                {
                    m_currentlySelected.GetObjectOutline().enabled = false;
                    m_currentlySelected = hitObject;
                }

                hitObject.GetObjectOutline().enabled = true;
                m_currentlySelected = hitObject;

                if (Input.GetMouseButtonDown(0))
                {
                    hitObject.GetComponent<InteractableObjectBase>().Interact();
                }
            }
            else
            {
                if (m_currentlySelected)
                {
                    m_currentlySelected.GetObjectOutline().enabled = false;
                    m_currentlySelected = null;
                }
            }

                /*
                Transform m_objectHit = m_hit.transform;
                //Run through all interactable objects within the game
                for (int i = 0; i < ig_interactable.Count; i++)
                {
                    if (m_objectHit.gameObject == ig_interactable[i].gameObject)
                    {
                        m_currentlySelected = ig_interactable[i];
                        if (ig_interactable[i].GetShouldGlow())
                        {
                           // Debug.Log("Should be glowing fam");
                            ig_interactable[i].GetObjectOutline().enabled = true;
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            //Call the specific object interact function
                            ig_interactable[i].Interact();
                            ig_interactable[i].GetObjectOutline()
                                .enabled = false;
                        }
                    }
                }
                */

            /*
            // if we hit nothing, remove the current glow
            else
            {
                if (m_currentlySelected)
                {
                    m_currentlySelected.GetObjectOutline().enabled = false;
                }
            }*/
        }
    }

   

    public bool SetCanInteract(bool _canInteractbool)
    {
        Debug.Log("CanInteractcalled");
        m_caninteract = _canInteractbool;
        return m_caninteract;
        
    }

    public bool SetCanCameraMove(bool _canCamerabool)
    {
        m_cancameramove = _canCamerabool;
        return m_cancameramove;
    }

    public bool SetCanPlayerMove(bool _canPlayerbool)
    {
        m_canmove = _canPlayerbool;
        return m_canmove;
    }

    public bool SetIsInInterview(bool _isininterview)
    {
        m_isininterview = _isininterview;
        return m_isininterview;
    }
}
