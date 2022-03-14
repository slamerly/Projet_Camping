/* Authors: Steven Lamerly
 * Description:
 */
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }
}
