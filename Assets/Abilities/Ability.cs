using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Ability", order = 1)]
public class Ability : ScriptableObject
{
    public bool abilityOnCooldown = false;
    public float coolDownNumber = 0.0f;

    [Header("Ability UI")]
    public Sprite abilityIcon;
    public AudioClip abilitySoundEffect;

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

    private void OnDisable()
    {
        abilityOnCooldown = false;
    }

    public virtual IEnumerator useAbility(Character abilityCharacter)
    {
        Debug.Log("Checking if Ability on cooldown...");
        if (!abilityOnCooldown)
        {
            Debug.Log("Ability off cooldown...");
            Debug.Log("Wait for Cast Time...");
            abilityOnCooldown = true;
            yield return new WaitForSeconds(castTime);
            Debug.Log("Cast Time Complete...");
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
            abilityCharacter.audioSource.PlayOneShot(abilitySoundEffect);
            yield return new WaitForSeconds(duration);
            resetStats(abilityCharacter);
        }
    }

    public virtual IEnumerator putOnCooldown(Character abilityCharacter)
    {
        Debug.Log("Put On Cooldown Ability...");
        yield return new WaitForSecondsRealtime(cooldown);
        abilityOnCooldown = false;
    }

    public virtual void resetStats(Character abilityCharacter)
    {
        Debug.Log("Reseting Stats, Ability...");
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
