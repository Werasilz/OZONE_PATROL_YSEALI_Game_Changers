using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputManager : MonoBehaviour
{
    [ClearOnReload]
    public static TouchControls touchControls;

    public delegate void StartTouchEvent(Vector2 position, float time);
    public static event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public static event EndTouchEvent OnEndTouch;

    public static Vector2 touchPosition => touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    public static float touchPress => touchControls.Touch.TouchPress.ReadValue<float>();
    public bool debugTouchTime;

    void Awake()
    {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    // Clean up event when reload domain is disabled
    [ExecuteOnReload]
    public static void CleanUpEvent()
    {
        StartTouchEvent.Remove(OnStartTouch, null);
        EndTouchEvent.Remove(OnEndTouch, null);
        OnStartTouch = null;
        OnEndTouch = null;
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += context => StartTouch(context);
        touchControls.Touch.TouchPress.canceled += context => EndTouch(context);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        if (debugTouchTime) print("Touch started at position " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());

        if (OnStartTouch != null)
        {
            // If this event called it will send these value
            OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        if (debugTouchTime) print("Touch ended " + ((float)context.time - (float)context.startTime).ToString("F2") + " s");

        if (OnEndTouch != null)
        {
            // If this event called it will send these value
            OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }
}
