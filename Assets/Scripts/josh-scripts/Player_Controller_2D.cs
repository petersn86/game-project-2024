using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI; // For UI components like Slider


public class Player_Controller_2D : MonoBehaviour
{
    // Public variables
    private float speed = 1f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally

    public Sprite trainerSpriteUp;
    public Sprite trainerSpriteDown;
    public Sprite trainerSpriteLeft;
    public Sprite trainerSpriteRight;

    /* Variables for Dashing Mechanic */
    private KeyCode lastInput;
    private bool canDash = true; // Whether the player can dash
    public bool isDashing;
    private float dashSpeed = 5f; // Reduced dash speed (50% smaller)
    private float dashCooldown = 4f; // Cooldown time after each dash (seconds)
    public float cooldownTimer; // Timer for cooldown
    private float trailCooldown = 0.5f; // Time before trail and opacity are reset
    private float trailTimer; // Timer for trail and opacity reset
    [SerializeField] private TrailRenderer tr;
    private TilemapCollider2D tilemapCollider;
    public GameObject tilemap;
    private Color currentColor;

    // Reference to the cooldown slider
    public Slider dashCooldownSlider;

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally

    private static bool playerExists;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        

        // Initialize the Rigidbody2D and SpriteRenderer components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;

        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        lastInput = KeyCode.W;

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();

        // Initialize the slider if assigned
        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.maxValue = dashCooldown;
            dashCooldownSlider.value = dashCooldown;
            dashCooldownSlider.gameObject.SetActive(false); // Start inactive
        }
    }

    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Sprite change logic
        //if (!Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D)) { 
        if (Input.GetKeyDown(KeyCode.W)) { spriteRenderer.sprite = trainerSpriteUp; lastInput = KeyCode.W; }
        if (Input.GetKeyDown(KeyCode.S)) { spriteRenderer.sprite = trainerSpriteDown; lastInput = KeyCode.S; }
        if (Input.GetKeyDown(KeyCode.A)) { spriteRenderer.sprite = trainerSpriteLeft; lastInput = KeyCode.A; }
        if (Input.GetKeyDown(KeyCode.D)) { spriteRenderer.sprite = trainerSpriteRight; lastInput = KeyCode.D; }
        //}


        // Dash mechanic logic with cooldown
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
            tr.emitting = true;
            StartDash();
        }

        // Update the cooldown timers for dash
        if (!canDash)
        {
            cooldownTimer -= Time.deltaTime;
            trailTimer -= Time.deltaTime;

            if (trailTimer <= 0)
            {
                tr.emitting = false; // Stop trail effect
                spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f); // Reset opacity
            }

            // Update the slider value
            if (dashCooldownSlider != null)
            {
                dashCooldownSlider.value = cooldownTimer;

                if (cooldownTimer <= 0)
                {
                    dashCooldownSlider.gameObject.SetActive(false); // Hide the slider when cooldown ends
                    canDash = true; // Allow the player to dash again
                }
            }
        }

        // Handle player movement direction
        if (canMoveDiagonally)
        {
            movement = new Vector2(horizontalInput, verticalInput);
        }
        else
        {
            if (horizontalInput != 0)
                isMovingHorizontally = true;
            else if (verticalInput != 0)
                isMovingHorizontally = false;

            movement = isMovingHorizontally ? new Vector2(horizontalInput, 0) : new Vector2(0, verticalInput);
        }

    }

    private void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        rb.velocity = movement * speed;

        // Handle dashing
        if (isDashing)
        {
            PerformDash();
        }
    }

    private void StartDash()
    {
        // Start the DashCoroutine instead of directly handling dashing
        StartCoroutine(DashCoroutine());

        // Set the cooldown timer and disable dashing
        canDash = false;
        cooldownTimer = dashCooldown; // Start cooldown timer
        trailTimer    = trailCooldown;

        // Activate the slider and set its value
        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.gameObject.SetActive(true);
            dashCooldownSlider.value = dashCooldown;
        }
    }

    private IEnumerator DashCoroutine()
    {
        // Temporarily disable tilemap collider to allow dashing through walls
        tilemapCollider.enabled = false;

        // Wait for a small moment to ensure the collider is disabled
        yield return new WaitForFixedUpdate();  // This will wait for one fixed update cycle

        // Set the dash state
        isDashing = true;

        // Perform the dash movement
        PerformDash();

        // Wait for a fixed duration to end the dash
        yield return new WaitForSeconds(0.1f); // Dash lasts for 0.1 seconds

        // Re-enable the tilemap collider after dash ends
        tilemapCollider.enabled = true;

        // End dash state
        isDashing = false;
    }

    private void PerformDash()
    {
        Vector2 dashDirection = Vector2.zero;

        // Determine dash direction based on last input
        if (lastInput == KeyCode.W)
            dashDirection = transform.up;
        else if (lastInput == KeyCode.S)
            dashDirection = -transform.up;
        else if (lastInput == KeyCode.A)
            dashDirection = -transform.right;
        else if (lastInput == KeyCode.D)
            dashDirection = transform.right;

        // Apply the dash velocity (forceful movement in the dash direction)
        rb.velocity = dashDirection * dashSpeed;
    }

    
}
