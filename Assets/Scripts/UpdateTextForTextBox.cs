using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateTextForTextBox : MonoBehaviour
{
    private const int METRES_PER_NAUTICAL_MILE = 1852;
    private const float YARDS_PER_METRE = 1.094f;

    public string name;
    TextMeshPro textMeshProUGUI;
    public GameObject ship;
    public GameObject thisShip;
    private TrackDriver trackDriver;

    Vector3 previousPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshPro>();
        textMeshProUGUI.text = name;

        trackDriver = thisShip.GetComponent<TrackDriver>();
    }

    // Update is called once per frame
    void Update()
    {

        string speed = getShipSpeed();

        float range = Vector3.Distance(thisShip.transform.position, ship.transform.position);

        textMeshProUGUI.text = name + "\n" +
            "Speed: " + speed + "Kts" + "\n" +
            "Range: " + (range * YARDS_PER_METRE).ToString("F0") + "yds";

        previousPosition = thisShip.transform.position;

    }

    private string getShipSpeed()
    {
        if (previousPosition == Vector3.zero)
        {
            return trackDriver.shipSpeed.ToString("F1");
        }

        Vector3 currentPosition = thisShip.transform.position;
        float distance = Vector3.Distance(previousPosition, currentPosition) / METRES_PER_NAUTICAL_MILE;

        float time = Time.deltaTime / 3600;
        float speed = 0;

        if (time != 0)
        {
            speed = distance / time;
        }

        return speed.ToString("F1");
    }
}
