using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    //public Texture2D cursorTexture;
    //private CursorMode cursorMode = CursorMode.Auto;
    //private Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // shows the cursor on screen
    // code for a custom cursor is still useful incase we change it in the future so im leaving it commented for now
    public void EnableCursor()
    {
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // removes the cursor on screen
    public void DisableCursor()
    {
        //Cursor.SetCursor(null, Vector2.zero, cursorMode);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
