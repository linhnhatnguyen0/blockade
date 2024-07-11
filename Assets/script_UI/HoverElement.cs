using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HoverElement : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D DefaultTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot= Vector2.zero;
    
    public void changeWhenHover( ){    
       Cursor.SetCursor(cursorTexture,hotSpot,cursorMode);
           
    }
    public void changeWhenLeaves(){
        Cursor.SetCursor(DefaultTexture,Vector2.zero,cursorMode);
    }


}
