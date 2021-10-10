using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] public GameObject MapTile;
    [SerializeField] public GameObject ParentGameObject;

    // Map width and height
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    // Decorators
    /*[SerializeField] public Color pathColor;
    [SerializeField] public Color startColor;
    [SerializeField] public Color endColor;*/
    [SerializeField] public Sprite pathSprite;
    [SerializeField] public Sprite startSprite;
    [SerializeField] public Sprite endSprite;
    [SerializeField] public Sprite defaultSprite;

    // 
    public List<GameObject> mapTiles = new List<GameObject>();
    public List<GameObject> pathTiles = new List<GameObject>();

    public GameObject startTile;
    public GameObject endTile;

    // Validate that map if navigable
    private bool reachedX = false;
    private bool reachedY = false;
    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;

    // Generate map tiles
    private void generateMap()
    {
        for(int y=0; y<mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(MapTile, ParentGameObject.transform);
                mapTiles.Add(newTile);
                newTile.transform.position = new Vector2(x, y);
            }
        }

        // Pick random start and end tile
        List<GameObject> topEdgeTiles = getTopEdgeTiles();
        List<GameObject> bottomEdgeTiles = getBottomEdgeTiles();

        /*int rand1 = Random.Range(0, mapWidth);
        int rand2 = Random.Range(0, mapWidth);

        startTile = topEdgeTiles[rand1];
        endTile = bottomEdgeTiles[rand2];*/

        // Manually set start and end tiles
        startTile = topEdgeTiles[mapWidth / 2];
        endTile = bottomEdgeTiles[mapWidth / 2];

        // Validate that start and end tiles can be reached
        currentTile = startTile;
        int loopCount = 0;
        while(!reachedX && loopCount < 100)
        {
            // Navigate left or right to match the target x value
            /*if(currentTile.transform.position.x > endTile.transform.position.x)
            {
                moveLeft();
            }
            else if (currentTile.transform.position.x < endTile.transform.position.x)
            {
                moveRight();
            }
            else
            {
                reachedX = true;
            }*/

            if (currentTile.transform.position.y > endTile.transform.position.y)
            {
                moveDown();
            }

            loopCount++;
        }
        while(!reachedY && loopCount<100)
        {
            if(currentTile.transform.position.y > endTile.transform.position.y)
            {
                moveDown();
            }
            else
            {
                reachedY = true;
            }
        }

        pathTiles.Add(endTile);

        // Add decorators to map
        foreach(GameObject obj in pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().sprite = pathSprite;
        }
        startTile.GetComponent<SpriteRenderer>().sprite = startSprite;
        endTile.GetComponent<SpriteRenderer>().sprite = endSprite;
    }

    // Helper functions for navigating from top to bottom and left/right
    private void moveDown()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }
    private void moveLeft()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-1;
        currentTile = mapTiles[nextIndex];
    }
    private void moveRight()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex+1;
        currentTile = mapTiles[nextIndex];
    }

    // List of top and bottom edge tiles
    private List<GameObject> getTopEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        // Loop through all tiles on top row (mapHeight -1 )
        for(int i=mapWidth*(mapHeight-1); i< mapWidth*mapHeight; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }
    private List<GameObject> getBottomEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        // Loop through all tiles on bottom row (first row)
        for (int i = 0; i < mapWidth; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }
        return edgeTiles;
    }

    private void Start()
    {
        generateMap();
    }
}
