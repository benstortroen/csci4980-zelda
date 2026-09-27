using System;
using System.Collections;
using System.Collections.Generic; // for typed Queues
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

// move camera to new room
public class RoomTransitionManager : MonoBehaviour, IReceiverComponent
{
    CoroutineUtiilities coroutineUtiilities;
    MessengerComponent messenger;
    Queue<DirectionalMessage> messageQueue;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform playerTransform;

    static int roomWidth = 16;
    static int roomHeight = 11;
    private Dictionary<int, Vector3> cameraVector = new Dictionary<int, Vector3>();
    private Dictionary<int, Vector3> playerVector = new Dictionary<int, Vector3>();
    [SerializeField] float transitionDuration = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Fill dictionaries with transition messages
        cameraVector.Add(DirectionalMessage.RIGHT_ARROW, roomWidth * Vector3.right);
        cameraVector.Add(DirectionalMessage.LEFT_ARROW, roomWidth * Vector3.left);
        cameraVector.Add(DirectionalMessage.UP_ARROW, roomHeight * Vector3.up);
        cameraVector.Add(DirectionalMessage.DOWN_ARROW, roomHeight * Vector3.down);
        playerVector.Add(DirectionalMessage.RIGHT_ARROW, 4 * Vector3.right);
        playerVector.Add(DirectionalMessage.LEFT_ARROW, 4 * Vector3.left);
        playerVector.Add(DirectionalMessage.UP_ARROW, 4 * Vector3.up);
        playerVector.Add(DirectionalMessage.DOWN_ARROW, 4 * Vector3.down);
        
        // Setup messenger components and utilities
        coroutineUtiilities = new CoroutineUtiilities();
        messenger = gameObject.GetComponent<MessengerComponent>();
        messenger.subscribe(this);
        messageQueue = new Queue<DirectionalMessage>();
    }

    void Update()
    {
        // run transition if message is in queue
        if (messageQueue.Count > 0 && !coroutineUtiilities.isRunningCoroutine())
        {
            DirectionalMessage msg = messageQueue.Dequeue();
            runTransition(msg, cameraTransform, cameraVector);
            runTransition(msg, playerTransform, playerVector);
        }
    }

    public void receive(IMessage message)
    {
        // TODO: don't add to queue if coroutine is running
        if (message.GetType() == typeof(DirectionalMessage) && !coroutineUtiilities.isRunningCoroutine())
        {
            // Queue messages
            messageQueue.Enqueue( (DirectionalMessage) message);
        }
        
    }

    // move the camera (this gameobject)
    void runTransition(DirectionalMessage message, Transform _transform, Dictionary<int, Vector3> dict)
    {
        int direction = message.getInput();
        Vector3 finalPos = _transform.position;

        // calculate final position of camera based on input direction
        finalPos += dict[direction];

        // start moving the camera with coroutine
        StartCoroutine(coroutineUtiilities.moveObjectOverTime(_transform, 
                                                                _transform.position, 
                                                                finalPos,
                                                                transitionDuration));
    }
}
