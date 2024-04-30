using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HoverElement : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D DefaultTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot= Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeWhenHover(){
        Cursor.SetCursor(cursorTexture,hotSpot,cursorMode);
    }
    public void changeWhenLeaves(){
        Cursor.SetCursor(DefaultTexture,Vector2.zero,cursorMode);
    }


}
