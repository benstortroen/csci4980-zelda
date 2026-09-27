using UnityEditorInternal;
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

    void handleInput()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            messenger.send(new ArrowInputMessage(this, TransitionMessage.RIGHT_ARROW));

        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            messenger.send(new ArrowInputMessage(this, TransitionMessage.LEFT_ARROW));

        }
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            messenger.send(new ArrowInputMessage(this, TransitionMessage.UP_ARROW));

        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            messenger.send(new ArrowInputMessage(this, TransitionMessage.DOWN_ARROW));

        }
    }

    // constantly check for arrow keystrokes
    void Update()
    {
        handleInput();
    }
}
