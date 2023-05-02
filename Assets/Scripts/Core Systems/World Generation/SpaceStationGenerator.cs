using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class SpaceStationGenerator : MonoBehaviour
{
    /// <summary>
    /// The main information for the generation
    /// </summary>
    [System.Serializable]
    public class Cell
    {
        public bool[] doors = new bool[4];     // Which doors are open | 0 = North, 1 = East, 2 = South, 3 = West
        public Vector2Int cellPosition;        // Cordinates of this cell (not relative to unity)
    }

    [Header("The height and length of the maze")]
    [SerializeField] int sizeX; // Length
    [SerializeField] int sizeY; // Height

    [Header("Offset for the rooms")]
    [SerializeField] Vector2 offset;    // Based on the size of the floor, the offset needs to be set so they don't intersect

    [Header("Prefab for the rooms to spawn")]
    [SerializeField] List<GameObject> roomPrefab;
    [SerializeField] GameObject bossRoomPrefab;

    [Header("Next step of generation")]
    public NavMeshSurface[] surfaces;
    [SerializeField] WorldSpawner objectSpawner;

    List<Cell> cells;           // List of all current cells
    Cell curCell;               // The current cell being used

    private void Start() { CellInitialization(); }

    /// <summary>
    /// Initializes the first cell and starts the cell construction
    /// </summary>
    void CellInitialization()
    {
        cells = new List<Cell>();

        CellPositions();
        curCell = cells[0];         // Sets the current cell equal to the cell at (0,0)

        // Go through each cell and open the approritate doors
        foreach(Cell cell in cells)
        {
            curCell = cell;
            CheckNeighbors(curCell.cellPosition.x, curCell.cellPosition.y);
        }

        SpawnPrefabs();

        // Bake the navmesh
        surfaces = GetComponentsInChildren<NavMeshSurface>();
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }

        if (objectSpawner) objectSpawner.StartSpawning(offset.x  * sizeX);
    }

  
    void CellPositions()
    {
        for (int y = 0; y < sizeY; y++) // Vertical/Column
        {
            for (int x = 0; x < sizeX; x++) // Horizontal/Row
            {
                // Create a new empty cell and add it to the list
                cells.Add(new Cell());

                // Convert a 2D array to a list and store the cordinates of the cell in itself
                cells[y * sizeX + x].cellPosition = new Vector2Int(y, x);       // row * width + col
            }
        }
    }

    /// <summary>
    /// Checks all possible neighboring cells and adds them to a list
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    void CheckNeighbors(int x, int y)
    {
        // Up
        if (y < sizeY - 1)
        {
            curCell.doors[0] = true;            // Open north door of current cell
        }

        // Right
        if (x < sizeX - 1)
        {
            // open east door
            curCell.doors[1] = true;            // Open east door of current cell
        }

        // Down
        if (y >= 1)
        {
            // open south door
            curCell.doors[2] = true;            // Open south door of current cell
        }

        // Left
        if (x >= 1)
        {
            // open west door
            curCell.doors[3] = true;            // Open west door of current cell
        }
    }

    /// <summary>
    /// Spawns the room prefabs in according with the cells
    /// </summary>
    void SpawnPrefabs()
    {
        GameObject newRoom;
        for (int y = 0; y < sizeY; y++)  // Row
        {
            for (int x = 0; x < sizeX; x++) // Column
            {
                // Spawn boss room in center of the size of the map
                if(x == (sizeX % 2) && y == (sizeY % 2))
                {
                    // Spawns the room in at the approriate offset
                    newRoom = Instantiate(bossRoomPrefab, new Vector3(y * offset.y, 0, x * offset.x), Quaternion.identity, transform);

                    // Adjusts the doors for each room according to the cell values
                    newRoom.GetComponent<RoomGeneration>().UpdateDoor(cells[y * sizeX + x].doors);

                    // Adjusts the names of the rooms to something not annoying to read
                    newRoom.name = "Room: (" + y.ToString() + ", " + x.ToString() + ")";
                }

                // Spawn normal room
                else
                {
                    // Spawns the room in at the approriate offset
                    newRoom = Instantiate(roomPrefab[Random.Range(0, roomPrefab.Count)], new Vector3(y * offset.y, 0, x * offset.x), Quaternion.identity, transform);

                    // Adjusts the doors for each room according to the cell values
                    newRoom.GetComponent<RoomGeneration>().UpdateDoor(cells[y * sizeX + x].doors);

                    // Adjusts the names of the rooms to something not annoying to read
                    newRoom.name = "Room: (" + y.ToString() + ", " + x.ToString() + ")";
                }
            }
        }
    }
}
