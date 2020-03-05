using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    private const int DTG_CHECK_DISTANCE = 100;
    private const int WAYPOINT_CHECK_DISTANCE = 20;

    public GameObject[] waypoints;
    public GameObject[] DTG_Bubbles;
    public GameObject ship;

    public Vector3 waypoint;

    int currentWaypointIndex = 0;
    int currentDtgIndex = 0;

    float[] distances = null;


    // Start is called before the first frame update
    void Start()
    {
        waypoint = waypoints[0].transform.position;

        waypoint = new Vector3(waypoint.x, ship.transform.position.y,
            waypoint.z);

        for (int i = 1; i < DTG_Bubbles.Length; i++)
        {
            DTG_Bubbles[i].SetActive(false);
        }

        CalculateDistances();

        for (int i = 0; i < distances.Length; i++)
        {
            Debug.Log("" + i + ": " + distances[i]);
        }

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
        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, ship.transform.position) < WAYPOINT_CHECK_DISTANCE)
        {
            if (currentWaypointIndex < waypoints.Length)
            {
                currentWaypointIndex++;
            }
            waypoint = waypoints[currentWaypointIndex].transform.position;
            waypoint = new Vector3(waypoint.x, ship.transform.position.y, waypoint.z);
        }

        var t = 120f * Time.deltaTime;

        ship.transform.position = Vector3.MoveTowards(ship.transform.position, waypoint, t);

        Vector3 relativePos = waypoint - ship.transform.position;
        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.LookRotation(-relativePos), 0.2f * Time.deltaTime);

        CheckDistanceToGoBubble();


    }

    void CheckDistanceToGoBubble()
    {
        if (Vector3.Distance(DTG_Bubbles[currentDtgIndex].transform.position,
            ship.transform.position) < DTG_CHECK_DISTANCE)
        {
            DTG_Bubbles[currentDtgIndex].SetActive(false);
            currentDtgIndex++;

            if (currentDtgIndex < DTG_Bubbles.Length)
            {
                DTG_Bubbles[currentDtgIndex].SetActive(true);
            }

        }

        TextMesh textMesh = DTG_Bubbles[currentDtgIndex].GetComponent<TextMesh>();
        textMesh.text = "" + DistanceToGo() + "nm to go\nSpeed required " + SpeedRequired() + "kts";
    }

    float DistanceToGo()
    {
        float distanceToNextWaypoint = Vector3.Distance(waypoints[currentWaypointIndex].transform.position, ship.transform.position);



        return 0f;
    }

    float SpeedRequired()
    {
        return 0f;
    }
}
