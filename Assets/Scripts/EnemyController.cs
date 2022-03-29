/* Authors: Steven Lamerly
 * Description:
 */
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float rangeLook = 5f;
    public float distance;
    public float timeBeforeReturnToAttack = 3f;
    public NavMeshAgent agent;

    private Enemy enemy;
    private GameObject target;
    private GameObject bushDespawn;
    private GameObject[] bushes;
    private float distanceBush; //distance between enemy and bush
    private float mostShortDistanceBush; //most short distance between enemy and bush
    private float timerBeforeReturn;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        bushes = GameObject.FindGameObjectsWithTag("Bush");
        bushDespawn = bushes[0];
        mostShortDistanceBush = Vector3.Distance(bushes[0].transform.position, transform.position);
        timerBeforeReturn = 3 + timeBeforeReturnToAttack;
    }

    void Update()
    {
        if(target != null)
        {
            if (target == GameObject.FindGameObjectWithTag("Player"))
            {
                distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance < target.GetComponent<Player>().mostShortDistanceBramble)
                {
                    if (distance <= rangeLook)
                    {
                        agent.SetDestination(target.transform.position);
                        // Attack Player
                        if (distance <= agent.stoppingDistance && !enemy.retreat)
                        {
                            StartCoroutine(enemy.Attack());
                        }
                        FaceTarget(target.transform);
                    }
                }
            }

            //Retreat
            Retreat();
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
        agent.SetDestination(target.transform.position);
    }

    void Retreat()
    {
        if (enemy.retreat == true)
        {
            FindDestinationToDespawn();
            timerBeforeReturn -= Time.deltaTime;
            //Debug.Log(timerBeforeReturn);
            if (timerBeforeReturn > 0)
            {
                if (mostShortDistanceBush <= agent.stoppingDistance)
                {
                    //Despawn
                    Destroy(gameObject);
                    mostShortDistanceBush = 100000;
                }
            }
            else
            {
                timerBeforeReturn = 3 + timeBeforeReturnToAttack;
                enemy.retreat = false;
                target = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
}
