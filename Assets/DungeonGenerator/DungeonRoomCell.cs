using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomCell : MonoBehaviour
{
    public GameObject dungeonRoomCellObject;

    public GameObject northWallObject;
    public GameObject eastWallObject;
    public GameObject southWallObject;
    public GameObject westWallObject;

    // 0 = North, 1 = East, 2 = South, 3 = West
    public DungeonRoomCell[] adjacentDungeonRoomCells = new DungeonRoomCell[4];

    public void disableWalls()
    {
        int index = 0;
        foreach(DungeonRoomCell drc in adjacentDungeonRoomCells)
        {
            if(drc != null)
            {
                switch(index)
                {
                    case 0:
                        northWallObject.SetActive(false);
                        break;

                    case 1:
                        eastWallObject.SetActive(false);
                        break;

                    case 2:
                        southWallObject.SetActive(false);
                        break;

                    case 3:
                        westWallObject.SetActive(false);
                        break;

                    default:
                        break;
                }
            }
            index++;
        }
    }

    public void setWalls(DungeonRoomCell[] dungeonRoomCells)
    {
        adjacentDungeonRoomCells = dungeonRoomCells;
        disableWalls();
    }

}
