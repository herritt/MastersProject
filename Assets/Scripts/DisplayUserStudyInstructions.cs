using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayUserStudyInstructions : MonoBehaviour
{
    public string[] textToDisplay;
    public int[] displayDurations;
    public int[] pausesBetweenInstructions;
    public TextMeshProUGUI instruction;
    public SceneTransition sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayInstructions(0));
    }

    private IEnumerator DisplayInstructions(int index)
    {
        instruction.text = "";
        yield return new WaitForSeconds(pausesBetweenInstructions[index]);

        instruction.text = textToDisplay[index];
        yield return new WaitForSeconds(displayDurations[index]);

        if (index < textToDisplay.Length - 1)
        {
            index++;
            StartCoroutine(DisplayInstructions(index));
        }
        else
        {
            instruction.text = "";
            index++;
            yield return new WaitForSeconds(pausesBetweenInstructions[index]);
            sceneTransition.TransitionOut("UserStudyMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
