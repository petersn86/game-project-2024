using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReload : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool enemyExists;

    void Start()
    {
        if (!enemyExists)
        {
            enemyExists = true;
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
