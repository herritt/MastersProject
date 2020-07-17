using System.Collections;
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
        float range = Vector3.Distance(gameObject.transform.position, ownship.transform.position);

        textMeshProUGUI.text = (range * YARDS_PER_METRE).ToString("F0");
    }
}
