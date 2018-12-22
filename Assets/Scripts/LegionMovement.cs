using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionMovement : MonoBehaviour
{
    public float speed;

    // This is what the boss uses to move, it moves from one point to another 
   // [HideInInspector]
    public int currentPatrolIndex;
    public Transform[] patrolPoints;
   // [HideInInspector]
    public Transform currentPatrolPoint;
    public BoolVariable playerExit;
    // To make sure the boss is only active when we're in the boss room
    public BoolVariable active;
    public BoolVariable killed;

    // Start is called before the first frame update
    void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (playerExit.Switch == true)
        {
            Destroy(gameObject);
        }
        if (killed.Switch == false){
            Moving();
        }
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }



    void Moving()
    {
        if (active.Switch == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPatrolPoint.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, currentPatrolPoint.position) < 0.2f)
            {
                if (currentPatrolIndex == patrolPoints.Length - 1){
                    currentPatrolIndex = 0;
                }
                else{
                    currentPatrolIndex += 1;
                }
            }
        }
    }

   
}
