/* Authors: Steven Lamerly
 * Description:
 */
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rangeAttack = 5f;
    public float attackRate = 5f;
    public float mostShortDistanceWolf; //most short distance between player and wolf
    public float mostShortDistanceBramble;
    public int nbBrambles = 0;

    private GameObject[] wolfes;
    private GameObject wolfSelected;
    private GameObject[] brambles;
    private GameObject brambleSelected;
    private float distanceWolf; //distance between player and wolf
    private float distanceBramble; //distance between player and wolf
    private float delayAttack = 0f;
    private int nbWolfes = 0;

    void Start()
    {
        wolfes = GameObject.FindGameObjectsWithTag("Wolf");
        brambles = GameObject.FindGameObjectsWithTag("Bramble");
        nbWolfes = wolfes.Length;
        nbBrambles = brambles.Length;
        if(nbWolfes > 0)
        {
            wolfSelected = wolfes[0];
            mostShortDistanceWolf = Vector3.Distance(wolfes[0].transform.position, transform.position);
        }
        if(nbBrambles > 0)
        {
            brambleSelected = brambles[0];
            mostShortDistanceBramble = Vector3.Distance(brambles[0].transform.position, transform.position);
        }
    }

    void Update()
    {
        SelectWolf();
        SelectBramble();

        // Wolf enter in attack area of the player
        if (mostShortDistanceWolf <= rangeAttack && nbWolfes > 0)
        {
            if (Input.GetButton("Fire1") && Time.time >= delayAttack)
            {
            Debug.Log("ATTACK");
                delayAttack = Time.time / attackRate;
                Attack();
            }
        }
        else if (mostShortDistanceBramble <= rangeAttack && nbBrambles > 0)
        {
            if (Input.GetButton("Fire1") && Time.time >= delayAttack)
            {
                delayAttack = Time.time / attackRate;
                PutFire();
            }
        }
        /*if (Input.GetButtonDown("Fire1") && Time.time >= delayAttack)
        {
            if (mostShortDistanceWolf <= rangeAttack && nbWolfes > 0)
            {
                Debug.Log("ATTACK");
                delayAttack = Time.time / attackRate;
                Attack();
            }
            else if (mostShortDistanceBramble <= rangeAttack && nbBrambles > 0)
            {
                delayAttack = Time.time / attackRate;
                PutFire();
            }
        }*/
    }

    // Player attack
    void Attack()
    {
        // Attack animation
        wolfSelected.GetComponent<Enemy>().retreat = true;
        nbWolfes--;
    }

    void PutFire()
    {
        // Put fire animation
        Debug.Log("Allumer le feu !");
        brambleSelected.GetComponent<Enemy>().burning = true;
        nbBrambles--;
        //mostShortDistanceBramble = mostShortDistanceWolf + 1; // permet de d'avoir une valeur supérieur au cas ou il n'y aurait plus de buisson
    }

    // Select the wolf the most of the player
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

    void SelectBramble()
    {
        if (brambles != null && nbBrambles > 0)
        {
            foreach (GameObject bramble in brambles)
            {
                distanceBramble = Vector3.Distance(bramble.transform.position, transform.position);
                if (distanceBramble < mostShortDistanceBramble)
                {
                    mostShortDistanceBramble = distanceBramble;
                    brambleSelected = bramble;
                }
            }
        }
    }

    // Player die
    public void Die()
    {
        Destroy(gameObject);
    }
}
