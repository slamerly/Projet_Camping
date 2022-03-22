/* Authors: Steven Lamerly
 * Description:
 */
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float rangeLook = 5f;
    
    private GameObject target;
    private NavMeshAgent agent;
    private Enemy enemy;
    private GameObject[] bushes;
    private GameObject bushDespawn;
    private float distanceBush; //distance between enemy and bush
    private float mostShortDistanceBush; //most short distance between enemy and bush

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        bushes = GameObject.FindGameObjectsWithTag("Bush");
        bushDespawn = bushes[0];
        mostShortDistanceBush = Vector3.Distance(bushes[0].transform.position, transform.position);
    }

    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance <= rangeLook)
        {
            agent.SetDestination(target.transform.position);
            if(distance <= agent.stoppingDistance)
            {
                //Attack
            }
            FaceTarget(target.transform);
        }

        //Retreat
        if (enemy.retreat == true)
        {
            FindDestinationToDespawn();
            if (mostShortDistanceBush <= agent.stoppingDistance)
            {
                //Despawn
                Destroy(gameObject);
            }
        }

    }

    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    void FindDestinationToDespawn()
    {
        foreach (GameObject bush in bushes)
        {
            distanceBush = Vector3.Distance(bush.transform.position, transform.position);

            if (distanceBush < mostShortDistanceBush)
            {
                mostShortDistanceBush = distanceBush;
                bushDespawn = bush;
            }
        }
        target = bushDespawn;
    }
}
