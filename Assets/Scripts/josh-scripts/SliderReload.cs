using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderReload : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool sliderExists;

    void Start()
    {
        if (!sliderExists)
        {
            sliderExists = true;
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
