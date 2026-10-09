using System;
using System.Collections;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
    
    // In the editor, set which cardinal direction the push block is allowed to be pushed
    [SerializeField] bool isPushable = false;
    private bool hasBeenPushed = false;
    private Vector3 initPos;

    // Choose which directions to move in inspector
    [SerializeField] bool canMoveNorth = false;
    [SerializeField] bool canMoveWest = false;
    [SerializeField] bool canMoveEast = false;
    [SerializeField] bool canMoveSouth = false;

    private CoroutineUtiilities coroutineUtiilities;
    
    void Start()
    {
        coroutineUtiilities = new CoroutineUtiilities();
        initPos = transform.position;
    }

    void Update()
    {
        // When block is moved, a floor tile needs to be beneath it.
        // This will make sure the floor tile stays in place when the parent tile moves
        Transform floorTile = transform.GetChild(0);
        floorTile.position = initPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // find the direction to push the block and send it to the move block function
            Vector3 push_direction = gameObject.transform.position - collision.transform.position;
            push_direction = push_direction.normalized;
            MoveBlock(push_direction);
        }
    }

    void MoveBlock(Vector3 push_direction)
    {
        // exit early if block cannot be pushed
        if (!isPushable || hasBeenPushed || coroutineUtiilities.isRunningCoroutine())
        {
            return;
        }

        // only move if it matches an allowed direction
        if ((canMoveNorth && push_direction == Vector3.up) ||
            (canMoveWest && push_direction == Vector3.left) || 
            (canMoveEast && push_direction == Vector3.right) || 
            (canMoveSouth && push_direction == Vector3.down))
        {
            // start coroutine to move block in push direction
            hasBeenPushed = true;
            Vector3 finalPos = transform.position + push_direction;
            IEnumerator BlockTransition = coroutineUtiilities.moveObjectOverTime(transform, initPos, finalPos, 1.0f);
            StartCoroutine(BlockTransition);
        }


        
    }



}
