using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public Dungeon randomizedDungeon;

    public int numberOfDungeonRooms = 5;

    private void Awake()
    {
        GenerateRandomDungeon();
    }

    public void GenerateRandomDungeon()
    {
        randomizedDungeon.initDungeonRooms(numberOfDungeonRooms);
    }
}
