using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject prefabJugador;

    [SerializeField] private List<Transform> posiciones;


    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && Player.LocalInstance == null)
        {
            Transform posicion = BuscarPosition();

            if (posicion != null)
            {
                PhotonNetwork.Instantiate(prefabJugador.name, posicion.position, Quaternion.identity);
            }

        }
    }
    public override void OnJoinedRoom()
    {
        if (Player.LocalInstance == null)
        {
            Transform posicion = BuscarPosition();

            if (posicion != null)
            {
                PhotonNetwork.Instantiate(prefabJugador.name, posicion.position, Quaternion.identity);
            }
        }
    }

    private Transform BuscarPosition()
    {
        foreach (Transform posicion in posiciones)
        {
            bool ocupada = Physics.CheckSphere(posicion.position, 0.25f);

            if (!ocupada)
            {
                return posicion;
            }
        }

        return null;
    }
}