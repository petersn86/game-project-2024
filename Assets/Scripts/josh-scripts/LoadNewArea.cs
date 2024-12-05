using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Tracking current scene

public class LoadNewArea : MonoBehaviour
{
    public string leveltoLoad;
    private string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene ();

		// Retrieve the name of this scene.
		sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if ((sceneName == "LossScene") && (Input.GetKeyDown(KeyCode.F)))
        {
            Application.LoadLevel("GridScene");
        }

        if ((sceneName == "WinScene") && (Input.GetKeyDown(KeyCode.F)))
        {
            Application.LoadLevel("GridScene");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player"){
            Application.LoadLevel(leveltoLoad);
        }
    }

    //private void TryAgain()
    //{
        // Try Again
        //if ((sceneName == "LossScene") && (Input.GetKeyDown(KeyCode.F)))
        //{
            //Application.LoadLevel("GridScene");
        //}
    //}
}
