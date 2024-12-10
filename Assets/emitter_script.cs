using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emitter_script : MonoBehaviour
{
    public GameObject player;
    public MeshRenderer mesh_Renderer;
    float intensity;
    Material material;


    private void Start()
    {
        material = mesh_Renderer.material;
        intensity = -3f;

        material.SetColor("_EmissionColor", Color.green * intensity);
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        intensity = Mathf.InverseLerp(1f, 0f, distance);

        material.SetColor("_EmissionColor", Color.green * intensity);
    }
}