using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            --health;
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
