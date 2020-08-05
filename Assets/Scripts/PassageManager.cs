using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PassageManager : MonoBehaviour
{
    public TrackDriver trackDriver;
    public GameObject ship;
    public GameObject staticDTG;
    public Vector3 waypoint;
    float[] distances = null;
    float passageDurationInMinutes = 10;
    Vector3 lastPostion;

    IEnumerator UpdateDisplay()
    {
        while (true)
        {
            TextMesh textMesh = staticDTG.GetComponent<TextMesh>();
            textMesh.text = CreateDisplayText();
            yield return new WaitForSeconds(0.25f);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //XRSettings.eyeTextureResolutionScale = 1.5f;
        CalculateDistances();
        StartCoroutine(UpdateDisplay());

    }

    void Update()
    {
        
    }

    void CalculateDistances()
    {
        GameObject[] waypoints = trackDriver.waypoints;

        distances = new float[waypoints.Length];

        //distances[0] = Vector3.Distance(ship.transform.position, waypoints[0].transform.position);

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
        string heading = ((int)trackDriver.GetHeading()).ToString("D3");
        string courseMadeGood = ((int)trackDriver.CMG).ToString("D3");
        string speedMadeGood = trackDriver.SMG.ToString("F1");

        string line_1 = distance + " " + timeToGo;
        string line_2 = "Set: " + GetTidalSetToString();
        string line_3 = "HDG: " + heading + " (CMG: " + courseMadeGood + ")";
        string line_4 = "SR: " + SpeedRequired().ToString("F1") + " (SMG: " + speedMadeGood + ")" + " kts";

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

        float seconds = (deltaInMinutes - (int)deltaInMinutes) * 60f;
        seconds = Mathf.Abs(seconds);       

        string sign = deltaInMinutes > 0 ? "+" : "-";

        deltaInMinutes = Mathf.Abs(deltaInMinutes);
        deltaInMinutes = Mathf.Floor(deltaInMinutes);

        return sign + deltaInMinutes.ToString("F0") + " mins " + seconds.ToString("F0") + " secs";
    }

    string GetTidalSetToString()
    {
        float tidalSetSpeedInKts = trackDriver.tidalSetSpeed * TrackDriver.KTS_TO_MPS;

        return "" + trackDriver.tidalSetBearing.ToString("D3") + "° @ " +
            tidalSetSpeedInKts.ToString("F1") + " kt";
    }

    float DistanceToGo()
    {
        GameObject[] waypoints = trackDriver.waypoints;
        int index = trackDriver.currentWaypointIndex;
        float distanceToNextWaypoint = Vector3.Distance(waypoints[index].transform.position, ship.transform.position);

        float remainingDistanceAfterNextWaypoint = 0f;

        for (int i = index; i < distances.Length; i++)
        {
            remainingDistanceAfterNextWaypoint += distances[i];
        }

        return (distanceToNextWaypoint + remainingDistanceAfterNextWaypoint) / 1852f;
    }

    public float SpeedRequired()
    {
        float distance = DistanceToGo();
        float time = TimeRemainingInHours();

        return distance / time;
    }

    float TimeRemainingInHours()
    {

        float timeInSecondsSinceStart = Time.timeSinceLevelLoad;
        float remainingTimeInSeconds = (passageDurationInMinutes * 60f) - timeInSecondsSinceStart;

        return remainingTimeInSeconds / 3600;

    }
}
