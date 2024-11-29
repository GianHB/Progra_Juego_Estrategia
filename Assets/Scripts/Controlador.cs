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
        Posicion posicion = BuscarPosicion();

        if (PhotonNetwork.IsConnectedAndReady && Player.LocalInstance == null)
        {

            if (posicion != null)
            {
                PhotonNetwork.Instantiate(prefabJugador.name, posicion.posicion.position, Quaternion.identity);
                OcuparPosicion(posicion);
            }

        }
    }
    public override void OnJoinedRoom()
    {
        Posicion posicion = BuscarPosicion();

        if (Player.LocalInstance == null)
        {

            if (posicion != null)
            {
                PhotonNetwork.Instantiate(prefabJugador.name, posicion.posicion.position, Quaternion.identity);
                OcuparPosicion(posicion);
            }
        }
    }

    private Posicion BuscarPosicion()
    {
        foreach (Posicion posicion in posiciones)
        {
            if (!posicion.ocupada)
            {
                return posicion;
            }
        }

        return null;
    }

    private void OcuparPosicion(Posicion posicion)
    {
        posicion.ocupada = true;
    }
}