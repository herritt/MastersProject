using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuButtonHover : MonoBehaviour
{
    private TextMeshProUGUI text;

    public Color inActiveColour;
    public Color activeColour;

    public void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnMouseOver()
    {
        text.color = activeColour;
    }

    public void OnMouseExit()
    {

        text.color = inActiveColour;
    }

}
