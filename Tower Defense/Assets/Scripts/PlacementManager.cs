using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] public ShopManager shopManager;
    public GameObject basicTowerObject;
    private GameObject dummyPlacement;
    private GameObject hoverTile;
    public Camera cam;
    public LayerMask mask;
    public LayerMask towerMask;
    public bool isBuilding;

    private GameObject currentTowerPlacing;

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

    public bool checkForTower()
    {
        bool blocked = false;

        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, towerMask, -20, 20);

        if( hit.collider != null)
        {
            blocked = true;
        }

        return blocked;
    }

    public void placeBuilding()
    {
        if(hoverTile != null && !checkForTower() )
        {
            if (shopManager.CanBuyTower(currentTowerPlacing))
            {
                GameObject newTower = Instantiate(currentTowerPlacing);
                newTower.layer = LayerMask.NameToLayer("Tower");
                newTower.transform.position = hoverTile.transform.position;
            }
            else
            {
                Debug.Log("Insufficient money to buy tower");
            }

            EndBuilding();
            shopManager.buyTower(currentTowerPlacing);
        }
    }

    public void StartBuilding(GameObject towerToBuild)
    {
        isBuilding = true;
        currentTowerPlacing = towerToBuild;

        dummyPlacement = Instantiate(currentTowerPlacing);
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
        if(dummyPlacement != null)
        {
            Destroy(dummyPlacement);
        }
    }

    public void Update()
    {
        if(isBuilding && dummyPlacement != null)
        {
            currentHoverTile();
            if(hoverTile!=null)
            {
                dummyPlacement.transform.position = hoverTile.transform.position;
            }
        }

        if(Input.GetButtonDown("Fire1"))
        {
            placeBuilding();
        }
    }
}
