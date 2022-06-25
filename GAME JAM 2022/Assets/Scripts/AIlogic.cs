using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIlogic : MonoBehaviour
{
    public NavMeshAgent ag;
    public float speed;
    public float AttackDistance;
    public float reactionTime;
    public float CreactionTime = 3;
    Vector3 Point;
    public bool isranged;
    GameObject projectile;
    bool ispatrolling = true;
    public bool stop = true;
    bool isAlert = false;
    Vector3[] posOfIntresse;
    public aiview view;
    GameObject pl;
    public int health = 100;

    public float CAttackCooldown;
    float AttackCoolDown;
    // Start is called before the first frame update
    void Start()
    {
        Point = transform.position;
        pl = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }  
        if (AttackCoolDown > -10)
        {
            AttackCoolDown -= Time.deltaTime;
        }


        if (ispatrolling)
        {
            speed = 4;
            Patrol(posOfIntresse);
            if(Random.Range(0,50) == 0 && stop == false)
            {
                Stop();
            }
        }

        if (isAlert)
        {
            speed = 8;
            Alert();
        }
        if (stop)
        {
            ag.SetDestination(transform.position);
        }
        else
        {
            ag.SetDestination(Point);
        }


       
    }

    void Patrol(Vector3[] fallBackPoints)
    {
        if(Vector3.Distance(Point, transform.position) < 10f)
        {
            Point = new Vector3(transform.position.x + Random.Range(-100,100), transform.position.y, transform.position.z + Random.Range(-100, 100));
            if (SetDestination(Point))
            {
                return;
            }
            else
            {
                Point = fallBackPoints[Random.Range(0, fallBackPoints.Length)];
            }
        }
    }

    void Curious()
    {

    }

    void Alert()
    {
        Point = pl.transform.position;
        if (Vector3.Distance(pl.transform.position, transform.position) < AttackDistance && AttackCoolDown < 0)
        {
            Stop();
            Attack(pl.transform.position);
            AttackCoolDown = CAttackCooldown;
        }
    }

    void Attack(Vector3 target)
    {
        if (isranged)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
        else
    }
    private bool SetDestination(Vector3 targetDestination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
        {

            return true;
        }
        return false;
    }


    void Think()
    {
        if(view.insight == true)
        {
            reactionTime -= Time.deltaTime;
        }
        else
        {
            reactionTime = CreactionTime;
        }

        if(reactionTime < 0)
        {
            isAlert = true;
            ispatrolling = false;
        }
    }

    void Stop()
    {
        stop = true;
        StartCoroutine("cooldown", Random.Range(2, 10));
    }
    IEnumerator cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        stop = false;
    }
}
