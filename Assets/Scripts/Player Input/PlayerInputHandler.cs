using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap playerActionMap;
    public int playerID => playerInput.playerIndex;

    #region ABXY Input Button
    // A Button
    private bool m_isPressedA;
    public bool isPressedA => m_isPressedA;
    private bool m_isStartA;
    private bool m_isReleaseA;

    // B Button
    private bool m_isPressedB;
    public bool isPressedB => m_isPressedB;
    private bool m_isStartB;
    private bool m_isReleaseB;

    // X Button
    private bool m_isPressedX;
    public bool isPressedX => m_isPressedX;
    private bool m_isStartX;
    private bool m_isReleaseX;

    // Y Button
    private bool m_isPressedY;
    public bool isPressedY => m_isPressedY;
    private bool m_isStartY;
    private bool m_isReleaseY;
    #endregion

    private void Awake()
    {
        // Setup for input system
        playerInput = GetComponent<PlayerInput>();
        playerActionMap = playerInput.actions.FindActionMap("Player");
    }

    private void OnEnable()
    {
        // Listeners for button press events on the player action map for buttons A, B, X, and Y
        playerActionMap.FindAction("A").performed += i => m_isPressedA = true;
        playerActionMap.FindAction("B").performed += i => m_isPressedB = true;
        playerActionMap.FindAction("X").performed += i => m_isPressedX = true;
        playerActionMap.FindAction("Y").performed += i => m_isPressedY = true;

        playerActionMap.FindAction("A").started += i => m_isStartA = true;
        playerActionMap.FindAction("B").started += i => m_isStartB = true;
        playerActionMap.FindAction("X").started += i => m_isStartX = true;
        playerActionMap.FindAction("Y").started += i => m_isStartY = true;

        playerActionMap.FindAction("A").canceled += i => m_isReleaseA = true;
        playerActionMap.FindAction("B").canceled += i => m_isReleaseB = true;
        playerActionMap.FindAction("X").canceled += i => m_isReleaseX = true;
        playerActionMap.FindAction("Y").canceled += i => m_isReleaseY = true;

        playerActionMap.Enable();
    }

    private void OnDisable()
    {
        playerActionMap.Disable();
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        m_isPressedA = false;
        m_isPressedB = false;
        m_isPressedX = false;
        m_isPressedY = false;

        m_isStartA = false;
        m_isStartB = false;
        m_isStartX = false;
        m_isStartY = false;

        m_isReleaseA = false;
        m_isReleaseB = false;
        m_isReleaseX = false;
        m_isReleaseY = false;
    }
}
