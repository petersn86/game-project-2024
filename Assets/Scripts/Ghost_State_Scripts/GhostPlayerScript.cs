using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for UI components


// HOW THE SCRIPT WORKS!!!

/*

    This goes on the PLAYER character and interacts with the SLIDER componant that is placed under it.
        -> it does not need a slider componant however it has funcitonality for one, it will throw warnings without one however
    The main focus of this script is the "isGhost" feature, which ghosts the charater and can be refrenced anywhere else for compatability
    All other varaibles can be edited in unity itself and should 
*/


public class GhostState2DWithSlider : MonoBehaviour
{
    public KeyCode activateGhostKey = KeyCode.E;  // Key to activate ghost mode
    public float maxGhostDuration = 5f;  // Maximum ghost state duration
    public float transparency = 0.5f; // Transparency level (0 = fully transparent, 1 = fully opaque)
    public bool isGhost = false;
    private SpriteRenderer playerSpriteRenderer;
    private Color originalColor;

    public Slider ghostSlider; // Reference to the UI Slider

    private float remainingGhostTime;

    void Start()
    {
        // Get the SpriteRenderer component of the player character
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer != null)
        {
            // Store the original color of the player sprite
            originalColor = playerSpriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on the player character.");
        }

        // Initialize the slider (disable it at the start)
        if (ghostSlider != null)
        {
            ghostSlider.maxValue = maxGhostDuration; // Set slider's max value
            ghostSlider.value = 0;  // Start with slider set to 0
            ghostSlider.gameObject.SetActive(false);  // Hide the slider initially
        }
    }

    void Update()
    {
        // Check if the player presses the activate key and is not already in ghost state
        if (Input.GetKeyDown(activateGhostKey) && !isGhost)
        {
            StartCoroutine(ActivateGhostState());
        }
    }

    IEnumerator ActivateGhostState()
    {
        isGhost = true;
        remainingGhostTime = maxGhostDuration;

        // Show the slider when ghost mode is activated
        if (ghostSlider != null)
        {
            ghostSlider.gameObject.SetActive(true);
            ghostSlider.value = maxGhostDuration; // Set the slider to full
        }

        // Set the player's sprite transparency
        SetTransparency(transparency);

        // Deplete the ghost state over time
        while (remainingGhostTime > 0)
        {
            remainingGhostTime -= Time.deltaTime;

            // Update the slider value based on the remaining time
            if (ghostSlider != null)
            {
                ghostSlider.value = remainingGhostTime;
            }

            yield return null; // Wait for the next frame
        }

        // Reset transparency after ghost mode ends
        SetTransparency(1f);

        // Hide the slider when ghost mode ends
        if (ghostSlider != null)
        {
            ghostSlider.gameObject.SetActive(false);
        }

        isGhost = false;
    }

    void SetTransparency(float alpha)
    {
        if (playerSpriteRenderer != null)
        {
            // Modify the alpha (transparency) value of the player's sprite color
            Color newColor = originalColor;
            newColor.a = alpha;

            // Apply the new color to the player sprite
            playerSpriteRenderer.color = newColor;
        }
    }
}
