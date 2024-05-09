using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] GameObject movePoints;
    List<Transform> wayPoint = new List<Transform>();
    int i;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in movePoints.transform)
        {
            wayPoint.Add(t);
        }
        if (wayPoint.Count > 0)
        {
            i = 0;
            transform.position = wayPoint[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (i == wayPoint.Count) i = 0;
        transform.position = Vector3.MoveTowards(transform.position, wayPoint[i].position, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, wayPoint[i].position) < 0.1f)
        {
            if (i < wayPoint.Count) i++;
        }
    }
}
