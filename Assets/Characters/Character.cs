using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("Components")]
    public GameObject walktoTriggerObject;
    public GameObject characterObject;
    public GameObject healthBarObject;
    public NavMeshAgent characterAgent;
    public Rigidbody rigidBody;

    public bool moveable = false;
    public bool dashing = false;

    [Header("Character Variables")]
    public float attackDamage;
    public float speed;
    public float maxHealth;
    public float health;
    public float shield;

    [Header("Debug Variables")]
    public bool dying;

    public Ability[] abilities = new Ability[6];
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = characterObject.GetComponent<Rigidbody>();
        health = maxHealth;
    }

    public void AttackUp(float damageIncrease)
    {
        attackDamage += damageIncrease;
    }

    public void AttackDown(float damageDecrease)
    {
        attackDamage -= damageDecrease;
        if (attackDamage <= 0.1f)
            attackDamage = 0.1f;
    }

    public void Accelerate(float accelerationAmount)
    {
        speed += accelerationAmount;
    }

    public void Decelerate(float decelerationAmount)
    {
        speed -= decelerationAmount;
        if (speed <= 1.0f)
            speed = 1.0f;
    }

    public void Dash(float dashPower)
    {
        Vector3 dashPosition = characterObject.transform.forward * dashPower;
        walktoTriggerObject.transform.position = rigidBody.transform.position + dashPosition / 10.0f;

        rigidBody.velocity = dashPosition;

        dashing = true;
        StartCoroutine(dashChange());
    }

    private IEnumerator dashChange()
    {
        yield return new WaitForSeconds(0.05f);
        dashing = false;
        moveable = true;
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
            health = maxHealth;
    }

    public void Damage(float damage)
    {
        if (!dying)
        {
            health -= damage;
            if (health <= 0)
            {
                kill();
            }
            updateHealth();
        }
    }

    public void IncreaseShield(float shieldAmount)
    {
        shield += shieldAmount;
        if (shield >= maxHealth)
            shield = maxHealth;
    }

    public void DecreaseShield(float shieldAmount)
    {
        shield -= shieldAmount;
        if (shield <= 0)
            shield = 0.0f;
    }

    public void MaxHeal(float maxHealAmount)
    {
        float healthPercent = health / maxHealth;
        maxHealth += maxHealAmount;
        health = maxHealth * healthPercent;
    }

    public void MaxDamage(float maxDamageAmount)
    {
        maxHealth -= maxDamageAmount;
        if (maxHealth <= 1.0f)
            maxHealth = 1.0f;

        if(health >= maxHealth)
            health = maxHealth;
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
        healthBarObject.transform.localScale = newHealthBarScale;
    }

    private void updatePlayerHealth()
    {

    }

    public void useAbility(int abilityNumberSlot)
    {
        StartCoroutine(abilities[abilityNumberSlot].useAbility(this));
        Debug.Log("using an ability...");

        StartCoroutine(abilities[abilityNumberSlot].putOnCooldown(this));
        Debug.Log("cooldown ability...");
    }
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
