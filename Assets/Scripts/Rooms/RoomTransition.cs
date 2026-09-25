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

    [SerializeField] Vector3 rightRoomVector;
    [SerializeField] Vector3 leftRoomVector;
    [SerializeField] Vector3 upRoomVector;
    [SerializeField] Vector3 downRoomVector;
    [SerializeField] float transitionDuration = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = gameObject.transform;
        coroutineUtiilities = new CoroutineUtiilities();

        messenger = gameObject.GetComponent<MessengerComponent>();
        messenger.subscribe(this);

        messageQueue = new Queue<ArrowInputMessage>();
    }

    void Update()
    {
        if (messageQueue.Count > 0 && !coroutineUtiilities.isRunningCoroutine())
        {
            runTransition(messageQueue.Dequeue());
        }
    }

    public void receive(IMessage message)
    {
        // cast message to arrowinputmessage type
        if (message.GetType() == typeof(ArrowInputMessage))
        {
            messageQueue.Enqueue( (ArrowInputMessage) message);
        }
        
    }

    void runTransition(ArrowInputMessage message)
    {
        int direction = message.getInput();
        Vector3 finalPos = _transform.position;

        // calculate final position of camera based on input direction
        if (direction == ArrowInputMessage.RIGHT_ARROW) finalPos += rightRoomVector;
        if (direction == ArrowInputMessage.LEFT_ARROW) finalPos += leftRoomVector;
        if (direction == ArrowInputMessage.UP_ARROW) finalPos += upRoomVector;
        if (direction == ArrowInputMessage.DOWN_ARROW) finalPos += downRoomVector;

        // start moving the camera with coroutine
        StartCoroutine(coroutineUtiilities.moveObjectOverTime(_transform, 
                                                                _transform.position, 
                                                                finalPos,
                                                                transitionDuration));
    }
}
