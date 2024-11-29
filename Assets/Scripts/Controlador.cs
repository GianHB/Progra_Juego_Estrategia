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

        OcuparPosicion(posicion);

        if (PhotonNetwork.IsConnectedAndReady && Player.LocalInstance == null)
        {

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
        Posicion posicion = BuscarPosicion();

        OcuparPosicion(posicion);

            if (posicion != null)
            {
                PhotonNetwork.Instantiate(prefabJugador.name, posicion.posicion.position, Quaternion.identity);
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
        if (posicion == null) return;
        posicion.ocupada = true;
    }
}