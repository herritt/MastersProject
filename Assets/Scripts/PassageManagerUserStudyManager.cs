using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageManagerUserStudyManager : MonoBehaviour
{
    private float timeToSlowDown = 4f;
    public GameObject ownship;
    public GameObject instructionDisplayObject;
    public GameObject passageManagerObject;

    private TrackDriver trackDriver;
    private DisplayUserStudyInstructions instructionDisplay;
    private PassageManager passageManager;

    // Start is called before the first frame update
    void Start()
    {
        trackDriver = ownship.GetComponent<TrackDriver>();
        instructionDisplay = instructionDisplayObject.GetComponent<DisplayUserStudyInstructions>();
        passageManager = passageManagerObject.GetComponent<PassageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //for now we just want to slow down at a certain point in the user study
        if (instructionDisplay.instruction.text.Contains("slow down"))
        {
            float newSpeed = passageManager.SpeedRequired() / TrackDriver.KTS_TO_MPS;
            trackDriver.shipSpeed = newSpeed;
        }

    }
}
