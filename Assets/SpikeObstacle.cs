using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeObstacle : MonoBehaviour
{
    public float speed;

    // This is what the Spike uses to move, it moves from one point to another 
    [HideInInspector]
    public int currentPatrolIndex;
    public Transform[] patrolPoints;
    [HideInInspector]
    public Transform currentPatrolPoint;

    // To make sure the spike is only active when we're in the room that uses it, although probably optional.
    public bool active = false;

    public float delay;

    Rigidbody2D rb;

    float timer;

    public bool enteredRoom;

    public Collider2D mainCollider;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        timer = 0;
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= delay && enteredRoom == true)
        {
            active = true;
            anim.SetBool("Activated", true);
            mainCollider.enabled = true;
        }
        else if (timer < delay && enteredRoom == true)
        {
            timer += 1;
        }
        if (enteredRoom == false)
        {
            mainCollider.enabled = false;
           anim.SetBool("Activated", false);
            timer = 0;
            currentPatrolIndex = 0;
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
            transform.position = currentPatrolPoint.position;
        }
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    private void FixedUpdate()
    {
        if (mainCollider.enabled == true)
        {
            Moving();
        }
    }

    void Moving()
    {
        if (active == true)
        {
             rb.position = Vector2.MoveTowards(transform.position, currentPatrolPoint.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, currentPatrolPoint.position) < 0.2f)
            {
                if (currentPatrolIndex == patrolPoints.Length - 1)
                {
                    currentPatrolIndex = 0;
                }
                else
                {
                    currentPatrolIndex += 1;
                }
            }
        }
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.tag == "Player")
        {
            mainCollider.enabled = false;
            yield return new WaitForSeconds(1);
            mainCollider.enabled = true;
        }
    }
}
