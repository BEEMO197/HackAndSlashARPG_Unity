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

    public Character detectedCharacter;
    public Character attackableCharacter;

    public Vector3 velocity;

    public Animator animator;

    public Coroutine attackCoroutine;
    public AnimationClip attackClip;
    public float attackLength;

    public bool detectedPlayer = false;
    public bool withinAttackRange = false;

    public bool attacking = false;

    public float attackSpeed = 1.0f;

    public bool attackable;

    public SphereCollider attackTrigger;
    private void Start()
    {
        attackLength = attackClip.length;
        enemy.characterAgent.stoppingDistance = attackTrigger.radius;
    }

    private void Update()
    {
        move();
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

    private void lookAt(Vector3 positionToLookAt)
    {
        enemy.characterObject.transform.rotation = Quaternion.LookRotation(new Vector3(positionToLookAt.x, enemy.characterObject.transform.position.y, positionToLookAt.z) - enemy.characterObject.transform.position, enemy.characterObject.transform.up);
    }


    private void move()
    {
        // if Character is in detection radius, but not attackRadius, move, else if in both, Attack, else do nothing / Patrol or something
        if (detectedCharacter != null && attackableCharacter == null)
        {
            if(attacking)
            {
                StopCoroutine(attackCoroutine);
            }
            enemy.characterAgent.SetDestination(detectedCharacter.rigidBody.transform.position);
            // Apply Movement
        }
        else if (detectedCharacter != null && attackableCharacter != null)
        {
            if(!attacking)
            {
                attackCoroutine = StartCoroutine(attack(attackSpeed, attackableCharacter));
            }
            // Attack
        }
        else
        {
            enemy.characterAgent.SetDestination(transform.position);
            // Don't move / Patrol or idle or something
        }
    }

    private IEnumerator attack(float attackSpeed, Character attackedCharacter)
    {
        attacking = true;
        animator.SetBool("IsAttacking", attacking);
        animator.SetFloat("AttackSpeed", attackSpeed);

        yield return new WaitForSeconds(attackLength / attackSpeed / 2);

        attackedCharacter.Damage(enemy.attackDamage, enemy);

        yield return new WaitForSeconds(attackLength / attackSpeed / 2);

        attacking = false;

        animator.SetBool("IsAttacking", attacking);
    }

}
