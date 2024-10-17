using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    public Transform target;

    private Vector3 vel = Vector3.zero;

    private static bool cameraExists;

    void Start(){

        if(!cameraExists){
            cameraExists= true;
            DontDestroyOnLoad(transform.gameObject);
        } else{
            Destroy (gameObject);
        }
        
    }

    private void FixedUpdate() {
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }
}

