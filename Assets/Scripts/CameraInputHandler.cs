using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour
{
    MessengerComponent messenger;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        messenger = gameObject.GetComponent<MessengerComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    void handleInput()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            messenger.send(new ArrowInputMessage(this, ArrowInputMessage.RIGHT_ARROW));

        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            messenger.send(new ArrowInputMessage(this, ArrowInputMessage.LEFT_ARROW));
            
        }
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            messenger.send(new ArrowInputMessage(this, ArrowInputMessage.UP_ARROW));
            
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            messenger.send(new ArrowInputMessage(this, ArrowInputMessage.DOWN_ARROW));
            
        }
    }
}
