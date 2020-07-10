using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    Vector3 previousPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = name;

        trackDriver = thisShip.GetComponent<TrackDriver>();

        thisShipLastPosition = thisShip.transform.position;
        ownshipLastPostion = ownship.transform.position;

        StartCoroutine(UpdateText());
    }

    private IEnumerator UpdateText()
    {
        while (true)
        {
            float cpa = CalculateCPA();
            float speed = CalculateShipSpeed();

            float range = Vector3.Distance(thisShip.transform.position, ownship.transform.position);

            textMeshProUGUI.text = 
                "Speed: \t" + speed.ToString("F1") + "Kts" + "\n" +
                "Range: \t" + (range * YARDS_PER_METRE).ToString("F0") + "yds\n" +
                "CPA: \t\t" + (cpa * YARDS_PER_METRE).ToString("F0") + "yds"; 

            previousPosition = thisShip.transform.position;

            yield return new WaitForSeconds(1);
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

    private float CalculateShipSpeed()
    {
        if (previousPosition == Vector3.zero)
        {
            return trackDriver.shipSpeed;
        }

        Vector3 currentPosition = thisShip.transform.position;
        float distance = Vector3.Distance(previousPosition, currentPosition) / METRES_PER_NAUTICAL_MILE;

        float time = 1f / 3600;
        float speed = 0;

        if (time != 0)
        {
            speed = distance / time;
        }

        return speed;
    }
}
