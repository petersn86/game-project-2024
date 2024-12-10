using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReload2 : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool enemyExists2;

    void Start()
    {
        if (!enemyExists2)
        {
            enemyExists2 = true;
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
