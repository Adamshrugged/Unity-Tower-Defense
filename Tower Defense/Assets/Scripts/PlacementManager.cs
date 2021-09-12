using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject basicTowerObject;
    private GameObject dummyPlacement;
    private GameObject hoverTile;
    public Camera cam;
    public LayerMask mask;
    public bool isBuilding;

    // get fector from mouse position
    public Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void currentHoverTile()
    {
        // detect map tile from mouse position
        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, mask, -100, 100);

        if (hit.collider != null)
        {
            // Check for map tile
            if(MapGenerator.mapTiles.Contains(hit.collider.gameObject))
            {
                // Check that it is not a path tile
                if(!MapGenerator.pathTiles.Contains(hit.collider.gameObject))
                {
                    hoverTile = hit.collider.gameObject;
                }
            }
        }
    }

    public void StartBuilding()
    {
        isBuilding = true;
        dummyPlacement = Instantiate(basicTowerObject);
        if( dummyPlacement.GetComponent<Tower>() != null )
        {
            Destroy(dummyPlacement.GetComponent<Tower>());
        }
        if (dummyPlacement.GetComponent<TowerBarrelRotation>() != null)
        {
            Destroy(dummyPlacement.GetComponent<TowerBarrelRotation>());
        }
    }

    public void EndBuilding()
    {
        isBuilding = false;
    }

    public void Update()
    {
        Debug.Log(GetMousePosition());
        StartBuilding();
    }
}
