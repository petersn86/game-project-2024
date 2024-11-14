using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{

    public Transform[]  patrolPoints;
    public float        moveSpeed           = 2.0f;
    private float       waitTime            = 3f;
    public float        deathCounter        = 4f;

    public int          currentPointIndex   = 0;
    public float        waitCounter;
    public bool         waiting             = false;

    public GameObject fovObject;
    public fov_script fovObjectScript;

    public Vector3      originOfEnemy;

    public bool         playerCaught;
    public float        caughtCounter;


    public GameObject   player;


    private Rigidbody2D rb;
    void Start()
    {
        /* Get RigidBody Component */
        rb          = GetComponent<Rigidbody2D>();

        /* Wait Counter Starts at Max Time */
        waitCounter = waitTime;

        fovObjectScript = fovObject.GetComponent<fov_script>();
        originOfEnemy   = transform.position;

        playerCaught    = false;
        caughtCounter   = 5f;
    }

    void Update()
    {
        originOfEnemy = transform.position;
        if (fovObjectScript.playerDetected) 
        {
            if (!playerCaught)
            {
                playerCaught    = true;
                caughtCounter   = 4f;
            }

            Vector2 playerDirection = (player.transform.position - transform.position).normalized;

            fovObjectScript.SetAimDirection(playerDirection);
            rb.velocity     = playerDirection * 0;

            deathCounter    -= Time.deltaTime;

            if (deathCounter <= 0) 
            {
                Debug.Log("Player Died!");
            }

            caughtCounter   -= Time.deltaTime;

            if(caughtCounter <= 0)
            {
                fovObjectScript.playerDetected  = false;
                playerCaught                    = false;
                deathCounter                    = 4;
            }

        }
        else if (!playerCaught)
        {
            /* If the enemy is waiting, decrement the wait counter */
            if (waiting)
            {
                waitCounter -= Time.deltaTime;
                /* Once wait counter = 0, move towards next patrol point */
                if (waitCounter <= 0)
                {
                    waiting = false;
                    waitCounter = waitTime;

                    /* Set index to the next patrol point */
                    currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
                }
            }
            else
            {
                moveTowardsPatrolPoint();
            }
        }

    }
    
    /* Move enemy to next patrol point */
    void moveTowardsPatrolPoint()
    {
        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector2   direction   = (targetPoint.position - transform.position).normalized;

        rb.velocity           = direction * moveSpeed;

        /* Set Mesh direction to direction enemy is moving */
        fovObjectScript.SetAimDirection(direction);

        /* If the distance between the enemy & the patrol point is 0, then enter waiting state */
        if(Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            waiting = true;
            rb.velocity = direction * 0;
        }

    }
}
