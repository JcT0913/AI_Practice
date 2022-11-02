using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using JetBrains.Annotations;
using System.IO;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform image;
    public float speed = 200f;
    public float nextWaypointDistance = 1f;

    private Pathfinding.Path path;
    private int currentWaypoint = 0;
    private bool reached = false;
    private Seeker seeker;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        // keep updating new paths from 0 second and each 0.5 seconds, to make enemy keep chasing the target
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        //seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reached = true;
            return;
        }
        else
        {
            reached = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb2D.AddForce(force);

        float distance = Vector2.Distance(rb2D.position, path.vectorPath[currentWaypoint]);
        if (distance <= nextWaypointDistance)
        {
            currentWaypoint += 1;
        }

        // change the direction the enemy is facing according to its speed on x axis
        if (force.x >= 0.01f)
        {
            image.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            image.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb2D.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
