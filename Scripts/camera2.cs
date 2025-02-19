using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera2 : MonoBehaviour
{   
    public GameObject player;
    private Vector3 offset;

    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public float minimumY = -60f;
    public float maximumY = 60f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Calcular la distancia entre la cámara y el jugador
        offset = transform.position - player.transform.position;
    }

    // LateUpdate se llama después de que se hayan procesado todos los objetos en la escena
    void LateUpdate()
    {
        // Obtener el movimiento del ratón
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivityY;

        // Limitar la rotación vertical
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        // Rotar la cámara
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);

        // Actualizar la posición de la cámara manteniendo el offset
        transform.position = player.transform.position + offset;
    }
}
