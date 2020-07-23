using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PassageManager : MonoBehaviour
{
    private const int WAYPOINT_CHECK_DISTANCE = 10;
    public TrackDriver trackDriver;
    public GameObject ship;
    public GameObject staticDTG;
    public Vector3 waypoint;
    int currentWaypointIndex = 0;
    float[] distances = null;
    float passageDurationInMinutes = 15f;
    Vector3 lastPostion;

    IEnumerator UpdateDisplay()
    {
        while (true)
        {
            TextMesh textMesh = staticDTG.GetComponent<TextMesh>();
            textMesh.text = CreateDisplayText();
            yield return new WaitForSeconds(1);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //XRSettings.eyeTextureResolutionScale = 1.5f;
        CalculateDistances();
        StartCoroutine(UpdateDisplay());

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


    string CreateDisplayText()
    {
        string distance = DistanceToGo().ToString("F2") + " NM to go";
        string timeToGo = "(" + CalculateDeltaTimeFromPlannedETA() + ")";
        string set = GetTidalSetToString();
        string courseMadeGood = trackDriver.CMG.ToString();
        string speedMadeGood = trackDriver.SMG.ToString("F1");

        string line_1 = distance + " " + timeToGo;
        string line_2 = "Set: " + GetTidalSetToString();
        string line_3 = "Course: " + " 000 " + " (" + courseMadeGood + ")";
        string line_4 = "Speed: " + SpeedRequired().ToString("F2") + " (" + speedMadeGood + ")" + " kts";
        return line_1 + "\n" + line_2 + "\n" + line_3 + "\n" + line_4;

        //textMesh.text = "" + DistanceToGo().ToString("F2") + "nm to go\nSpeed required " + SpeedRequired().ToString("F2") + "kts";
    }

    string CalculateDeltaTimeFromPlannedETA()
    {
        float speedMadeGood = trackDriver.SMG;
        float distanceToGo = DistanceToGo();

        float timeToGo = (distanceToGo / speedMadeGood);
        float timeToETA = TimeRemainingInHours();

        float deltaInMinutes = (timeToGo - timeToETA) * 60f;

        string sign = deltaInMinutes > 0 ? "+" : "";

        return sign + deltaInMinutes.ToString("F0") + " mins";
    }

    string GetTidalSetToString()
    {
        Vector3 tidalSet = trackDriver.tidalSet;

        int bearing = (int)Vector3.Angle(tidalSet, transform.forward);
        float speed = tidalSet.magnitude;

        return "" + bearing + "°R @ " + speed.ToString("F1") + " kt";
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

        return (distanceToNextWaypoint + remainingDistanceAfterNextWaypoint) / 1852f;
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
