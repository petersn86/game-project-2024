using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReload3 : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool enemyExists3;

    void Start()
    {
        if (!enemyExists3)
        {
            enemyExists3 = true;
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
