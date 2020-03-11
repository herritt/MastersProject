using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDriver : MonoBehaviour
{
    private const int WAYPOINT_CHECK_DISTANCE = 10;
    private const float KTS_TO_MPS = 1.944f;

    public GameObject[] waypoints;
    public Vector3 waypoint;
    int currentWaypointIndex = 0;
    public float shipSpeed = 11;
    GameObject ship;


    private void Awake()
    {
        ship = gameObject;
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoint = waypoints[currentWaypointIndex].transform.position;
            waypoint = new Vector3(waypoint.x, ship.transform.position.y, waypoint.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {       
        waypoint = waypoints[0].transform.position;

        // convert shipSpeed from kts to units per second
        shipSpeed /= KTS_TO_MPS;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypointIndex == waypoints.Length) return;

        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, ship.transform.position) < WAYPOINT_CHECK_DISTANCE)
        {
            if (currentWaypointIndex < waypoints.Length)
            {
                currentWaypointIndex++;
            }

            if (currentWaypointIndex < waypoints.Length)
            {
                waypoint = waypoints[currentWaypointIndex].transform.position;
            }


        }

        //var t = shipSpeed * Time.deltaTime;

        //float tidalSetBearing = 45;
        //float tidalSetSpeed = 50000f; // .5 m/s or roughly 1 kt
        //float distance = tidalSetSpeed * Time.deltaTime;

        //Vector3 tidalPos = Quaternion.AngleAxis(tidalSetBearing, Vector3.up) * Vector3.forward * distance;

        //Debug.Log(tidalPos);

        ship.transform.position = Vector3.MoveTowards(ship.transform.position, waypoint + tidalPos, t);

        Vector3 relativePos = waypoint - ship.transform.position;
        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.LookRotation(-relativePos), shipSpeed / 30 * Time.deltaTime);

  

    }

}
