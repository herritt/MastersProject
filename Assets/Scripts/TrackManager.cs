using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TrackManager : MonoBehaviour
{
    private const int WAYPOINT_CHECK_DISTANCE = 10;

    public TrackDriver trackDriver;

    public GameObject ship;

    public GameObject staticDTG;

    public Vector3 waypoint;

    int currentWaypointIndex = 0;
    int currentDtgIndex = 0;

    float[] distances = null;

    float passageDurationInMinutes = 15f;

    // Start is called before the first frame update
    void Start()
    {
        //XRSettings.eyeTextureResolutionScale = 1.5f;
        CalculateDistances();

    }

    void CalculateDistances()
    {
        GameObject[] waypoints = trackDriver.waypoints;

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

        CheckDistanceToGoBubble();

    }

    void CheckDistanceToGoBubble()
    { 
        TextMesh textMesh = staticDTG.GetComponent<TextMesh>();
        textMesh.text = "" + DistanceToGo().ToString("F2") + "nm to go\nSpeed required " + SpeedRequired().ToString("F2") + "kts";
    }

    float DistanceToGo()
    {
        GameObject[] waypoints = trackDriver.waypoints;
        float distanceToNextWaypoint = Vector3.Distance(waypoints[currentWaypointIndex].transform.position, ship.transform.position);

        float remainingDistanceAfterNextWaypoint = 0f;

        for (int i = currentWaypointIndex; i < distances.Length; i++)
        {
            remainingDistanceAfterNextWaypoint += distances[i];
        }

        return (distanceToNextWaypoint + remainingDistanceAfterNextWaypoint)/1852f;
    }

    float SpeedRequired()
    {
        float distance = DistanceToGo();
        float time = TimeRemainingInHours();

        return distance / time;
    }

    float TimeRemainingInHours()
    {

        float timeInSecondsSinceStart = Time.time;
        float remainingTimeInSeconds = (passageDurationInMinutes * 60f) - timeInSecondsSinceStart;

        return remainingTimeInSeconds / 3600;

    }
}
