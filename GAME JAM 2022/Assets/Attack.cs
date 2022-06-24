using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float nbHits = 0;
    public float n = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            ++n;
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(other.gameObject);
                ++nbHits;
            }
        }
    }
}
