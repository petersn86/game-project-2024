using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emitter_parent : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool emittersExists;

    void Start()
    {
        if (!emittersExists)
        {
            emittersExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

