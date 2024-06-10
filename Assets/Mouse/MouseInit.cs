using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInit : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D DefaultTexture;
    
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot= Vector2.zero;

    void Start()
    {
         Cursor.SetCursor(DefaultTexture,Vector2.zero,cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
