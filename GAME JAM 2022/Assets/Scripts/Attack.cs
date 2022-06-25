using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public PlayerHealth playerStats;
    public AIlogic ai;
    private void OnTriggerStay(Collider other)
    { 
        if (other.GetComponent<AIlogic>() != null && gameObject.GetComponentInParent<movements>() != null)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                other.GetComponent<AIlogic>().health -= 100;
                
                playerStats.GiveHealth(1000);
            }
        }
        else if(other.GetComponent<movements>() != null  && ai.AttackCoolDown <0){
                    playerStats.TakeDamage(1000);
            ai.AttackCoolDown = ai.CAttackCooldown;
        }
    }
}
