using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_2D : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally

    public Sprite trainerSpriteUp;
    public Sprite trainerSpriteDown;
    public Sprite trainerSpriteLeft;
    public Sprite trainerSpriteRight;



    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally


    private static bool playerExists;

    void Start()
    {
        // Initialize the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if(!playerExists){
            playerExists= true;
            DontDestroyOnLoad(transform.gameObject);
        } else{
            Destroy (gameObject);
        }

        
    }

    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = trainerSpriteUp;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = trainerSpriteDown;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = trainerSpriteLeft;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = trainerSpriteRight;
        }


        // Check if diagonal movement is allowed
        if (canMoveDiagonally)
        {
            // Set movement direction based on input
            movement = new Vector2(horizontalInput, verticalInput);
            // Optionally rotate the player based on movement direction
            RotatePlayer(horizontalInput, verticalInput);
        }
        else
        {
            // Determine the priority of movement based on input
            if (horizontalInput != 0)
            {
                isMovingHorizontally = true;
            }
            else if (verticalInput != 0)
            {
                isMovingHorizontally = false;
            }

            // Set movement direction and optionally rotate the player
            if (isMovingHorizontally)
            {
                movement = new Vector2(horizontalInput, 0);
                RotatePlayer(horizontalInput, 0);
            }
            else
            {
                movement = new Vector2(0, verticalInput);
                RotatePlayer(0, verticalInput);
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        rb.velocity = movement * speed;
    }

    void RotatePlayer(float x, float y)
    {
        // If there is no input, do not rotate the player
        //if (x == 0 && y == 0) return;

        //if button

        // Calculate the rotation angle based on input direction
        //float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        // Apply the rotation to the player
        //transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
