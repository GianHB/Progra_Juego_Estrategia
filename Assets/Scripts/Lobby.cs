using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    [Header("UI Components")]
    [SerializeField] private Button botonComenzar;
    [SerializeField] private TMP_InputField inputField;

    [Header("Settings")]
    [SerializeField] private string escenaJuego;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        botonComenzar.onClick.AddListener(GuardarDatos);
    }

    private void GuardarDatos()
    {
        Gamedata.nombreJugador = inputField.text;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        options.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom("Room", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        PhotonNetwork.LoadLevel(escenaJuego);
    }
}