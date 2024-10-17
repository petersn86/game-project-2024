using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    private Player_Controller_2D thePlayer;
    private CameraController theCamera;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player_Controller_2D>();
        thePlayer.transform.position = new Vector3(transform.position.x, transform.position.y, thePlayer.transform.position.z);//thePlayer.transform.position = transform.position;

        theCamera = FindObjectOfType<CameraController>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
