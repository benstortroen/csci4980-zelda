using System;
using System.Collections;
using System.Collections.Generic; // for typed Queues
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RoomTransition : MonoBehaviour, IReceiverComponent
{
    CoroutineUtiilities coroutineUtiilities;
    Transform _transform;
    MessengerComponent messenger;
    Queue<ArrowInputMessage> messageQueue;

    static int roomWidth = 16;
    static int roomHeight = 11;
    Vector3 rightRoomVector = roomWidth * Vector3.right;
    Vector3 leftRoomVector = roomWidth * Vector3.left;
    Vector3 upRoomVector = roomHeight * Vector2.up;
    Vector3 downRoomVector = roomHeight * Vector2.down;
    [SerializeField] float transitionDuration = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = gameObject.transform;
        coroutineUtiilities = new CoroutineUtiilities();

        messenger = gameObject.GetComponent<MessengerComponent>();
        messenger.subscribe(this);

        // Queue transition messages
        messageQueue = new Queue<ArrowInputMessage>();
    }

    void Update()
    {
        // run transition is message is in queue
        if (messageQueue.Count > 0 && !coroutineUtiilities.isRunningCoroutine())
        {
            runTransition(messageQueue.Dequeue());
        }
    }

    public void receive(IMessage message)
    {
        // cast message to arrowinputmessage type
        // TODO: don't add to queue if coroutine is running
        if (message.GetType() == typeof(ArrowInputMessage))
        {
            messageQueue.Enqueue( (ArrowInputMessage) message);
        }
        
    }

    // move the camera (this gameobject)
    void runTransition(ArrowInputMessage message)
    {
        int direction = message.getInput();
        Vector3 finalPos = _transform.position;

        // calculate final position of camera based on input direction
        if (direction == TransitionMessage.RIGHT_ARROW) finalPos += rightRoomVector;
        if (direction == TransitionMessage.LEFT_ARROW) finalPos += leftRoomVector;
        if (direction == TransitionMessage.UP_ARROW) finalPos += upRoomVector;
        if (direction == TransitionMessage.DOWN_ARROW) finalPos += downRoomVector;

        // start moving the camera with coroutine
        StartCoroutine(coroutineUtiilities.moveObjectOverTime(_transform, 
                                                                _transform.position, 
                                                                finalPos,
                                                                transitionDuration));
    }
}
