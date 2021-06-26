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

    private TextMeshProUGUI textMeshProUGUI;
    public GameObject ownship;
    public GameObject thisShip;
    private TrackDriver trackDriver;

    private Vector3 thisShipLastPosition;
    private Vector3 ownshipLastPostion;

    private float[] speeds = new float[5];
    private int speedIndex;
    private int NUM_SPEEDS_TO_AVERAGE = 5;

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
        float speed = trackDriver.AverageSpeed();
        float range = getRangeInYards();



        textMeshProUGUI.text =
            "Speed: \t" + speed.ToString("F1") + "Kts" + "\n" +
            "Range: \t" + range.ToString("F0") + "yds\n" +
            "CPA: \t\t" + cpa.ToString("F0") + "yds";

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

        CPA cpaCalculator = new CPA();
        Track ownShipTrack = new Track();
        ownShipTrack.p0 = ownship.transform.position;
        float ownShipSpeed_MPS = ownship.transform.GetComponentInParent<TrackDriver>().shipSpeed;
        Vector3 ownShipVelocityVector = (ownship.transform.position - ownshipLastPostion) / ownShipSpeed_MPS;
        ownShipTrack.v = ownShipVelocityVector;

        Track thisShipTrack = new Track();
        thisShipTrack.p0 = thisShip.transform.position;

        float denominator = trackDriver.shipSpeed;
        if (denominator == 0 || float.IsNaN(denominator))
        {
            denominator = 0.00001f;
        }

        Vector3 thisShipVelocityVector = (thisShip.transform.position - thisShipLastPosition) / denominator;
        thisShipTrack.v = thisShipVelocityVector;

        float cpa = cpaCalculator.CalculateCPADistance(ownShipTrack, thisShipTrack);

        thisShipLastPosition = thisShip.transform.position;
        ownshipLastPostion = ownship.transform.position;

        if (float.IsNaN(cpa))
        {
            cpa = 0f;
        }

        return cpa;
    }

    public float getRangeInYards()
    {
        float range = Vector3.Distance(thisShip.transform.position, ownship.transform.position) * YARDS_PER_METRE;

        if (float.IsNaN(range))
        {
            range = 0f;
        }

        return range;
    }
}
