using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grafitti : MonoBehaviour
{
    [SerializeField]
    private GameObject PlaceableObjectPrefab;

    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.E;

    private GameObject currentPlaceableObject;
    public GameObject Player;

    private Vector2 spawnPosition = new Vector2();
    //private Quaternion spawnRotation = new Quaternion();

    private void Update()
    {
        HandleNewObjectHotkey();
    }

    private void HandleNewObjectHotkey()
    {
        spawnPosition = Player.transform.position + (Player.transform.forward * 2);
    
        if (Input.GetKeyDown(newObjectHotkey))
        {
            currentPlaceableObject = Instantiate(PlaceableObjectPrefab, spawnPosition, Quaternion.identity);
            /*
            if(currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(PlaceableObjectPrefab, spawnPosition, Quaternion.identity);
            }
            
            else
            {
                Destroy(currentPlaceableObject);
            }
            */
        }
    }
}
