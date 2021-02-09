using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableTriggerScript : MonoBehaviour
{
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        foreach (EnemyController ec in enemyControllers)
    //        {
    //            if (ec.attackable != true)
    //            {
    //                if (other.gameObject.GetComponentInParent<EnemyController>() == ec)
    //                {
    //                    ec.attackable = true;
    //                }
    //            }
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRiggerEntered");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Trggersd");
            other.gameObject.GetComponentInParent<EnemyController>().attackable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<EnemyController>().attackable = false;
        }
    }
}
