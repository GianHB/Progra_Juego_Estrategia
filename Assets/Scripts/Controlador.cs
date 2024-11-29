using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject prefabJugador;

    [SerializeField] private List<Posicion> posiciones;

    [System.Serializable]
    public class Posicion
    {
        public Transform posicion;
        public bool ocupada;
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && Player.LocalInstance == null)
        {
            Posicion posicion = BuscarPosition();

            if (posicion != null)
            {
                PhotonNetwork.Instantiate(prefabJugador.name, posicion.posicion.position, Quaternion.identity);
            }

        }
    }
    public override void OnJoinedRoom()
    {
        if (Player.LocalInstance == null)
        {
            Posicion posicion = BuscarPosition();

            if (posicion != null)
            {
                PhotonNetwork.Instantiate(prefabJugador.name, posicion.posicion.position, Quaternion.identity);
            }
        }
    }

    private Posicion BuscarPosition()
    {
        foreach (Posicion posicion in posiciones)
        {
            if (posicion.ocupada) continue;

            posicion.ocupada = true;
            return posicion;
        }

        return null;
    }
}