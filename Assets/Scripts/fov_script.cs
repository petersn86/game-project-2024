using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fov_script : MonoBehaviour
{
    public Mesh mesh;
    public float fov;
    public float startingAngle;
    public GameObject    enemyObject;
    private enemy_script enemyObjectScript;
    public float smoothTime = 0.1f;
    private float currentVelocity;
    public LayerMask playerMask;
    public bool playerDetected;

    /* Get Vector from given angle */
    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    /* Get Angle from given vector */
    public static float GetAngleFromVector(Vector2 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    
    void Start()
    {
        /* Create Mesh Object */
        mesh                            = new Mesh();

        /*Get Mesh Object from game object */
        GetComponent<MeshFilter>().mesh = mesh;

        /* Set Field of View */
        fov                             = 45;

        /* Get Enemy Script from Enemy game object */
        enemyObjectScript               = enemyObject.GetComponent<enemy_script>();
    }

    
    void Update()
    {
        /* Set number of rays & mesh properties */ 
        int rayCount        = 50;
        float angle         = startingAngle;
        float viewDistance  = 1.5f;
        float angleIncrease = fov / rayCount;
        Vector3 origin      = enemyObjectScript.originOfEnemy;

        Vector3[] vertices  = new Vector3[rayCount + 2];
        Vector2[] uv        = new Vector2[vertices.Length];
        int[] triangles     = new int[rayCount * 3];

        vertices[0]         = origin;

        int vertexIndex     = 1;
        int triangleIndex   = 0;

        for(int i =0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance);

            /* If no hit */
            if(raycastHit.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            /* Else hit */
            else
            {
                vertex = raycastHit.point;


                RaycastHit2D raycastHitPlayer 
                        = Physics2D.Raycast(origin, GetVectorFromAngle(angle), Vector3.Distance(origin, vertex), playerMask);

                /* If player is hit */
                if (raycastHitPlayer.collider != null)
                {
                    playerDetected = true;
                }
            }

            vertices[vertexIndex] = vertex;

            if(i > 0)
            {
                triangles[triangleIndex]        = 0;
                triangles[triangleIndex + 1]    = vertexIndex - 1;
                triangles[triangleIndex + 2]    = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        /* Set to mesh */
        mesh.vertices   = vertices;
        mesh.uv         = uv;
        mesh.triangles  = triangles;

        mesh.RecalculateBounds();


    }

    public void SetAimDirection(Vector2 aimDirection)
    {
        float targetAngle = GetAngleFromVector(aimDirection) + (fov / 2f);

        float turnSpeed = 5f;

        /* Smoothly interpolate the target angle */
        //startingAngle = Mathf.SmoothDampAngle(startingAngle, targetAngle, ref currentVelocity, smoothTime);

        startingAngle = Mathf.LerpAngle(startingAngle, targetAngle, Time.deltaTime * turnSpeed);

    }
}
