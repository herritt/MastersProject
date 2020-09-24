using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    public GameObject ownship;
    private const float YARDS_PER_METRE = 1.094f;
    private bool toggledOff = false;
    public float rangeToToggleOff = 40;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateText(0.25f));
    }

    private IEnumerator UpdateText(float seconds)
    {
        while (true)
        {
            UpdateDistanceText(seconds);

            yield return new WaitForSeconds(seconds);
        }

    }

    private void UpdateDistanceText(float seconds)
    {
        range = Vector3.Distance(gameObject.transform.position, ownship.transform.position) * YARDS_PER_METRE;

        textMeshProUGUI.text = range.ToString("F0");

        GameObject parent = gameObject.transform.parent.gameObject;
        Canvas canvas = parent.GetComponent<Canvas>();

        Transform postTransform = parent.transform.Find("Post");
        GameObject post = null;
        if (postTransform != null)
        {
            post = parent.transform.Find("Post").gameObject;
        }

        if (range < rangeToToggleOff && !toggledOff)
        {
            toggledOff = true;
            canvas.enabled = false;

            if (post != null)
            {
                post.SetActive(false);
            }

        }
        else if (range > rangeToToggleOff && toggledOff)
        {
            toggledOff = false;
            parent.GetComponent<Canvas>().enabled = true;

            if (post != null)
            {
                post.SetActive(true);
            }

        }

    }

}
