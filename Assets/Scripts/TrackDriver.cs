using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDriver : MonoBehaviour
{
    private const int WAYPOINT_CHECK_DISTANCE = 20;
    private const float KTS_TO_MPS = 1.944f;

    public GameObject[] waypoints;
    public Vector3 waypoint;
    int currentWaypointIndex = 0;
    public float shipSpeed = 11;
    public GameObject ship;
    public Rigidbody m_Rigidbody;
    public Vector3 relativePos;

    // Start is called before the first frame update
    void Awake()
    {
        ship = gameObject;
        waypoint = waypoints[0].transform.position;
        waypoint = new Vector3(waypoint.x, ship.transform.position.y, waypoint.z);

        // convert shipSpeed from kts to units per second
        shipSpeed /= KTS_TO_MPS;
        m_Rigidbody = GetComponent<Rigidbody>();

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
                waypoint = new Vector3(waypoint.x, ship.transform.position.y, waypoint.z);
            }


        }

        var t = shipSpeed * Time.deltaTime;

        relativePos = waypoint - ship.transform.position;
        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.LookRotation(-relativePos), shipSpeed / 30 * Time.deltaTime);

        //calculate a position ahead of the ship based on current heading and move towards it
        Vector3 newPosition = ship.transform.rotation * Vector3.forward;

        //ship.transform.position = Vector3.MoveTowards(ship.transform.position, newPosition, t);
        m_Rigidbody.velocity = -transform.forward * shipSpeed;

    }

}
