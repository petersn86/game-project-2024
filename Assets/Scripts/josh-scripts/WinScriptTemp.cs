using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScriptTemp : MonoBehaviour
{
    public KeyScript keyObjectScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (keyObjectScript.keyGet)
        {
            Application.LoadLevel("WinScene");
        }
    }
}
