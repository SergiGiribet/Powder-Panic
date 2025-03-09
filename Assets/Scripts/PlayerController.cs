using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public Transform snowboard;  // Referencia a la tabla
    public float moveSpeed = 5f;
    public float rotateSpeed = 3f;

    void Update()
    {
        // Mover al jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);
        player.Rotate(Vector3.up * horizontal * rotateSpeed * Time.deltaTime);

        // Simular que la tabla sigue al jugador (puedes hacerla rotar independientemente)
        snowboard.rotation = player.rotation;  // Sincronizar rotación
    }
}
