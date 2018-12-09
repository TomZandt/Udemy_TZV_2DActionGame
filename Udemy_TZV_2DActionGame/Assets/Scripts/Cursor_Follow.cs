using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Follow : MonoBehaviour
{
    //****************************************************************************************************
    private void Start()
    {
        Cursor.visible = false;
    }

    //****************************************************************************************************
    private void Update()
    {
        transform.position = Input.mousePosition;
    }

}
