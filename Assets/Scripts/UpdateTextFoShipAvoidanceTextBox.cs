using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpdateTextFoShipAvoidanceTextBox : MonoBehaviour
{
    private const int METRES_PER_NAUTICAL_MILE = 1852;
    private const float YARDS_PER_METRE = 1.094f;

    TextMeshProUGUI textMeshProUGUI;
    public GameObject ownship;
    public GameObject thisShip;
    private TrackDriver trackDriver;

    private Vector3 thisShipLastPosition;
    private Vector3 ownshipLastPostion;

    private float[] speeds = new float[5];
    private int speedIndex;
    private int NUM_SPEEDS_TO_AVERAGE = 5;

    Vector3 previousPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = name;

        trackDriver = thisShip.GetComponent<TrackDriver>();

        thisShipLastPosition = thisShip.transform.position;
        ownshipLastPostion = ownship.transform.position;

        StartCoroutine(UpdateText(0.25f));
    }

    private IEnumerator UpdateText(float seconds)
    {
        while (true)
        {
            UpdateShipAvoidanceText(seconds);

            yield return new WaitForSeconds(seconds);
        }

    }

    private void UpdateShipAvoidanceText(float seconds)
    {
        float cpa = CalculateCPA() * YARDS_PER_METRE;
        float speed = CalculateShipSpeed(seconds);

        float range = Vector3.Distance(thisShip.transform.position, ownship.transform.position);

        textMeshProUGUI.text =
            "Speed: \t" + AverageSpeed().ToString("F1") + "Kts" + "\n" +
            "Range: \t" + (range * YARDS_PER_METRE).ToString("F0") + "yds\n" +
            "CPA: \t\t" + cpa.ToString("F0") + "yds";

        previousPosition = thisShip.transform.position;

        //highlight panel if cpa is within 200 yards
        Image image = gameObject.transform.parent.GetComponentInChildren<Image>();

        if (cpa < 100f)
        {
            image.color = Color.red;
        }
        else if (cpa < 200f)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.white;
        }
    }

    private float CalculateCPA()
    {
        /*
        CPA cpaCalculator = new CPA();
        Track ownShipTrack = new Track();
        ownShipTrack.p0 = ownship.transform.position;
        float ownShipSpeed_MPS = ownship.transform.GetComponentInParent<TrackDriver>().shipSpeed;
        Vector3 ownShipVelocityVector = (ownship.transform.GetComponentInParent<TrackDriver>().waypoint - ownship.transform.position) / ownShipSpeed_MPS;
        ownShipTrack.v = ownShipVelocityVector;

        Track thisShipTrack = new Track();
        thisShipTrack.p0 = thisShip.transform.position;
        Vector3 thisShipVelocityVector = (trackDriver.waypoint - thisShip.transform.position) /
            trackDriver.shipSpeed;
        thisShipTrack.v = thisShipVelocityVector;

        float cpa = cpaCalculator.CalculateCPADistance(ownShipTrack, thisShipTrack);
        */


        CPA cpaCalculator = new CPA();
        Track ownShipTrack = new Track();
        ownShipTrack.p0 = ownship.transform.position;
        float ownShipSpeed_MPS = ownship.transform.GetComponentInParent<TrackDriver>().shipSpeed;
        Vector3 ownShipVelocityVector = (ownship.transform.position - ownshipLastPostion) / ownShipSpeed_MPS;
        ownShipTrack.v = ownShipVelocityVector;

        Track thisShipTrack = new Track();
        thisShipTrack.p0 = thisShip.transform.position;
        Vector3 thisShipVelocityVector = (thisShip.transform.position - thisShipLastPosition) / trackDriver.shipSpeed;
        thisShipTrack.v = thisShipVelocityVector;

        float cpa = cpaCalculator.CalculateCPADistance(ownShipTrack, thisShipTrack);

        thisShipLastPosition = thisShip.transform.position;
        ownshipLastPostion = ownship.transform.position;

        return cpa;
    }

    private float AverageSpeed()
    {
        int num_non_zeros = 0;
        float sum = 0.0f;

        for (int i = 0; i < NUM_SPEEDS_TO_AVERAGE; i++)
        {
            if (Math.Abs(speeds[i]) < 0.001f)
            {
                num_non_zeros++;
            }
            sum += speeds[i];

        }

        return sum / (NUM_SPEEDS_TO_AVERAGE - num_non_zeros);
    }

    private float CalculateShipSpeed(float seconds)
    {
        if (previousPosition == Vector3.zero)
        {
            return trackDriver.shipSpeed;
        }

        Vector3 currentPosition = thisShip.transform.position;
        float distance = Vector3.Distance(previousPosition, currentPosition) / METRES_PER_NAUTICAL_MILE;

        float time = seconds / 3600;
        float speed = 0;

        if (time != 0)
        {
            speed = distance / time;
        }

        speeds[speedIndex] = speed;
        speedIndex = (speedIndex + 1 ) % NUM_SPEEDS_TO_AVERAGE;
        
        return speed;
    }
}
