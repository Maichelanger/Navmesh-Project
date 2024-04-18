using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float updateRate = 0.5f;

    private Transform player;
    private NavMeshAgent navAgent;
    private float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(player.position);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateRate)
        {
            navAgent.SetDestination(player.position);
            timer = 0;
        }
    }
}
