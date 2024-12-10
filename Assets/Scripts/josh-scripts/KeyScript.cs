using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI components



public class KeyScript : MonoBehaviour
{
    private SpriteRenderer keySpriteRenderer;
    public bool keyGet; //is key obtained?
    public float transparency = 0.0f; // Transparency level (0 = fully transparent, 1 = fully opaque)
     private Color originalColor;
    // Get the SpriteRenderer component of the key
    

    // Start is called before the first frame update
    void Start()
    {
        keySpriteRenderer = GetComponent<SpriteRenderer>();
        if (keySpriteRenderer != null)
        {
            // Store the original color of the player sprite
            originalColor = keySpriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on the player character.");
        }
        keyGet = false;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        keyGet = true;
        
        SetTransparency(transparency);
    }

    void SetTransparency(float alpha)
    {
        if (keySpriteRenderer != null)
        {
            // Modify the alpha (transparency) value of the player's sprite color
            Color newColor = originalColor;
            newColor.a = alpha;

            // Apply the new color to the player sprite
            keySpriteRenderer.color = newColor;
        }
    }
}
