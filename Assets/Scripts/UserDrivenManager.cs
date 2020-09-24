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
    public float fadeInSpeed = 2f;
    public float fadeOutSpeed = 2f;
    public TextMeshProUGUI message;

    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        distanceDisplay = DistanceDisplay.GetComponent<DistanceDisplay>();
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
        StartCoroutine(FadeInWithMessage("Range"));
    }

    private IEnumerator FadeInWithMessage(string text)
    {
        panel.GetComponent<Image>().CrossFadeAlpha(1, fadeOutSpeed, true);
        yield return new WaitForSeconds(fadeOutSpeed);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        message.text = text;


    }
}
