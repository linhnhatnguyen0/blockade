using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(BoxCollider))]
public class MouseAspect : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursorTexture;
    public Texture2D DefaultTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot= Vector2.zero;
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture,hotSpot,cursorMode);
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        Cursor.SetCursor(DefaultTexture,Vector2.zero,cursorMode);
    }
}
