using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public GameObject dungeonRoomPrefab;

    public DungeonRoom[] dungeonRooms;
    public DungeonRoom baseDungeonRoom;

    public void initDungeonRooms(int numberOfRooms = 5)
    {
        dungeonRooms = new DungeonRoom[numberOfRooms];
        dungeonRooms[0] = Instantiate(dungeonRoomPrefab).GetComponent<DungeonRoom>();

        dungeonRooms[0].transform.SetParent(transform);
        dungeonRooms[0].transform.position = transform.position;

        baseDungeonRoom = dungeonRooms[0];

        for (int i = 1; i < numberOfRooms; i++)
        {
            dungeonRooms[i] = Instantiate(dungeonRoomPrefab).GetComponent<DungeonRoom>();

            bool valid = false;
            connectedRoomDirection crd;
            int roomIndexToAttach = 0;
            do
            {
                crd = (connectedRoomDirection)Random.Range(0, (int)connectedRoomDirection.COUNT);
                roomIndexToAttach = Random.Range(0, i);

                if (dungeonRooms[i] != dungeonRooms[roomIndexToAttach])
                {

                    dungeonRooms[i].transform.SetParent(dungeonRooms[roomIndexToAttach].transform);
                    dungeonRooms[i].transform.position = dungeonRooms[roomIndexToAttach].transform.position;

                    // Testing
                    //crd = connectedRoomDirection.NORTH;
                    switch (crd)
                    {
                        case connectedRoomDirection.NORTH:
                            if (!dungeonRooms[roomIndexToAttach].connectedDirs.north)
                            {
                                valid = true;
                                dungeonRooms[roomIndexToAttach].connectedDirs.north = true;
                                dungeonRooms[i].connectedDirs.south = true;

                                int roomIndexRandX = Random.Range(0, dungeonRooms[roomIndexToAttach].DungeonRoomSizeX);
                                int connectedRoomIndexRandX = Random.Range(0, dungeonRooms[i].DungeonRoomSizeX);

                                dungeonRooms[roomIndexToAttach].attachRoom(roomIndexRandX, 0, crd, dungeonRooms[i]);
                                dungeonRooms[i].attachRoom(connectedRoomIndexRandX, dungeonRooms[i].DungeonRoomSizeY - 1, connectedRoomDirection.SOUTH, dungeonRooms[roomIndexToAttach]);

                                float xDiff = dungeonRooms[roomIndexToAttach].dungeonRoomCells[roomIndexRandX, 0].transform.position.x
                                                - dungeonRooms[i].dungeonRoomCells[connectedRoomIndexRandX, dungeonRooms[i].DungeonRoomSizeY - 1].transform.position.x;

                                dungeonRooms[i].transform.position -= new Vector3(-xDiff, 0, dungeonRooms[roomIndexToAttach].DungeonRoomSizeY * dungeonRooms[roomIndexToAttach].dungeonRoomCellPrefab.transform.localScale.z);
                            }
                            break;

                        case connectedRoomDirection.EAST:
                            if (!dungeonRooms[roomIndexToAttach].connectedDirs.east)
                            {
                                valid = true;
                                dungeonRooms[roomIndexToAttach].connectedDirs.east = true;
                                dungeonRooms[i].connectedDirs.west = true;

                                int roomIndexRandY = Random.Range(0, dungeonRooms[roomIndexToAttach].DungeonRoomSizeY);
                                int connectedRoomIndexRandY = Random.Range(0, dungeonRooms[i].DungeonRoomSizeY);

                                dungeonRooms[roomIndexToAttach].attachRoom(dungeonRooms[roomIndexToAttach].DungeonRoomSizeX - 1, roomIndexRandY, crd, dungeonRooms[i]);
                                dungeonRooms[i].attachRoom(0, connectedRoomIndexRandY, connectedRoomDirection.WEST, dungeonRooms[roomIndexToAttach]);

                                float yDiff = dungeonRooms[roomIndexToAttach].dungeonRoomCells[dungeonRooms[roomIndexToAttach].DungeonRoomSizeX - 1, roomIndexRandY].transform.position.z
                                                - dungeonRooms[i].dungeonRoomCells[0, connectedRoomIndexRandY].transform.position.z;

                                dungeonRooms[i].transform.position += new Vector3(dungeonRooms[roomIndexToAttach].DungeonRoomSizeX * dungeonRooms[roomIndexToAttach].dungeonRoomCellPrefab.transform.localScale.x, 0, yDiff);
                            }   
                            break;

                        case connectedRoomDirection.SOUTH:
                            if (!dungeonRooms[roomIndexToAttach].connectedDirs.south)
                            {
                                valid = true;
                                dungeonRooms[roomIndexToAttach].connectedDirs.south = true;
                                dungeonRooms[i].connectedDirs.north = true;

                                int roomIndexRandX = Random.Range(0, dungeonRooms[roomIndexToAttach].DungeonRoomSizeX);
                                int connectedRoomIndexRandX = Random.Range(0, dungeonRooms[i].DungeonRoomSizeX);

                                dungeonRooms[roomIndexToAttach].attachRoom(roomIndexRandX, dungeonRooms[roomIndexToAttach].DungeonRoomSizeY - 1, crd, dungeonRooms[i]);
                                dungeonRooms[i].attachRoom(connectedRoomIndexRandX, 0, connectedRoomDirection.NORTH, dungeonRooms[roomIndexToAttach]);

                                float xDiff = dungeonRooms[roomIndexToAttach].dungeonRoomCells[roomIndexRandX, dungeonRooms[roomIndexToAttach].DungeonRoomSizeY - 1].transform.position.x
                                                - dungeonRooms[i].dungeonRoomCells[connectedRoomIndexRandX, 0].transform.position.x;

                                dungeonRooms[i].transform.position += new Vector3(xDiff, 0, dungeonRooms[roomIndexToAttach].DungeonRoomSizeY * dungeonRooms[roomIndexToAttach].dungeonRoomCellPrefab.transform.localScale.z);
                            }
                            break;

                        case connectedRoomDirection.WEST:
                            if (!dungeonRooms[roomIndexToAttach].connectedDirs.west)
                            {
                                valid = true;
                                dungeonRooms[roomIndexToAttach].connectedDirs.west = true;
                                dungeonRooms[i].connectedDirs.east = true;

                                int roomIndexRandY = Random.Range(0, dungeonRooms[roomIndexToAttach].DungeonRoomSizeY);
                                int connectedRoomIndexRandY = Random.Range(0, dungeonRooms[i].DungeonRoomSizeY);

                                dungeonRooms[roomIndexToAttach].attachRoom(0, roomIndexRandY, crd, dungeonRooms[i]);
                                dungeonRooms[i].attachRoom(dungeonRooms[i].DungeonRoomSizeX - 1, connectedRoomIndexRandY, connectedRoomDirection.EAST, dungeonRooms[roomIndexToAttach]);

                                float yDiff = dungeonRooms[roomIndexToAttach].dungeonRoomCells[0, roomIndexRandY].transform.position.z
                                                - dungeonRooms[i].dungeonRoomCells[dungeonRooms[i].DungeonRoomSizeX - 1, connectedRoomIndexRandY].transform.position.z;

                                dungeonRooms[i].transform.position -= new Vector3(dungeonRooms[roomIndexToAttach].DungeonRoomSizeX * dungeonRooms[roomIndexToAttach].dungeonRoomCellPrefab.transform.localScale.x, 0, -yDiff);
                            }
                            break;

                        default:
                            break;
                    }
                }
            } while (!valid);

            
        }
    }
}
