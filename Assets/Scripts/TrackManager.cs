using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    private const int DTG_CHECK_DISTANCE = 100;
    private const int WAYPOINT_CHECK_DISTANCE = 20;

    public GameObject[] waypoints;
    public GameObject ship;

    public GameObject staticDTG;

    public Vector3 waypoint;

    int currentWaypointIndex = 0;
    int currentDtgIndex = 0;

    float[] distances = null;

    float shipSpeed = 15;


    // Start is called before the first frame update
    void Start()
    {
        waypoint = waypoints[0].transform.position;

        waypoint = new Vector3(waypoint.x, ship.transform.position.y,
            waypoint.z);


        CalculateDistances();

    }

    void CalculateDistances()
    {
        distances = new float[waypoints.Length - 1];

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            GameObject firstObject = waypoints[i];
            GameObject secondObject = waypoints[i + 1];

            float distance = Vector3.Distance(firstObject.transform.position, secondObject.transform.position);
            distances[i] = distance;

        }
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

        ship.transform.position = Vector3.MoveTowards(ship.transform.position, waypoint, t);

        Vector3 relativePos = waypoint - ship.transform.position;
        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.LookRotation(-relativePos), shipSpeed / 30 * Time.deltaTime);

        CheckDistanceToGoBubble();


    }

    void CheckDistanceToGoBubble()
    { 
        TextMesh textMesh = staticDTG.GetComponent<TextMesh>();
        textMesh.text = "" + DistanceToGo().ToString("F2") + "nm to go\nSpeed required " + SpeedRequired() + "kts";
    }

    float DistanceToGo()
    {
        float distanceToNextWaypoint = Vector3.Distance(waypoints[currentWaypointIndex].transform.position, ship.transform.position);

        float remainingDistanceAfterNextWaypoint = 0f;

        for (int i = currentWaypointIndex; i < distances.Length; i++)
        {
            remainingDistanceAfterNextWaypoint += distances[i];
        }

        return (distanceToNextWaypoint + remainingDistanceAfterNextWaypoint)/1852.0f;
    }

    float SpeedRequired()
    {
        return 0f;
    }
}
