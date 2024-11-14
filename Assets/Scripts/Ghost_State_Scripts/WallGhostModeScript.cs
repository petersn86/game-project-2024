using UnityEngine;

/*
    This is the script to enable passthrough with ghost mode
    It works by getting the ghost state and if the player is ghosted just remove the collider of the wall
    It also turns the wall slighly transparent to signify that players can go through it
    
    To use, simply just attach to any wall, the rest should work normally
*/

public class GhostPassThroughSquare : MonoBehaviour
{
    public GameObject player;         // Reference to the player
    public float detectionRadius = 5f; // Distance at which the square becomes translucent
    public float transparency = 0.5f; // Transparency of the square when the player is near in ghost mode

    private SpriteRenderer squareSpriteRenderer;
    private Color originalColor;
    private Collider2D squareCollider; // Collider for the square

    private GhostState2DWithSlider ghostState; // Reference to the ghost state script on the player

    void Start()
    {
        // Get the SpriteRenderer component of the square
        squareSpriteRenderer = GetComponent<SpriteRenderer>();
        if (squareSpriteRenderer != null)
        {
            // Store the original color of the square
            originalColor = squareSpriteRenderer.color;
        }

        // Get the Collider2D component of the square
        squareCollider = GetComponent<Collider2D>();

        // Get the GhostState2DWithSlider component from the player
        ghostState = player.GetComponent<GhostState2DWithSlider>();
        if (ghostState == null)
        {
            Debug.LogError("Player does not have GhostState2DWithSlider component.");
        }
    }

    void Update()
    {
        if (ghostState != null && ghostState.isGhost) // Check if the player is in ghost mode
        {
            // Check the distance between the player and the square
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= detectionRadius)
            {
                // Player is within the radius and in ghost mode, make the square translucent and passable
                SetSquareTransparency(transparency);
                squareCollider.isTrigger = true; // Make the square passable
            }
            else
            {
                // Player is outside the radius, return the square to its original state
                SetSquareTransparency(1f);
                squareCollider.isTrigger = false; // Make the square solid again
            }
        }
        else
        {
            // Player is not in ghost mode, return the square to its original state
            SetSquareTransparency(1f);
            squareCollider.isTrigger = false; // Ensure the square is solid
        }
    }

    void SetSquareTransparency(float alpha)
    {
        if (squareSpriteRenderer != null)
        {
            // Modify the alpha (transparency) value of the square's sprite color
            Color newColor = originalColor;
            newColor.a = alpha;

            // Apply the new color to the square sprite
            squareSpriteRenderer.color = newColor;
        }
    }
}
