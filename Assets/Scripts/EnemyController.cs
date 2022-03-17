/* Authors: Steven Lamerly
 * Description:
 */

using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float rangeLook = 5f;
    
    private Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= rangeLook)
        {
            agent.SetDestination(target.position);
            if(distance <= agent.stoppingDistance)
            {
                //attack
            }
            FaceTarget(target);
        }
    }

    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeLook);
    }
}
