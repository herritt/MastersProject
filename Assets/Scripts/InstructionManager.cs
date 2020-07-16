using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    public List<String> descriptions;
    public List<Image> images;
    public Image image;
    public TextMeshProUGUI description;

    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject startButton;

    private int slideNumber = 0;

    private void Start()
    {
        LoadSlide(slideNumber);
    }

    public void NextSlide()
    {
        slideNumber++;
        LoadSlide(slideNumber);
    }

    public void PreviousSlide()
    {
        slideNumber--;
        LoadSlide(slideNumber);
    }

    public void LoadSlide(int slideNumber)
    {
        if (descriptions.ElementAtOrDefault(slideNumber) == null)
        {
            return;
        }

        image = images[slideNumber];
        description.text = descriptions[slideNumber];

        if (slideNumber == 0)
        {
            previousButton.SetActive(false);
        }
        else
        {
            previousButton.SetActive(true);
        }

        if (slideNumber == descriptions.Count - 1)
        {
            nextButton.SetActive(false);
            startButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            startButton.SetActive(false);
        }

    }

}
