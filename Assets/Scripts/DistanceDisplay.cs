﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    public GameObject ownship;
    private const float YARDS_PER_METRE = 1.094f;

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
        float range = Vector3.Distance(gameObject.transform.position, ownship.transform.position) * YARDS_PER_METRE;

        textMeshProUGUI.text = range.ToString("F0");

        GameObject parent = gameObject.transform.parent.gameObject;
        Canvas canvas = parent.GetComponent<Canvas>();

        Transform postTransform = parent.transform.Find("Post");
        GameObject post = null;
        if (postTransform != null)
        {
            post = parent.transform.Find("Post").gameObject;
        }
        

        if (range < 40)
        {
            canvas.enabled = false;

            if (post != null)
            {
                post.SetActive(false);
            }

            

        } else if (!canvas.isActiveAndEnabled)
        { 
            parent.GetComponent<Canvas>().enabled = true;

            if (post != null)
            {
                post.SetActive(true);
            }
            
        }

}

}
