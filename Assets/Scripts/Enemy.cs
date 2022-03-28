/* Authors: Steven Lamerly
 * Description: 
 */
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public bool retreat = false;
    public bool burning = false;
    public float delayBeforeAttack = 3f;

    private void Update()
    {
        if (burning)
        {
            StartCoroutine(Burn());
        }
    }

    public IEnumerator Attack()
    {
        GetComponent<EnemyController>().agent.isStopped = true;
        yield return new WaitForSeconds(delayBeforeAttack);
        if (player != null && GetComponent<EnemyController>().distance <= GetComponent<EnemyController>().agent.stoppingDistance && !retreat)
        {
            player.GetComponent<Player>().Die();
        }
        else
            GetComponent<EnemyController>().agent.isStopped = false;
    }

    public IEnumerator Burn()
    {
        yield return new WaitForSeconds(delayBeforeAttack);
        //Burning anim
        player.GetComponent<Player>().mostShortDistanceBramble = player.GetComponent<Player>().mostShortDistanceWolf + 1; // permet de d'avoir une valeur supérieur au cas ou il n'y aurait plus de buisson
        burning = false;
        Destroy(gameObject);
    }
}