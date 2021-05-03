using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct connectedDirections
{
    public bool north;
    public bool east;
    public bool south;
    public bool west;
}

public enum connectedRoomDirection
{
    NORTH,
    EAST,
    SOUTH,
    WEST,
    COUNT
}

public class DungeonRoom : MonoBehaviour
{
    public DungeonRoomCell[,] dungeonRoomCells;
    public DungeonRoomCell[] doorwayCells;

    public connectedDirections connectedDirs;
    public GameObject dungeonRoomCellPrefab;

    public int DungeonRoomSizeX = 5;
    public int DungeonRoomSizeY = 5;

    public void Awake()
    {
        //DungeonRoomSizeX = Random.Range(3, 6);
        //DungeonRoomSizeY = Random.Range(3, 6);
        initDungeonCells(DungeonRoomSizeX, DungeonRoomSizeY);
    }

    public void initDungeonCells(int sizeX, int sizeY)
    {
        dungeonRoomCells = new DungeonRoomCell[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                dungeonRoomCells[x, y] = Instantiate(dungeonRoomCellPrefab).GetComponent<DungeonRoomCell>();
                dungeonRoomCells[x, y].transform.SetParent(transform);
                dungeonRoomCells[x, y].transform.position = new Vector3(transform.position.x - (sizeX * dungeonRoomCells[x, y].transform.localScale.x / 2) + dungeonRoomCells[x, y].transform.localScale.z / 2 + x * dungeonRoomCells[x, y].transform.localScale.x,
                                                                        transform.position.y,
                                                                        transform.position.z - (sizeY * dungeonRoomCells[x, y].transform.localScale.z / 2) + dungeonRoomCells[x, y].transform.localScale.z / 2 + y * dungeonRoomCells[x, y].transform.localScale.z);
            }
        }

        for (int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                dungeonRoomCells[x, y].setWalls(new DungeonRoomCell[4] 
                                                    {y == sizeY - 1 ? null : dungeonRoomCells[x, y + 1], 
                                                     x == sizeX - 1? null : dungeonRoomCells[x + 1, y], 
                                                     y == 0? null : dungeonRoomCells[x, y - 1],
                                                     x == 0 ? null : dungeonRoomCells[x - 1, y]});
            }
        }
    }

    public void attachRoom(int cellX, int cellY, connectedRoomDirection direction, DungeonRoom roomConnected)
    {
        
        // 0 = South, 1 = East, 2 = North, 3 = West

        switch(direction)
        {
            case connectedRoomDirection.NORTH:
                Debug.Log("North");
                dungeonRoomCells[cellX, cellY].setWalls(new DungeonRoomCell[4] {dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[0],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[1],
                                                                                roomConnected.dungeonRoomCells[Random.Range(0, roomConnected.DungeonRoomSizeX), 0],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[3]});

                break;

            case connectedRoomDirection.EAST:
                Debug.Log("East");
                dungeonRoomCells[cellX, cellY].setWalls(new DungeonRoomCell[4] {dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[0],
                                                                                roomConnected.dungeonRoomCells[0, Random.Range(0, roomConnected.DungeonRoomSizeY)],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[2],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[3]});
                break;

            case connectedRoomDirection.SOUTH:
                Debug.Log("South");
                dungeonRoomCells[cellX, cellY].setWalls(new DungeonRoomCell[4] {roomConnected.dungeonRoomCells[Random.Range(0, roomConnected.DungeonRoomSizeX), roomConnected.DungeonRoomSizeY - 1],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[1],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[2],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[3]});
                break;

            case connectedRoomDirection.WEST:
                Debug.Log("West");
                dungeonRoomCells[cellX, cellY].setWalls(new DungeonRoomCell[4] {dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[0],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[1],
                                                                                dungeonRoomCells[cellX, cellY].adjacentDungeonRoomCells[2],
                                                                                roomConnected.dungeonRoomCells[roomConnected.DungeonRoomSizeX - 1, Random.Range(0, roomConnected.DungeonRoomSizeY)]});
                break;

            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(DungeonRoomSizeX * dungeonRoomCellPrefab.transform.localScale.x, 0.5f, DungeonRoomSizeY * dungeonRoomCellPrefab.transform.localScale.z));
    }
}
