using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Texture2D CursorArrow;
    public Texture2D CursorHand;
    Vector2 position;

    void Start()
    {
        position = new Vector2(15, 10);
        //Cursor.visible = false;
        Cursor.SetCursor(CursorArrow, position, CursorMode.ForceSoftware);
        
    }


    public void OnMouseEnter()
    {
        Cursor.SetCursor(CursorHand, position, CursorMode.ForceSoftware);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(CursorArrow, position, CursorMode.ForceSoftware);
    }
}
