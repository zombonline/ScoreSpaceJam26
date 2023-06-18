using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle, 
    Run,
    Wander,
}

public class EnemyMovement : MonoBehaviour
{

    Transform playerTransform;
    [SerializeField] State currentState = State.Idle, stateLastFrame;
    [SerializeField] LayerMask obstacleLayer, fenceLayer;
    bool collideForward, collideBackward, collideLeft, collideRight;

    [SerializeField] float runSpeed = 10f, wanderSpeed;
    Vector3 direction;

    float stateTimer = 0f;
    [SerializeField] float stateTimerMin = 4f, stateTimerMax = 8f;

    Animator animator;
    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        stateTimer -= Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        animator.SetBool("Walking", currentState != State.Idle);

        if (direction.x != 0)
        {
            animator.SetInteger(("x"), Mathf.RoundToInt(direction.x));
        }
        if (direction.z != 0)
        {
            animator.SetInteger(("z"), Mathf.RoundToInt(direction.z));
        }

        if (currentState == State.Wander)
        {
            Wander();
        }
        else if(currentState == State.Run)
        {
            Run();
        }
        else if(currentState == State.Idle)
        {
            Idle();
        }
    }

    private void Idle()
    {
        if (stateTimer <= 0)
        {
            //Enemy has just moved into the idle state, assign a new time before they change.
            if (stateLastFrame != currentState)
            {
                stateTimer = Random.Range(stateTimerMin, stateTimerMax);
            }
            //Enemy was already in idle state and time has ran out, move to new state.
            else
            {
                currentState = State.Wander;
            }
        }
        stateLastFrame = State.Idle;
    }

    private void Wander()
    {
        CheckForFenceAreaAndCollisions();
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction.normalized, 1f * Time.deltaTime * wanderSpeed);
        direction = UpdateDirectionBasedOnCollisions(direction);
        if (stateLastFrame != currentState)
        {
            stateTimer = Random.Range(stateTimerMin, stateTimerMax);
            GenerateRandomDirection(direction);
        }

        if (stateTimer <= 0)
        {
            currentState = State.Idle;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, 1f * Time.deltaTime * wanderSpeed);

        stateLastFrame = State.Wander;
    }

    private void Run()
    {
        CheckForCollisions();

        var directionAwayFromPlayer = (transform.position - playerTransform.position).normalized;
        direction = UpdateDirectionBasedOnCollisions(directionAwayFromPlayer);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction.normalized, 1f * Time.deltaTime * runSpeed);

        stateLastFrame = State.Run;
    }

    private Vector3 UpdateDirectionBasedOnCollisions(Vector3 directionToUpdate)
    {
        if (directionToUpdate.z > 0 && collideForward)
        {
            directionToUpdate = new Vector3(direction.x, 0, 0);
        }
        if (directionToUpdate.z < 0 && collideBackward)
        {
            directionToUpdate = new Vector3(direction.x, 0, 0);
        }
        if (directionToUpdate.x > 0 && collideRight)
        {
            directionToUpdate = new Vector3(0, 0, direction.z);
        }
        if (directionToUpdate.x < 0 && collideLeft)
        {
            directionToUpdate = new Vector3(0, 0, direction.z);
        }
        if(directionToUpdate == Vector3.zero)
        {
            directionToUpdate = GenerateRandomDirection(directionToUpdate);
        }
        return directionToUpdate.normalized;
    }

    private Vector3 GenerateRandomDirection(Vector3 directionToUpdate)
    {
        var randomNum = Random.Range(0, 8);
        if (randomNum == 0) { directionToUpdate = Vector3.forward; }
        else if (randomNum == 1) { directionToUpdate = Vector3.back; }
        else if (randomNum == 2) { directionToUpdate = Vector3.left; }
        else if (randomNum == 3) { directionToUpdate = Vector3.right; }
        else if (randomNum == 4) { directionToUpdate = new Vector3(1, 0, 1); }
        else if (randomNum == 5) { directionToUpdate = new Vector3(-1, 0, -1); }
        else if (randomNum == 6) { directionToUpdate = new Vector3(-1, 0, 1); }
        else if (randomNum == 7) { directionToUpdate = new Vector3(1, 0, -1); }

        return directionToUpdate;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player")) { currentState = State.Run; }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player")) { currentState = State.Wander; }
    }

    void CheckForCollisions()
    {
        collideForward = ShootRaycast(Vector3.forward, obstacleLayer);
        collideBackward = ShootRaycast(Vector3.back, obstacleLayer);
        collideLeft = ShootRaycast(Vector3.left, obstacleLayer);
        collideRight = ShootRaycast(Vector3.right, obstacleLayer);
    }

    void CheckForFenceAreaAndCollisions()
    {
        collideForward = ShootRaycast(Vector3.forward, fenceLayer) || ShootRaycast(Vector3.forward, obstacleLayer);
        collideBackward = ShootRaycast(Vector3.back, fenceLayer) || ShootRaycast(Vector3.back, obstacleLayer);
        collideLeft = ShootRaycast(Vector3.left, fenceLayer) || ShootRaycast(Vector3.left, obstacleLayer);
        collideRight = ShootRaycast(Vector3.right, fenceLayer) || ShootRaycast(Vector3.right, obstacleLayer);
    }

    bool ShootRaycast(Vector3 dir, LayerMask layer)
    {
        var rayHit = Physics.Raycast(transform.position, dir,5f, layer);
        return rayHit;
    }

}
