using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DamageAbilityScript : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 direction;
    public float distanceTraveled;
    public Character castingCharacter;
    public DamageAbility abilityUsed;
    public bool setToDie = false;

    public void SetUpAbilityObject()
    {
        transform.position = castingCharacter.characterObject.transform.position;
        startPosition = transform.position;
        direction = castingCharacter.GetMouseDirectionFromPlayer() * abilityUsed.Speed;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * abilityUsed.AbilitySize;
        GetComponent<Renderer>().material = abilityUsed.AbilityMaterial;
    }

    private void Update()
    {
        if (distanceTraveled >= abilityUsed.AbilityRange)
        {
            if (!setToDie)
            {
                if (abilityUsed.AOE)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * abilityUsed.AOERadius;
                    GetComponent<Renderer>().material = abilityUsed.AOEMaterial;
                    StartCoroutine(aoeDestroyTimer());
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            transform.position += direction;
            distanceTraveled = Vector3.Distance(startPosition, transform.position);
        }
    }

    private IEnumerator aoeDestroyTimer()
    {
        setToDie = true;
        yield return new WaitForSeconds(abilityUsed.AOETime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<EnemyController>().enemy.Damage(castingCharacter.attackDamage * (abilityUsed.DamagePercent / 100.0f));
        }
    }
}
