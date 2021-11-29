using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    public float BlockDestroyTime;
    bool CanDig = true;


    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contain(tileOnNewPosition)) {
            transform.position = newPosition;
        } else {
            
                if (Input.GetKey(KeyCode.X) && CanDig)
                {
                    StartCoroutine(Wait());
                    transform.position = newPosition;
                    Vector3Int Position = tilemap.WorldToCell(newPosition);
                    tilemap.SetTile(Position, tilemap.GetComponent<TilemapCaveGenerator>().floorTile);
                }
        }
    }
    private IEnumerator Wait()
    {
        CanDig = false;
        yield return new WaitForSeconds(BlockDestroyTime);
        CanDig = true;
    }

}
