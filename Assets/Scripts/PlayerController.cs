using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;  // Rigidbody para movimiento físico
    public Transform snowboard;  // Referencia a la tabla

    public float acceleration = 8f;  // Aceleración en pendiente
    public float maxSpeed = 15f;  // Velocidad máxima
    public float turnSpeed = 2f;  // Sensibilidad de giro
    public float dragFactor = 0.98f;  // Factor de frenado (simula resistencia en nieve)

    private Vector3 moveDirection;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Bloquear rotaciones innecesarias
    }

    void Update()
    {
        HandleMovement();
        AdjustBoardRotation();
    }

    void HandleMovement()
    {
        float turn = Input.GetAxis("Horizontal") * turnSpeed;
        float speed = Input.GetAxis("Vertical") * acceleration;

        // Aplicar fuerza solo en la dirección hacia adelante
        moveDirection = transform.forward * speed;
        rb.AddForce(moveDirection, ForceMode.Acceleration);

        // Limitar la velocidad máxima
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;

        // Girar el jugador
        transform.Rotate(Vector3.up * turn * Time.deltaTime * 100f);

        // Simular fricción en la nieve
        rb.velocity *= dragFactor;

        Debug.Log(speed);
    }

    void AdjustBoardRotation()
    {
        // Obtener solo la rotación en el eje Y del jugador
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        snowboard.rotation = targetRotation; // Mantener la tabla horizontal
    }
}
