using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class EnemyAI : MonoBehaviour
{
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    [SerializeField] private float speed = 2f;

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        // Make box collider a trigger
        GetComponent<BoxCollider2D>().isTrigger = true;

        // Create root object
        GameObject root = new GameObject(name + "_Root");

        // Reset position of root to enemy position
        root.transform.position = transform.position;

        // Set enemy as child of root
        transform.SetParent(root.transform);

        // Create waypoint object
        GameObject waypoints = new GameObject("Waypoints");

        // Reset position of waypoint to root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        // Create two points (gameobject) and reset their position to waypoint objects
        GameObject point1 = new GameObject("Point1");
        point1.transform.SetParent(waypoints.transform);
        point1.transform.position = root.transform.position;

        GameObject point2 = new GameObject("Point2");
        point2.transform.SetParent(waypoints.transform);
        point2.transform.position = root.transform.position;

        // Init points list then add the points to the list
        points = new List<Transform>();
        points.Add(point1.transform);
        points.Add(point2.transform);


    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, goalPoint.position) < .1f)
        {
            if (nextID == points.Count - 1)
            {
                idChangeValue = -1;
            }
            if (nextID == 0)
            {
                idChangeValue = 1;
            }
            nextID += idChangeValue;
        }
    }
}
