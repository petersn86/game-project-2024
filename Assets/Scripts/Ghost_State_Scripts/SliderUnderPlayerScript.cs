using UnityEngine;
using UnityEngine.UI;

/*

    The only thing this script does is place the slider under the player game object (ie a thing with the player tag)
        -> if you want to make a new slider for somthing change the tag in here with a copy of the script
    To use, place an empty object under the slider prefab and attach this script to it, that all
*/

public class SliderBinding : MonoBehaviour
{
    public Slider slider; // Reference to the slider GameObject

    void Update()
    {
        // Find the object with the "Player" tag
        GameObject player = GameObject.FindWithTag("Player");

        // If a player object is found, position the slider beneath it
        if (player != null)
        {
            slider.transform.position = player.transform.position + new Vector3(0, -1, 0); // Adjust the y-offset as needed
            slider.transform.rotation = Quaternion.identity; // Ensure the slider doesn't rotate
        }
    }
}