using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum selectedState
{
    UNSELECTED,
    HOVERED,
    SELECTED,
};

public class EnemyController : MonoBehaviour
{
    public Character enemy;
    public selectedState currentState = selectedState.UNSELECTED;

    public bool attackable;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeState(selectedState newState)
    {
        currentState = newState;

        switch(currentState)
        {
            case selectedState.UNSELECTED:
                enemy.characterObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
                break;

            case selectedState.HOVERED:
                enemy.characterObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.grey);
                break;

            case selectedState.SELECTED:
                enemy.characterObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
                break;

            default:
                break;
        };
    }
}
