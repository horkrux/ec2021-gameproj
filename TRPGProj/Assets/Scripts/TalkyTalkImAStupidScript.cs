using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkyTalkImAStupidScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D mouse;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(mouse, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
