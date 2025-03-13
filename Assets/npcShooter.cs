using UnityEngine;
using UnityEngine.AI;

public class npcShooter : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public GameObject[] projectiles;  // Diferentes proyectiles que puede lanzar
    public Transform firePoint;  // Punto desde donde dispara
    public float shootingRange = 10f;  // Distancia a la que dispara
    public float shootInterval = 2f;  // Tiempo entre disparos
    public float projectileSpeed = 15f;  // Velocidad de los proyectiles
    public float chaseSpeed = 4f;  // Velocidad de persecución

    private NavMeshAgent agent;
    private float shootTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        shootTimer = shootInterval;
        agent.speed = chaseSpeed;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > shootingRange)
        {
            // Perseguir al jugador
            agent.SetDestination(player.position);
        }
        else
        {
            // Disparar si está en rango
            agent.ResetPath();  // Detiene el movimiento
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            if (projectiles.Length > 0)
            {
                // Elegir un proyectil aleatorio
                GameObject projectilePrefab = projectiles[Random.Range(0, projectiles.Length)];
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

                // Apuntar hacia el jugador
                Vector3 direction = (player.position - firePoint.position).normalized;
                projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

                shootTimer = shootInterval;  // Reiniciar el tiempo de disparo
            }
        }
    }
}
