/* Authors: Steven Lamerly
 * Description: 
 */
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public int damage = 5;

    private 
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Attack()
    {
        /*
        if(player.life > 0)
        {
            player.life -= damage;
        }
        else
        {
            player.Die();
        }
        */
    }

    /*bool Retreat()
    {

        if (player.Attack())
        {
            return true;
        }
        return false;
    }
    */
}
