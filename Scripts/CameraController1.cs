using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Calcular la distancia entre la cámara y el jugador
        offset = transform.position - player.transform.position;
    }

    // LateUpdate se llama después de que se hayan procesado todos los objetos en la escena
    void LateUpdate()
    {   // Actualizar la posición de la cámara
        transform.position = player.transform.position + offset; 
    }
}
