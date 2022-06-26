using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class AIlogic : MonoBehaviour
{
    public NavMeshAgent ag;
    public float speed;
    public float AttackDistance;
    public float reactionTime;
    public float CreactionTime = 3;
    Vector3 Point;
    public bool isranged;
    public GameObject projectile;
    bool ispatrolling = true;
    public bool stop = true;
    bool isAlert = false;
    public Transform[] posOfIntresse;
    public aiview view;
    GameObject pl;
    public int health = 100;

    public float CAttackCooldown;
    public float AttackCoolDown;
    public Animator anim;
    bool attackanim = false;
    // Start is called before the first frame update
    void Start()
    {
        Point = transform.position;
        pl = GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", ag.speed);
        anim.SetBool("Attack", attackanim);
        anim.SetBool("stop", stop);
        if (health < 0)
        {
            Destroy(gameObject);
        }  
        if (AttackCoolDown > -10)
        {
            AttackCoolDown -= Time.deltaTime;
        }
        Think();

        if (ispatrolling)
        {
            speed = 4;
            Patrol(posOfIntresse);
            if(Random.Range(0,1000) == 0 && stop == false)
            {
                Debug.Log("Stop");
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


        ag.speed = speed;
    }

    void Patrol(Transform[] fallBackPoints)
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
                Debug.Log("unvalid point going to default");
                Point = fallBackPoints[Random.Range(0, fallBackPoints.Length)].position;
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
            StartCoroutine("Attackanim");
            Stop();
            
        }     
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
        if(view.insight == true && !isAlert)
        {
            reactionTime -= Time.deltaTime;
            Stop();
            Debug.Log("!!! " + reactionTime);
        }
        else
        {
            reactionTime = CreactionTime;
        }

        if(reactionTime < 0)
        {
            isAlert = true;
            ispatrolling = false;
            Debug.Log("Spotted");
        }
    }

    void Stop()
    {
        stop = true;
        speed = 0;
        StartCoroutine("cooldown", Random.Range(5, 10));
    }
    IEnumerator cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("MOVE");
        stop = false;
    }
    IEnumerator Attackanim()
    {
        attackanim = true;
        yield return new WaitForSeconds(1);
        attackanim = false;
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
