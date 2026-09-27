using System;
using System.Collections;
using System.Collections.Generic; // for typed Queues
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

// move camera to new room
public class RoomTransition : MonoBehaviour, IReceiverComponent
{
    CoroutineUtiilities coroutineUtiilities;
    MessengerComponent messenger;
    Queue<ArrowInputMessage> messageQueue;
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
        cameraVector.Add(TransitionMessage.RIGHT_ARROW, roomWidth * Vector3.right);
        cameraVector.Add(TransitionMessage.LEFT_ARROW, roomWidth * Vector3.left);
        cameraVector.Add(TransitionMessage.UP_ARROW, roomHeight * Vector3.up);
        cameraVector.Add(TransitionMessage.DOWN_ARROW, roomHeight * Vector3.down);
        playerVector.Add(TransitionMessage.RIGHT_ARROW, 4 * Vector3.right);
        playerVector.Add(TransitionMessage.LEFT_ARROW, 4 * Vector3.left);
        playerVector.Add(TransitionMessage.UP_ARROW, 4 * Vector3.up);
        playerVector.Add(TransitionMessage.DOWN_ARROW, 4 * Vector3.down);
        
        // Setup messenger components and utilities
        coroutineUtiilities = new CoroutineUtiilities();
        messenger = gameObject.GetComponent<MessengerComponent>();
        messenger.subscribe(this);
        messageQueue = new Queue<ArrowInputMessage>();
    }

    void Update()
    {
        // run transition if message is in queue
        if (messageQueue.Count > 0 && !coroutineUtiilities.isRunningCoroutine())
        {
            ArrowInputMessage msg = messageQueue.Dequeue();
            runTransition(msg, cameraTransform, cameraVector);
            runTransition(msg, playerTransform, playerVector);
        }
    }

    public void receive(IMessage message)
    {
        // TODO: don't add to queue if coroutine is running
        if (message.GetType() == typeof(ArrowInputMessage) && !coroutineUtiilities.isRunningCoroutine())
        {
            // Queue messages
            messageQueue.Enqueue( (ArrowInputMessage) message);
        }
        
    }

    // move the camera (this gameobject)
    void runTransition(ArrowInputMessage message, Transform _transform, Dictionary<int, Vector3> dict)
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
