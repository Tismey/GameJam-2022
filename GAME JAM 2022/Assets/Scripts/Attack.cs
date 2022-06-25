using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(other.gameObject);
                PlayerHealth playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
                playerStats.GiveHealth(1000);
            }
        }
    }
}
