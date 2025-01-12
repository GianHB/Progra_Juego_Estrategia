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

        if (photonView.IsMine)
        {
            meshRenderer.material = BalaJugador;
        }
        else
        {
            meshRenderer.material = BalaOponente;
        }
    }
    public void SetUp(Vector3 direction, int ownerId)
    {
        this.direction = direction;
        this.ownerId = ownerId;
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
                player.RecibirDaņoBala(1);
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}
