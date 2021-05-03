using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageingAbility", menuName = "Ability/DamageingAbility", order = 2)]
public class DamageAbility : Ability
{
    private float distanceTraveled;

    [Header("Damageing Ability Effectors")]
    // The amount of your damage to increase for the abilities damage, 100 will do 100% will do your damage, 200 is 200% of your damage, 50 is 50% of your damage, etc..
    public int DamagePercent;
    public bool AOE;
    public float AOERadius;
    public float AOETime;
    public Material AOEMaterial;
    public bool Targeted;
    public float Speed;

    // How far the Ability will go out before disapearing
    public float AbilityRange;
    public float AbilitySize;
    public Material AbilityMaterial;
    public characterClass[] useableClasses;

    public SphereCollider AttackCollider;

    // Contains the Attack Collider, so multiple of the same ability can be used
    public GameObject attackPrefab;

    public override IEnumerator useAbility(Character abilityCharacter)
    {
        Debug.Log("Running Damaging Ability");

        abilityCharacter.StartCoroutine(base.useAbility(abilityCharacter));
        castAbility(abilityCharacter);
        yield return new WaitForSeconds(0.0f);
    }

    public override IEnumerator putOnCooldown(Character abilityCharacter)
    {
        abilityCharacter.StartCoroutine(base.putOnCooldown(abilityCharacter));
        yield return new WaitForSeconds(0.0f);
    }

    public override void resetStats(Character abilityCharacter)
    {
        Debug.Log("Reseting Stats, Damage...");
        base.resetStats(abilityCharacter);
    }

    private void castAbility(Character abilityCharacter)
    {
        GameObject abilityObject = Instantiate(attackPrefab);
        abilityObject.GetComponent<DamageAbilityScript>().castingCharacter = abilityCharacter;
        abilityObject.GetComponent<DamageAbilityScript>().abilityUsed = this;   
        abilityObject.GetComponent<DamageAbilityScript>().SetUpAbilityObject();
    }
}
