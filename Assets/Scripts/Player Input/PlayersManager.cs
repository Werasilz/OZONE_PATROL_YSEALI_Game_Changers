using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersManager : Singleton<PlayersManager>
{
    private PlayerInputManager playerInputManager;

    private PlayerInput[] m_playersInput;
    public PlayerInput[] playerInputs => m_playersInput;

    private PlayerInputHandler[] m_players;
    public PlayerInputHandler[] players => m_players;

    public override void Awake()
    {
        base.Awake();

        m_playersInput = new PlayerInput[4];
        m_players = new PlayerInputHandler[4];

        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        // Add new player
        m_playersInput[playerInput.playerIndex] = playerInput;

        // Get player controller
        m_players[playerInput.playerIndex] = playerInput.GetComponent<PlayerInputHandler>();

        // Joined 2 or 4 players
        if ((playerInputManager.playerCount % playerInputManager.maxPlayerCount / 2) == 0)
        {

        }

        // Change object name
        playerInput.gameObject.name = "[Player] " + playerInput.playerIndex;

        Debug.Log(string.Format("[Player] {0} Joined using {1}", playerInput.playerIndex, playerInput.currentControlScheme));
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        // Clear player 
        int leftPlayerIndex = playerInput.playerIndex;
        m_playersInput[leftPlayerIndex] = null;
        m_players[leftPlayerIndex] = null;

        Debug.Log(string.Format("[Player] {0} Left", playerInput.playerIndex));
    }
}
