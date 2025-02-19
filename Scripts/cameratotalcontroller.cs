using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameratotalcontroller : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public float rotationSpeed = 10f;  // Velocidad de rotación
    public float mouseSensitivity = 2f; // Sensibilidad del ratón

    private enum CameraStyle { Static, FirstPerson, ThirdPerson }
    private CameraStyle currentStyle = CameraStyle.ThirdPerson; // Cámara inicial en tercera persona

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;
    private float verticalRotationLimit = 80f; // Limite para la rotación vertical

    // Start is called before the first frame update
    void Start()
    {
        // Calcular la distancia entre la cámara y el jugador
        offset = transform.position - player.transform.position;

        // Asegurarse de que el cursor esté bloqueado y oculto
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Cambiar entre los estilos de cámara con la entrada personalizada "SwitchCamera"
        if (Input.GetButtonDown("SwitchCamera"))  // Usa la entrada personalizada configurada en el Input Manager
        {
            SwitchCameraStyle();
        }

        // Control de rotación del ratón en primera persona
        if (currentStyle == CameraStyle.FirstPerson)
        {
            horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

            transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);
        }
    }

    // LateUpdate se llama después de que se hayan procesado todos los objetos en la escena
    void LateUpdate()
    {
        switch (currentStyle)
        {
            case CameraStyle.Static:
                // Cámara estática
                transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(player.transform);
                break;

            case CameraStyle.FirstPerson:
                // Cámara en primera persona controlada por el ratón
                transform.position = player.transform.position + offset;
                break;

            case CameraStyle.ThirdPerson:
                // Cámara en tercera persona con rotación alrededor del jugador
                transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
                transform.LookAt(player.transform);
                break;
        }
    }

    // Cambiar entre los estilos de cámara
    void SwitchCameraStyle()
    {
        currentStyle = (CameraStyle)(((int)currentStyle + 1) % System.Enum.GetValues(typeof(CameraStyle)).Length);
    }
}
