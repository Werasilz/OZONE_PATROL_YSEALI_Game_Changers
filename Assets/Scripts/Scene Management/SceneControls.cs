using UnityEngine;
using UnityEngine.InputSystem;

public class SceneControls : MonoBehaviour
{
    private readonly InputAction m_anyKey = new(binding: "/*/<button>", type: InputActionType.Button);

    private void Awake()
    {
        m_anyKey.performed += PressAnyKeyToStart;
    }

    private void OnEnable()
    {
        m_anyKey.Enable();
    }

    private void OnDisable()
    {
        m_anyKey.Disable();
    }

    private void OnDestroy()
    {
        m_anyKey.performed -= PressAnyKeyToStart;
    }


    private void PressAnyKeyToStart(InputAction.CallbackContext ctx)
    {

    }
}