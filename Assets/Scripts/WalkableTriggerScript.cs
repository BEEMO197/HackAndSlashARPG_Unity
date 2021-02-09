using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableTriggerScript : MonoBehaviour
{
    public PlayerController pc;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pc.player.moveable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pc.player.moveable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pc.player.moveable = true;
        }
    }
}
