using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridReload : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool gridExists;

    void Start()
    {
        if (!gridExists)
        {
            gridExists = true;
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
