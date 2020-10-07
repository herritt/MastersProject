using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UserDrivenManager : MonoBehaviour
{
    private const float YARDS_PER_METRE = 1.094f;

    public GameObject DistanceDisplay;
    private DistanceDisplay distanceDisplay;
    bool monitoringRange = false;
    public float fadeOutSpeed = 1f;
    public TextMeshProUGUI message;
    public GameObject tryAgainButton;
    public GameObject exitButton;

    public GameObject[] otherShips;
    public GameObject ownship;

    public GameObject panel;

    public string rangeMessage;
    public string cpaMessage;

    public float otherShipRangeCutOff;
    
    // Start is called before the first frame update
    void Start()
    {
        distanceDisplay = DistanceDisplay.GetComponent<DistanceDisplay>();
        tryAgainButton.SetActive(false);
        exitButton.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        

        //check if more than 200 yards off track, and if so, end scene
        float range = distanceDisplay.range;

        if (range < 200) monitoringRange = true;
       
        if (range > 200f && monitoringRange)
        {
            TransitionOutDueToRange();
        }

        if (isTooCloseToOtherShip())
        {
            TransitionOutDueToCPAToOtherShip();
        }

        Debug.Log("Update");
    }

    private bool isTooCloseToOtherShip()
    {
        foreach (GameObject otherShip in otherShips)
        {
            float range = getRangeInYards(otherShip);

            if (range < otherShipRangeCutOff)
            {
                TransitionOutDueToCPAToOtherShip();
                   
            }
        }

        

        return false;
    }

    private void TransitionOutDueToCPAToOtherShip()
    {
        StartCoroutine(FadeInWithMessage(cpaMessage));
    }

    private void TransitionOutDueToRange()
    {
        StartCoroutine(FadeInWithMessage(rangeMessage));
    }

    private IEnumerator FadeInWithMessage(string text)
    {
        panel.GetComponent<Image>().CrossFadeAlpha(1, fadeOutSpeed, true);
        yield return new WaitForSeconds(fadeOutSpeed);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        tryAgainButton.SetActive(true);
        exitButton.SetActive(true);

        message.text = text;


    }

    public float getRangeInYards(GameObject otherShip)
    {
        return Vector3.Distance(otherShip.transform.position, ownship.transform.position) * YARDS_PER_METRE;
    }


}
