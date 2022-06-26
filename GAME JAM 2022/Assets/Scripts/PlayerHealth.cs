using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 6000;
    public int pain = 20000;
    public int bloodGiven;

    // Update is called once per frame
    void Update()
    {
        if (pain > 0)
        {
            --pain;
        }
        else
        {
            hagraah();
        }
        //Debug.Log("Health = " + health.ToString());
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        pain += damage;
    }

    public void GiveHealth(int amount)
    {
        health += amount;
    }

    public bool hagraah()
    {
        return true;
    }
}
