using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallPassThroughWithUI : MonoBehaviour
{
    // keybind to pass through walls
    public KeyCode passThroughKey = KeyCode.E;

    // duration of passthrough
    public float passThroughDuration = 2f;

    // References to wall, collider, and sprite renderer for color change
    private Collider2D parentWallCollider; // Reference to the parent wall's collider
    private SpriteRenderer parentWallRenderer;
    public Color passThroughColor = Color.green; // Color during pass-through
    private Color originalColor; // Original color of the wall

    public Slider passThroughSlider;

    // checks if wall is pass-throughable
    private bool isPassThroughActive = false;
    private bool isPlayerNear = false; // Tracks if the player is near the wall

    // remaining time of passthrough
    private float remainingTime = 0f;

    // Reference to player tag (used for detecting player)
    public string playerTag = "Player";

    // Reference to the child proximity trigger
    public Collider2D proximityTrigger;

    private void Start()
    {
        // Get the parent wall's collider and renderer (the object holding the solid wall part)
        parentWallCollider = transform.parent.GetComponent<Collider2D>(); // Access parent collider
        parentWallRenderer = transform.parent.GetComponent<SpriteRenderer>(); // Access parent renderer

        // Save the original wall color
        if (parentWallRenderer != null)
        {
            originalColor = parentWallRenderer.color;
        }

        // Initialize the pass-through slider
        if (passThroughSlider != null)
        {
            passThroughSlider.gameObject.SetActive(false);
            passThroughSlider.maxValue = passThroughDuration;
            passThroughSlider.value = 0;
        }
    }

    private void Update()
    {
        // checks if key is pressed and player is near the wall
        if (Input.GetKeyDown(passThroughKey) && !isPassThroughActive && isPlayerNear)
        {
            // Start the pass-through process
            StartCoroutine(DisableWallForDuration(passThroughDuration));
        }

        // Update the slider if pass-through is active
        if (isPassThroughActive && passThroughSlider != null)
        {
            passThroughSlider.value = remainingTime;
        }
    }

    private IEnumerator DisableWallForDuration(float duration)
    {
        // Enable the pass-through slider
        if (passThroughSlider != null)
        {
            passThroughSlider.gameObject.SetActive(true);
        }

        // Change the wall color to indicate pass-through
        if (parentWallRenderer != null && isPlayerNear)
        {
            parentWallRenderer.color = passThroughColor;
        }

        // Disable the parent's collider (not the proximity trigger)
        if (parentWallCollider != null)
        {
            parentWallCollider.enabled = false;
        }

        isPassThroughActive = true;
        remainingTime = duration;

        // Countdown for the pass-through duration
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        // Re-enable the parent's collider and reset the color/UI
        if (parentWallCollider != null)
        {
            parentWallCollider.enabled = true;
        }
        isPassThroughActive = false;

        if (parentWallRenderer != null)
        {
            parentWallRenderer.color = originalColor; // Reset to original color
        }

        if (passThroughSlider != null)
        {
            passThroughSlider.gameObject.SetActive(false);
        }
    }

    // Detect when the player enters the proximity trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = true;
        }
    }

    // Detect when the player exits the proximity trigger area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = false;
        }
    }
}
