/* Authors: Steven Lamerly
 * Description:
 */
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rangeAttack = 5f;
    public float attackRate = 5f;

    private GameObject[] wolfes;
    private GameObject wolfSelected;
    private float distanceWolf; //distance between player and wolf
    private float mostShortDistanceWolf; //most short distance between player and wolf
    private float delayAttack = 0f;
    private int nbWolfes = 0;

    void Start()
    {
        wolfes = GameObject.FindGameObjectsWithTag("Wolf");
        wolfSelected = wolfes[0];
        nbWolfes = wolfes.Length;
        mostShortDistanceWolf = Vector3.Distance(wolfes[0].transform.position, transform.position);
    }

    void Update()
    {
        SelectWolf();

        // Wolf enter in attack area of the player
        if(mostShortDistanceWolf <= rangeAttack)
        {
            //Debug.Log("Enter in area");
            if(Input.GetButton("Fire1") && Time.time >= delayAttack)
            {
                delayAttack = Time.time / attackRate;
                Attack();
            }
        }
    }

    void Attack()
    {
        // Attack animation
        wolfSelected.GetComponent<Enemy>().retreat = true;
        nbWolfes--;
    }

    //Select the wolf the most of the player
    void SelectWolf()
    {
        if (wolfes != null && nbWolfes > 0)
        {
            foreach (GameObject wolf in wolfes)
            {
                distanceWolf = Vector3.Distance(wolf.transform.position, transform.position);
                if (distanceWolf < mostShortDistanceWolf)
                {
                    mostShortDistanceWolf = distanceWolf;
                    wolfSelected = wolf;
                }
            }
        }
    }
}
