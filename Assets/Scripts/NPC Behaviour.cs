using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField] private bool useWaypoints = true;
    [SerializeField] private bool randomWaypointsPatrol = false;
    [SerializeField] private bool randomPatrolling = false;
    [SerializeField] private Transform waypointsParent;
    [SerializeField] private Vector3 mapMax;
    [SerializeField] private Vector3 mapMin;

    private Vector3 destination;
    private int currentWaypointIndex = 0;
    private NavMeshAgent navAgent;
    private Transform[] waypoints;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (useWaypoints)
        {
            storeWaypoints();

            destination = waypoints[currentWaypointIndex].position;
            navAgent.SetDestination(destination);
        }

        if (randomPatrolling)
        {
            destination = new Vector3(Random.Range(mapMin.x, mapMax.x), 0, Random.Range(mapMin.z, mapMax.z));
            navAgent.SetDestination(destination);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Update()
    {
        if (randomPatrolling)
            RandomPatrolling();
        else if (useWaypoints)
            if (randomWaypointsPatrol)
                RandomWaypointsPatrol();
            else
                Patrol();
        else
            GetMouseDestination();
    }

    private void storeWaypoints()
    {
        if (waypointsParent == null || waypointsParent.childCount == 0)
        {
            Debug.LogError("Waypoints not assigned. Random Patrolling.");
            useWaypoints = false;
            randomPatrolling = true;
            return;
        }

        waypoints = new Transform[waypointsParent.childCount];
        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
            destination = waypoints[currentWaypointIndex].position;
            navAgent.SetDestination(destination);
        }
    }

    private void RandomWaypointsPatrol()
    {
        if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            currentWaypointIndex = Random.Range(0, waypoints.Length);
            destination = waypoints[currentWaypointIndex].position;
            navAgent.SetDestination(destination);
        }
    }

    private void RandomPatrolling()
    {
        if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            destination = new Vector3(Random.Range(mapMin.x, mapMax.x), 1, Random.Range(mapMin.z, mapMax.z));
            navAgent.SetDestination(destination);
        }
    }

    private void GetMouseDestination()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                navAgent.SetDestination(hit.point);
            }
        }
    }
}
