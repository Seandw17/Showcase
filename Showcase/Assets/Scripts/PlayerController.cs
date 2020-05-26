using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float m_playerSpeed = 10.0f;

    //Camera Variables
    Vector2 m_mouseLook;
    Vector2 m_smoothV;
    float m_mouseSensitivity = 5.0f;
    float m_smoothing = 2.0f;
    [SerializeField]
    Camera m_camera;

    //Interactable Object - ref
    public List<InteractableObjectBase> ig_interactable;

    //Bool to handle player movement and camera
    public bool m_canmove = true;

    //Bool to check if the player is able to interact
    bool m_caninteract = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_camera = FindObjectOfType<Camera>(); //Find the camera which is a child of the Player

        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (m_canmove == true)
        {
            PlayerMovement();
        }
        CameraMovement();
        OnInteract();
    }

    //Seting up the player movements
    void PlayerMovement()
    {
        float m_Translation = Input.GetAxis("Vertical") * m_playerSpeed;
        float m_Straffe = Input.GetAxis("Horizontal") * m_playerSpeed;
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
        m_mouseLook.y = Mathf.Clamp(m_mouseLook.y, -90.0f, 90.0f);

        m_camera.transform.localRotation = Quaternion.AngleAxis(-m_mouseLook.y, Vector3.right);
        this.transform.localRotation = Quaternion.AngleAxis(m_mouseLook.x, this.transform.up);
    }

    //Setting up the interaction with left mouse button click
    void OnInteract()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (m_caninteract == true)
            {
                RaycastHit m_hit;
                Ray m_ray = m_camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(m_ray, out m_hit))
                {
                    Transform m_objectHit = m_hit.transform;
                    //Run through all interactable objects within the game
                    for (int i = 0; i < ig_interactable.Count; i++)
                    {

                        if (m_objectHit.gameObject == ig_interactable[i].gameObject)
                        {
                            //Call the specific object interact function
                            ig_interactable[i].Interact();
                        }
                    }
                }
            }
        }
    }

   

    public bool SetCanInteract(bool _canInteractbool)
    {
        Debug.Log("CanInteractcalled");
        m_caninteract = _canInteractbool;
        return m_caninteract;
        
    }
}
