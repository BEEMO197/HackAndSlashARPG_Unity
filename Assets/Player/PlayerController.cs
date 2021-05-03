using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Character player;

    public Vector3 clickedPosition;
    public Vector3 velocity;

    public GameObject hoveredObject;
    public GameObject selectedObject;

    public Animator animator;

    public bool atkCrRunning = false;
    public Coroutine attackCoroutine;
    public AnimationClip attackClip;
    public float attackLength;

    public bool attacking = false;

    public float attackSpeed = 1.0f;

    public float clickTimer = 0.25f;
    public float timer = 0.0f;
    public bool isClicking = false;

    public bool isPaused = false;

    public GameObject InventoryGameObject;
    public GameObject PauseMenuGameObject;

    // Start is called before the first frame update
    void Start()
    {
        attackLength = attackClip.length;
    }

    // Update is called once per frame
    void Update()
    {
        onMouseHover();
        onMouseClick();
        move();
        useAbilities();
        openCloseInventory();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        switch (isPaused)
        {
            case true:
                Time.timeScale = 1.0f;
                isPaused = false;
                break;

            case false:
                Time.timeScale = 0.0f;
                isPaused = true;
                break;
        }

        PauseMenuGameObject.SetActive(isPaused);
    }


    private void onMouseHover()
    {
        Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit clickRayHit;

        Debug.DrawRay(clickRay.origin, clickRay.direction, Color.red, 1.0f);
        if (Physics.Raycast(clickRay, out clickRayHit))
        {
            if (clickRayHit.collider.CompareTag("Enemy"))
            {
                if (selectedObject == null || clickRayHit.collider.gameObject != selectedObject)
                {
                    hoveredObject = clickRayHit.collider.gameObject;
                    hoveredObject.GetComponentInParent<EnemyController>().changeState(selectedState.HOVERED);
                }
            }
            else
            {
                if (hoveredObject != null)
                {
                    hoveredObject.GetComponentInParent<EnemyController>().changeState(selectedState.UNSELECTED);
                    hoveredObject = null;
                }
            }

            player.SetMouseDirectionFromPlayer(clickRayHit.point);

        }
    }

    private void onMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            isClicking = true;
            timer += Time.deltaTime;
            if (timer >= clickTimer)
            {
                timer = 0.0f;
                Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit clickRayHit;

                if (Physics.Raycast(clickRay, out clickRayHit))
                {
                    if (clickRayHit.collider.CompareTag("Enemy"))
                    {
                        if (selectedObject != null && hoveredObject == null)
                        {
                            if (selectedObject.GetComponentInParent<EnemyController>().attackable)
                            {
                                if (!attacking)
                                {
                                    attackCoroutine = StartCoroutine(attack(attackSpeed, selectedObject));
                                }
                            }
                            else
                            {
                                clickedPosition = clickRayHit.point;
                                player.walktoTriggerObject.transform.position = new Vector3(clickedPosition.x, player.characterObject.transform.position.y, clickedPosition.z);
                                player.moveable = true;

                                lookAt(clickedPosition);
                            }
                        }
                        else
                        {
                            if(selectedObject != null)
                            {
                                selectedObject.GetComponentInParent<EnemyController>().changeState(selectedState.UNSELECTED);
                            }
                            selectedObject = clickRayHit.collider.gameObject;
                            selectedObject.GetComponentInParent<EnemyController>().changeState(selectedState.SELECTED);
                            hoveredObject = null;
                        }

                    }
                    else
                    {
                        if (attacking)
                        {
                            StopCoroutine(attackCoroutine);
                            attacking = false;
                            animator.SetBool("IsAttacking", attacking);
                        }

                        clickedPosition = clickRayHit.point;
                        lookAt(clickedPosition);
                        player.walktoTriggerObject.transform.position = new Vector3(clickedPosition.x, player.characterObject.transform.position.y, clickedPosition.z);
                        player.moveable = true;

                        if (selectedObject != null)
                        {
                            selectedObject.GetComponentInParent<EnemyController>().changeState(selectedState.UNSELECTED);
                            selectedObject = null;
                        }
                    }
                }
            }
        }
        else
        {
            if (isClicking)
            {
                isClicking = false;
                timer = 0.0f;
            }
        }

        if (!isClicking && timer != clickTimer)
            timer = clickTimer;
    }

    private void openCloseInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryGameObject.SetActive(!InventoryGameObject.activeSelf);
        }
    }

    private void lookAt(Vector3 positionToLookAt)
    {
        player.characterObject.transform.rotation = Quaternion.LookRotation(new Vector3(positionToLookAt.x, player.characterObject.transform.position.y, positionToLookAt.z) - player.characterObject.transform.position, player.characterObject.transform.up);
    }

    private void move()
    {
        if (selectedObject != null)
        {
            if (selectedObject.GetComponentInParent<EnemyController>().attackable == true)
            {
                player.moveable = false;
            }
        }

        if (player.moveable)
        {
            Vector3 moveTo = player.walktoTriggerObject.transform.position;
            velocity = (new Vector3(moveTo.x, player.characterObject.transform.position.y, moveTo.z) - (player.characterObject.transform.position)).normalized;
        }

        else
        {
            player.characterAgent.SetDestination(transform.position);
        }

        if (!player.dashing)
        {
            //AI Movement 
            player.characterAgent.SetDestination(clickedPosition);

            //Basic Click Movement 
            //player.rigidBody.velocity = velocity * player.speed;
        }

        animator.SetBool("IsMoving", player.moveable);
        animator.SetFloat("MoveSpeed", player.speed / 15f);
        if(animator.GetFloat("MoveSpeed") > 5.0f)
        {
            animator.SetFloat("MoveSpeed", 5.0f);
        }
    }

    private IEnumerator attack(float attackSpeed, GameObject attackedObject)
    {
        attacking = true;
        animator.SetBool("IsAttacking", attacking);
        animator.SetFloat("AttackSpeed", attackSpeed);

        while (attacking)
        {

            if (attackedObject == null)
            {
                StopCoroutine(attackCoroutine);
                attacking = false;
                animator.SetBool("IsAttacking", attacking);
            }

            yield return new WaitForSeconds(attackLength / attackSpeed / 2);

            if (attackedObject == null)
            {
                StopCoroutine(attackCoroutine);
                attacking = false;
                animator.SetBool("IsAttacking", attacking);
            }
            else
            {
                attackedObject.GetComponentInParent<EnemyController>().enemy.Damage(player.attackDamage, player);
            }

            yield return new WaitForSeconds(attackLength / attackSpeed / 2);

        }

        attacking = false;

        animator.SetBool("IsAttacking", attacking);
    }

    private void useAbilities()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.useAbility(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.useAbility(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.useAbility(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.useAbility(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.useAbility(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            player.useAbility(5);
        }
    }

}
