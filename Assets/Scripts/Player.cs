using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    private static GameObject localInstance;
    private Rigidbody rb;

    [SerializeField] private TextMeshPro playerNameText;
    [SerializeField] private float velocidad;
    [SerializeField] private Material Celeste;
    [SerializeField] private Material Rojo;

    private MeshRenderer meshRenderer;

    public static GameObject LocalInstance { get { return localInstance; } }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (photonView.IsMine)
        {
            playerNameText.text = Gamedata.nombreJugador;
            photonView.RPC("SetName", RpcTarget.AllBuffered, Gamedata.nombreJugador);
            localInstance = gameObject;
            meshRenderer.material = Celeste;
        }
        else
        {
            meshRenderer.material = Rojo;
        }

        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody>();
    }


    [PunRPC]
    private void SetName(string playerName)
    {
        playerNameText.text = playerName;
    }

    private void Update()
    {
        if(!photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(horizontal * velocidad, rb.velocity.y, vertical * velocidad);
    }
}
