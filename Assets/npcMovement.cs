using UnityEngine;
using UnityEngine.AI;

public class npcMovement : MonoBehaviour
{
    public float moveRadius = 20f;  // Área donde se pueden mover
    public float waitTime = 3f;  // Tiempo de espera antes de moverse a otro punto

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
        SetNewDestination();  // Definir un primer destino
    }

    void Update()
    {
        // Si el NPC llega al destino, espera un poco y busca otro
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SetNewDestination();
                timer = waitTime;
            }
        }
    }

    void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;
        NavMeshHit hit;

        // Encontrar una posición válida en el NavMesh
        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
