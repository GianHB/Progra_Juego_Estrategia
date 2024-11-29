using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    private static GameObject localInstance;

    [SerializeField] private TextMeshPro playerNameText;

    public static GameObject LocalInstance { get { return localInstance; } }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            playerNameText.text = Gamedata.nombreJugador;
            photonView.RPC("SetName", RpcTarget.AllBuffered, Gamedata.nombreJugador);
            localInstance = gameObject;
        }

        DontDestroyOnLoad(gameObject);
    }


    [PunRPC]
    private void SetName(string playerName)
    {
        playerNameText.text = playerName;
    }

}
