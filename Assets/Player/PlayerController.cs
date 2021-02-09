using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Character player;

    public Vector3 clickedPosition;
    public Vector3 velocity;

    public GameObject hoveredObject;
    public GameObject selectedObject;

    public Animator animator;

    public bool attacking = false;

    public float attackSpeed = 1.0f;

    public float clickTimer = 0.25f;
    public float timer = 0.0f;
    public bool isClicking = false; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        onMouseHover();
        onMouseClick();
        move();
        useAbilities();
    }

    private void onMouseHover()
    {
       
        Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit clickRayHit;

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
        }
    }

    private void onMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
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
                                    StartCoroutine(attack(attackSpeed, selectedObject));
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
            Vector3 moveto = player.walktoTriggerObject.transform.position;
            //player.characterAgent.Move(clickedPosition);
            velocity = (new Vector3(moveto.x, player.characterObject.transform.position.y, moveto.z) - player.characterObject.transform.position).normalized;
        }

        else
        {
            velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }

        if(!player.dashing)
            player.rigidBody.velocity = velocity * player.speed;
    }
    private IEnumerator attack(float attackSpeed, GameObject attackedObject)
    {
        attacking = true;
        animator.SetBool("IsAttacking", attacking);

        attackedObject.GetComponentInParent<EnemyController>().enemy.Damage(player.attackDamage);

        yield return new WaitForSeconds(attackSpeed);
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
