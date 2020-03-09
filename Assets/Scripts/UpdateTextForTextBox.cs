using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTextForTextBox : MonoBehaviour
{
    public string name;
    TextMeshPro textMeshProUGUI;
    public GameObject ship;
    public GameObject thisShip;
    private TrackDriver trackDriver;

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
        string speed = "" + trackDriver.shipSpeed;
        float range = Vector3.Distance(thisShip.transform.position, ship.transform.position);

        textMeshProUGUI.text = name + "\n" +
            "Speed: " + speed + "Kts" + "\n" +
            "Range: " + (range * 1.094).ToString("F0") + "yds";

    }
}
