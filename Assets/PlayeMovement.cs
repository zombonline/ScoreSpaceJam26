using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMovement : MonoBehaviour
{

    [SerializeField] float speed;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        transform.position += movementVector * Time.deltaTime * speed;

        animator.SetBool("Walking", movementVector != Vector3.zero);
        
        if(movementVector.x != 0)
        {
            animator.SetInteger(("x"), Mathf.RoundToInt(movementVector.x));
        }
        if (movementVector.z != 0)
        {
            animator.SetInteger(("z"), Mathf.RoundToInt(movementVector.z));
        }

    }


}
