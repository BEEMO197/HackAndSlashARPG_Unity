using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum characterClass
{
    EVERY = -1,
    PEASANT,
    WARRIOR,
    WIZARD,
    ROGUE,
    CLASSCOUNT
}

public class Character : MonoBehaviour
{
    public const int MAX_LEVEL = 70;

    [Header("Components")]
    public GameObject walktoTriggerObject;
    public GameObject characterObject;
    public GameObject healthBarObject;
    public NavMeshAgent characterAgent;
    public Rigidbody rigidBody;

    public bool moveable = false;
    public bool dashing = false;
    public Vector3 mouseDirectionFromPlayer;

    [Header("Character Variables")]
    public characterClass currentCharacterClass;
    public float ExperiencePoints;
    public float ExperiencePointsForNextLevel;
    public int Level;
    public int statPoints;

    public int BaseStrength = 10;
    public int BaseDexterity = 10;
    public int BaseIntelligence = 10;
    public int BaseConstitution = 10;

    public int GearStrength;
    public int GearDexterity;
    public int GearIntelligence;
    public int GearConstitution;

    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public int Constitution;

    public float attackDamage;
    public float speed;
    public float maxHealth;
    public float health;
    public float healthRegenRate;
    public float shield;

    public float bonusSpeed;
    public float bonusDamage;
    public float bonusMaxHealth;

    public GearSlot[] gearSlots;

    public TMPro.TextMeshProUGUI levelText;
    public TMPro.TextMeshProUGUI statPointsText;

    public TMPro.TextMeshProUGUI experiencePointsText;
    public TMPro.TextMeshProUGUI experiencePointsNeededText;

    public TMPro.TextMeshProUGUI maxHealthValueText;
    public TMPro.TextMeshProUGUI attackDamageValueText;

    public TMPro.TextMeshProUGUI strengthValueText;
    public TMPro.TextMeshProUGUI dexterityValueText;
    public TMPro.TextMeshProUGUI constitutionValueText;
    public TMPro.TextMeshProUGUI intelligenceValueText;

    public GameObject statButtons;

    [Header("Debug Variables")]
    public bool dying;

