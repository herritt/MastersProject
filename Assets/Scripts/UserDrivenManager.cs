using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserDrivenManager : MonoBehaviour
{
    public GameObject DistanceDisplay;
    private DistanceDisplay distanceDisplay;
    public float rangeFromTrack = 0f;
    bool monitoringRange = false;
    public float fadeOutSpeed = 1f;
    public TextMeshProUGUI message;
    public GameObject tryAgainButton;
    public GameObject exitButton;

    public GameObject panel;

    public string rangeMessage;

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
        float range = distanceDisplay.range;

        if (range < 200) monitoringRange = true;
       
        if (range > 200f && monitoringRange)
        {
            TransitionOutDueToRange();
        }
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


}
