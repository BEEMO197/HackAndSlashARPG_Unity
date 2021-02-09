using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Ability", order = 1)]
public class Ability : ScriptableObject
{
    public bool abilityOnCooldown = false;

    [Header("Ability Effectors")]
    public float cooldown;
    public float castTime;
    public float duration;

    [Header("Ability Augments")]
    public bool healthAugment;
    public bool healAugment;
    public bool shieldAugment;
    public bool damageAugment;
    public bool speedAugment;
    public bool movementAugment;

    [Header("Ability Augment Values")]
    public float healthChange;
    public float healChange;
    public float shieldChange;
    public float damageChange;
    public float speedChange;
    public float movementChange;

    public IEnumerator useAbility(Character abilityCharacter)
    {
        Debug.Log("Checking if Ability on cooldown...");
        if (!abilityOnCooldown)
        {
            Debug.Log("Ability off cooldown...");
            yield return new WaitForSeconds(castTime);

            if (healthAugment)
            {
                abilityCharacter.MaxHeal(healthChange);
            }
            if (healAugment)
            {
                abilityCharacter.Heal(healChange);
            }
            if (shieldAugment)
            {
                abilityCharacter.IncreaseShield(shieldChange);
            }
            if (damageAugment)
            {
                abilityCharacter.AttackUp(damageChange);
            }
            if (speedAugment)
            {
                abilityCharacter.Accelerate(speedChange);
            }
            if (movementAugment)
            {
                abilityCharacter.Dash(movementChange);
            }
            abilityOnCooldown = true;
            yield return new WaitForSeconds(duration);
            resetStats(abilityCharacter);
        }
    }

    public IEnumerator putOnCooldown(Character abilityCharacter)
    {
        if (!abilityOnCooldown)
        {
            yield return new WaitForSeconds(cooldown);
            abilityOnCooldown = false;
        }
        yield return new WaitForSeconds(0.0f);
    }

    public void resetStats(Character abilityCharacter)
    {
        if (healthAugment)
        {
            abilityCharacter.MaxDamage(healthChange);
        }
        if (shieldAugment)
        {
            abilityCharacter.DecreaseShield(shieldChange);
        }
        if (damageAugment)
        {
            abilityCharacter.AttackDown(damageChange);
        }
        if (speedAugment)
        {
            abilityCharacter.Decelerate(speedChange);
        }
    }
}