    public AbilitySlot[] abilitySlots = new AbilitySlot[6];
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = characterObject.GetComponent<Rigidbody>();
        LevelUp();
        updateCoreStats();
        health = maxHealth;
    }

    private void Update()
    {
        updateCoreStats();
        updateHealth();
        regenHealth();
    }

    public void SetMouseDirectionFromPlayer(Vector3 mousePoint)
    {
        mouseDirectionFromPlayer = (new Vector3(mousePoint.x, characterObject.transform.position.y, mousePoint.z) - (characterObject.transform.position)).normalized;
    }

    public Vector3 GetMouseDirectionFromPlayer()
    {
        return mouseDirectionFromPlayer;
    }

    public void increaseExperiencePoints(float experiencePointsGained)
    {
        ExperiencePoints += experiencePointsGained;
        experiencePointsText.text = ((int)ExperiencePoints).ToString();

        while (ExperiencePoints >= ExperiencePointsForNextLevel)
        { 
            if (ExperiencePoints >= ExperiencePointsForNextLevel)
            {
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        if (Level < MAX_LEVEL)
        {
            Level++;

            statPoints++;
            if (levelText != null)
            {
                levelText.text = Level.ToString();
                statButtons.SetActive(true);
                statPointsText.text = statPoints.ToString();
            }
            ExperiencePoints -= ExperiencePointsForNextLevel;
            if (ExperiencePoints <= 0)
                ExperiencePoints = 0;

            ExperiencePointsForNextLevel = Mathf.Pow(Level, 3.5f) + 150;

            if (experiencePointsText != null)
            {
                experiencePointsText.text = ExperiencePoints.ToString();
                experiencePointsNeededText.text = ExperiencePointsForNextLevel.ToString();
            }
            updateCoreStats();
            health = maxHealth;
        }
    }

    // Increase or Decrease the Strength Base Stat Auto applies changes to other stats based on class.
    public void ChangeStrength(int strengthChange)
    {
        BaseStrength += strengthChange;
        if (strengthChange == 1)
        {
            statPoints--;
            statPointsText.text = statPoints.ToString();
            if(statPoints <= 0)
                statButtons.SetActive(false);
        }

        if (BaseStrength <= 0)
            BaseStrength = 0;

        updateCoreStats();
    }

    // Increase or Decrease the Dexterity Base Stat Auto applies changes to other stats based on class.
    public void ChangeDexterity(int dexterityChange)
    {
        BaseDexterity += dexterityChange;
        if (dexterityChange == 1)
        {
            statPoints--;
            statPointsText.text = statPoints.ToString();
            if (statPoints <= 0)
                statButtons.SetActive(false);
        }

        if (BaseDexterity <= 0)
            BaseDexterity = 0;
        updateCoreStats();
    }

    // Increase or Decrease the Constitution Base Stat Auto applies changes to other stats based on class.
    public void ChangeConstitution(int constitutionChange)
    {
        BaseConstitution += constitutionChange;
        if (constitutionChange == 1)
        {
            statPoints--;
            statPointsText.text = statPoints.ToString();
            if (statPoints <= 0)
                statButtons.SetActive(false);
        }

        if (BaseConstitution <= 0)
            BaseConstitution = 0;
        updateCoreStats();
    }

    // Increase or Decrease the Intelligence Base Stat Auto applies changes to other stats based on class.
    public void ChangeIntelligence(int intelligenceChange)
    {
        BaseIntelligence += intelligenceChange;
        if (intelligenceChange == 1)
        {
            statPoints--;
            statPointsText.text = statPoints.ToString();
            if (statPoints <= 0)
                statButtons.SetActive(false);
        }

        if (BaseIntelligence <= 0)
            BaseIntelligence = 0;
        updateCoreStats();
    }

    public void regenHealth()
    {
        if (health < maxHealth)
        {
            health += healthRegenRate * Time.deltaTime;
            if (health >= maxHealth)
                health = maxHealth;
        }
    }

    public void updateCoreStats()
    {
        UpdateGearslotStats();
        switch (currentCharacterClass)
        {
            case characterClass.WARRIOR:

                // Warrior Changes
                attackDamage = Level * 2.5f * Strength;
                speed = 0.75f * Dexterity;
                maxHealth = Level * 50.0f + (50.0f * Constitution);
                healthRegenRate = 0.2f * Constitution;

                break;
            case characterClass.WIZARD:

                // Wizzard Changes
                attackDamage = Level * 2.5f * Intelligence;
                speed = 0.9f * Dexterity;
                maxHealth = Level * 10.0f + (10.0f * Constitution);
                healthRegenRate = 0.05f * Constitution;

                break;
            case characterClass.ROGUE:

                // Rogue Changes
                attackDamage = Level * 2.5f * Dexterity;
                speed = 1.10f * Dexterity;
                maxHealth = Level * 20.0f + (20.0f * Constitution);
                healthRegenRate = 0.15f * Constitution;

                break;
            default:

                // Peasant Changes
                attackDamage = Level * 1.5f * Strength;
                speed = 0.75f * Dexterity;
                maxHealth = Level * 15.0f + (15.0f * Constitution);
                healthRegenRate = 0.1f * Constitution;

                break;

        }

        characterAgent.speed = speed + bonusSpeed;
        attackDamage += bonusDamage;
        maxHealth += bonusMaxHealth;

        if (maxHealthValueText != null)
        {
            maxHealthValueText.text = maxHealth.ToString();
            attackDamageValueText.text = attackDamage.ToString();
            strengthValueText.text = Strength.ToString();
            dexterityValueText.text = Dexterity.ToString();
            constitutionValueText.text = Constitution.ToString();
            intelligenceValueText.text = Intelligence.ToString();
        }
    }

    public void UpdateGearslotStats()
    {
        GearStrength = 0;
        GearDexterity = 0;
        GearConstitution = 0;
        GearIntelligence = 0;

        foreach(GearSlot gearSlot in gearSlots)
        {
            if (gearSlot.hasItemInSlot())
            {
                switch (gearSlot.itemInSlot.itemType)
                {
                    case ItemType.WEAPON:
                        GearStrength += (gearSlot.itemInSlot as Weapon).StrengthBonus;
                        GearDexterity += (gearSlot.itemInSlot as Weapon).DexterityBonus;
                        GearConstitution += (gearSlot.itemInSlot as Weapon).ConstitutionBonus;
                        GearIntelligence += (gearSlot.itemInSlot as Weapon).IntelligenceBonus;
                        break;

                    case ItemType.ARMOR:
                        GearStrength += (gearSlot.itemInSlot as Armor).StrengthBonus;
                        GearDexterity += (gearSlot.itemInSlot as Armor).DexterityBonus;
                        GearConstitution += (gearSlot.itemInSlot as Armor).ConstitutionBonus;
                        GearIntelligence += (gearSlot.itemInSlot as Armor).IntelligenceBonus;
                        break;

                    case ItemType.ACCESSORY:
                        GearStrength += (gearSlot.itemInSlot as Accessory).StrengthBonus;
                        GearDexterity += (gearSlot.itemInSlot as Accessory).DexterityBonus;
                        GearConstitution += (gearSlot.itemInSlot as Accessory).ConstitutionBonus;
                        GearIntelligence += (gearSlot.itemInSlot as Accessory).IntelligenceBonus;
                        break;
                    default:
                        break;
                }
            }
        }

        Strength = GearStrength + BaseStrength;
        Dexterity = GearDexterity + BaseDexterity;
        Constitution = GearConstitution + BaseConstitution;
        Intelligence = GearIntelligence + BaseIntelligence;
    }

    public void AttackUp(float damageIncrease)
    {
        bonusDamage += damageIncrease;
        updateCoreStats();
    }

    public void AttackDown(float damageDecrease)
    {
        bonusDamage -= damageDecrease;
        if (bonusDamage <= 0.0f)
            bonusDamage = 0.0f;
        updateCoreStats();
    }

    public void Accelerate(float accelerationAmount)
    {
        bonusSpeed += accelerationAmount;
        updateCoreStats();
    }

    public void Decelerate(float decelerationAmount)
    {
        bonusSpeed -= decelerationAmount;
        if (bonusSpeed <= 0.0f)
            bonusSpeed = 0.0f;
        updateCoreStats();
    }

    public void Dash(float dashPower)
    {
        Vector3 dashPosition = characterObject.transform.forward * dashPower;
        walktoTriggerObject.transform.position = rigidBody.transform.position + dashPosition / 10.0f;

        rigidBody.velocity = dashPosition;

        dashing = true;
        StartCoroutine(dashChange());
        updateCoreStats();
    }

    private IEnumerator dashChange()
    {
        yield return new WaitForSeconds(0.05f);
        rigidBody.velocity = Vector3.zero;
        dashing = false;
        moveable = true;
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
            health = maxHealth;

        updateHealth();
        updateCoreStats();
    }

    public void Damage(float damage, Character attackingCharacter = null)
    {
        if (!dying)
        {
            health -= damage;
            if (health <= 0)
            {
                if (attackingCharacter != null)
                    attackingCharacter.increaseExperiencePoints(ExperiencePointsForNextLevel / 2.0f);

                kill();
            }
            updateHealth();
        }
        updateCoreStats();
    }

    public void IncreaseShield(float shieldAmount)
    {
        shield += shieldAmount;
        if (shield >= maxHealth)
            shield = maxHealth;
        updateCoreStats();
    }

    public void DecreaseShield(float shieldAmount)
    {
        shield -= shieldAmount;
        if (shield <= 0)
            shield = 0.0f;
        updateCoreStats();
    }

    public void MaxHeal(float maxHealAmount)
    {
        float healthPercent = health / maxHealth;
        bonusMaxHealth += maxHealAmount;
        maxHealth += bonusMaxHealth;
        health += bonusMaxHealth * healthPercent;

        updateHealth();
        updateCoreStats();
    }

    public void MaxDamage(float maxDamageAmount)
    {
        bonusMaxHealth -= maxDamageAmount;

        maxHealth -= maxDamageAmount;
        if (bonusMaxHealth <= 0.0f)
            bonusMaxHealth = 0.0f;

        if (health >= maxHealth)
            health = maxHealth;

        updateHealth();
        updateCoreStats();
    }

    private void kill()
    {
        dying = true;
        StartCoroutine(killCharacter());
    }

    private IEnumerator killCharacter()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    private void updateHealth()
    {
        Vector3 newHealthBarScale = healthBarObject.transform.localScale;

        newHealthBarScale.x = health / maxHealth;
        if (newHealthBarScale.x <= 0)
            newHealthBarScale.x = 0;
        healthBarObject.transform.localScale = newHealthBarScale;
    }

    public void useAbility(int abilityNumberSlot)
    {
        if (!abilitySlots[abilityNumberSlot].abilityInSlot.abilityOnCooldown)
        {
            StartCoroutine(abilitySlots[abilityNumberSlot].abilityInSlot.useAbility(this));
            Debug.Log("using an ability...");

            StartCoroutine(abilitySlots[abilityNumberSlot].abilityInSlot.putOnCooldown(this));
            Debug.Log("cooldown ability...");

        }
    }
}
