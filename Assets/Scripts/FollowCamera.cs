using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;      // El jugador a seguir
    public Vector3 offset;        // Desplazamiento con respecto al jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public float rotationSpeed = 5f; // Velocidad de rotación de la cámara

    private void LateUpdate()
    {
        // Movimiento suave
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Rotación de la cámara con el jugador (opcional)
        float horizontalInput = Input.GetAxis("Horizontal");  // Captura la entrada del jugador
        transform.RotateAround(player.position, Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        // Siempre mirar hacia el jugador
        transform.LookAt(player);
    }
}
