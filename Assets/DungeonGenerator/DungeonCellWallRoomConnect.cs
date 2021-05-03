using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCellWallRoomConnect : MonoBehaviour
{
    public DungeonRoomCell connectedCell;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if(collision.collider.CompareTag("DungeonGround"))
        {
            DungeonRoomCell DRG = collision.gameObject.GetComponent<DungeonRoomCell>();

            if(connectedCell != DRG)
                gameObject.SetActive(false);
        }
    }
}
