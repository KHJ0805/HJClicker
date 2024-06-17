using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(MousePos.x, MousePos.y);
    }
}
