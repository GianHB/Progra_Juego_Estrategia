using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviourPun
{
    public int ownerId;
    private Rigidbody rb;
    [SerializeField] private float speed;
    private Vector3 direction;
    [SerializeField] private Material BalaJugador;
    [SerializeField] private Material BalaOponente;
    private MeshRenderer meshRenderer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetUp(Vector3 direction, int ownerId)
    {
        this.direction = direction;
        this.ownerId = ownerId;

        photonView.RPC("SetMaterial", RpcTarget.All, photonView.IsMine);
    }

    void Update()
    {
        if (!photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            return;
        }
        rb.velocity = direction.normalized * speed;
    }

    public void DestruirBala()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;

        if (other.TryGetComponent(out Player player))
        {
            if (ownerId != player.ObtenerID())
            {
                player.RecibirDañoBala(1);
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
    [PunRPC]
    public void SetMaterial(bool Jugador)
    {
        if (Jugador)
        {
            meshRenderer.material = BalaJugador;
        }
        else
        {
            meshRenderer.material = BalaOponente;
        }
    }
}
