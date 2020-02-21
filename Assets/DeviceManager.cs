using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceManager : MonoBehaviour
{
    [SerializeField]
    private static int PlayerCount = 0;

    [SerializeField]
    public static InputDevice[] PlayerDevices { get; } = new InputDevice[4];

    [SerializeField]
    private GameObject playerPrefab;

    public void InstantiatePlayers()
    {
        for(int i = 0; i < 4; i++)
        {
            if(PlayerDevices[i] != null)
            {
                PlayerInputManager pm = GetComponent<PlayerInputManager>();
                pm.playerPrefab = playerPrefab;
                pm.JoinPlayer(pairWithDevice: PlayerDevices[i]);
                Debug.Log("Joining player");
            }
        }
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Joined" + playerInput.playerIndex);
        PlayerDevices[playerInput.playerIndex] = playerInput.devices[0];
        PlayerCount++;

        playerInput.gameObject.transform.Translate(Vector3.up * 10);
    }

    /*void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("Left" + playerInput.playerIndex);
        PlayerDevices[playerInput.playerIndex] = null;
        PlayerCount--;
    }*/
}
