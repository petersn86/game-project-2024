using UnityEngine;
using UnityEngine.Tilemaps;


/*
        To be placed on the tilemap of walls you want to go through
*/
public class WallTilemapGhostHandler : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public float transparency = 0.5f; // Transparency level for walls in ghost mode (0 = fully transparent, 1 = fully opaque)

    private Tilemap tilemap; // Reference to the Tilemap component
    private TilemapCollider2D tilemapCollider; // Reference to the TilemapCollider2D
    private GhostState2DWithSlider playerGhostState; // Reference to the player's ghost mode script
    private Vector3Int lastTransparentTile; // Store the last transparent tile position
    private Color originalTileColor; // Store the original color of the tile
    private bool tileChanged = false; // Track if a tile was changed

    void Start()
    {
        // Get the Tilemap component
        tilemap = GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("Tilemap component not found on this object.");
            return;
        }

        // Get the TilemapCollider2D component
        tilemapCollider = GetComponent<TilemapCollider2D>();
        if (tilemapCollider == null)
        {
            Debug.LogError("TilemapCollider2D component not found on this object.");
            return;
        }

        // Get the player's GhostState2DWithSlider script
        if (player != null)
        {
            playerGhostState = player.GetComponent<GhostState2DWithSlider>();
            if (playerGhostState == null)
            {
                Debug.LogError("GhostState2DWithSlider script not found on the player.");
            }
        }
        else
        {
            Debug.LogError("Player object is not assigned.");
        }
    }

    void Update()
    {
        if (player == null || playerGhostState == null || tilemap == null || tilemapCollider == null) return;

        // Check if the player is in ghost mode
        if (playerGhostState.isGhost)
        {
            HandleWallTransparency();
            EnableWallPassThrough(); // Disable collision
        }
        else
        {
            ResetWallTransparency();
            DisableWallPassThrough(); // Enable collision
        }
    }

    void HandleWallTransparency()
    {
        // Get the player's current position
        Vector3 playerPosition = player.transform.position;

        // Convert the player's position to a tile position
        Vector3Int tilePosition = tilemap.WorldToCell(playerPosition);

        // If the player has moved to a different tile
        if (tilePosition != lastTransparentTile)
        {
            ResetWallTransparency(); // Reset the previous tile's transparency

            // Check if the current tile exists in the tilemap
            if (tilemap.HasTile(tilePosition))
            {
                // Store the original color of the tile
                originalTileColor = tilemap.GetColor(tilePosition);

                // Set the tile's transparency
                Color transparentColor = originalTileColor;
                transparentColor.a = transparency;
                tilemap.SetColor(tilePosition, transparentColor);

                // Update the last transparent tile
                lastTransparentTile = tilePosition;
                tileChanged = true;
            }
        }
    }

    void ResetWallTransparency()
    {
        // If the last transparent tile exists, reset its color
        if (tilemap.HasTile(lastTransparentTile))
        {
            tilemap.SetColor(lastTransparentTile, originalTileColor);
        }

        tileChanged = false;
    }

    void EnableWallPassThrough()
    {
        // Disable the TilemapCollider2D so the player can pass through walls
        if (tilemapCollider != null)
        {
            tilemapCollider.enabled = false;
        }
    }

    void DisableWallPassThrough()
    {
        // Enable the TilemapCollider2D so walls block the player again
        if (tilemapCollider != null)
        {
            tilemapCollider.enabled = true;
        }
    }
}
