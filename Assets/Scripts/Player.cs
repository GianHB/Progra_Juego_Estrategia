using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    private static GameObject localInstance;

    [SerializeField] private TextMeshPro playerNameText;

    private Rigidbody rb;
    [SerializeField] private float Velocidad;

    public static GameObject LocalInstance { get { return localInstance; } }

    [SerializeField] private GameObject BalaPrefab;
    [SerializeField] private Transform PuntodeDisparo;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            playerNameText.text = Gamedata.nombreJugador;
            photonView.RPC("SetName", RpcTarget.AllBuffered, Gamedata.nombreJugador);
            localInstance = gameObject;
        }
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody>();
    }


    [PunRPC]
    private void SetName(string playerName)
    {
        playerNameText.text = playerName;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            return;
        }
        Movimiento();
        Disparo();
    }

    void Movimiento()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector3(horizontal * Velocidad, rb.velocity.y, vertical * Velocidad);
        
    }

    void Disparo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 PosiciondelMouse = hitInfo.point;
                Vector3 direccion = (PosiciondelMouse - transform.position);
                direccion.y = 0;
                direccion.Normalize();
                GameObject obj = PhotonNetwork.Instantiate(BalaPrefab.name, PuntodeDisparo.position, Quaternion.identity);
                obj.GetComponent<Bala>().SetUp(direccion, photonView.ViewID);
            }
        }

    }
}
