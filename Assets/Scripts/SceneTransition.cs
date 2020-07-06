using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneTransition : MonoBehaviour
{
    public GameObject panel;
    private String newScene;
    public float fadeInSpeed = 2f;
    public float fadeOutSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        panel.GetComponent<Image>().CrossFadeAlpha(0, fadeInSpeed, true);
    }

    public void TransitionOut(string scene)
    {
        newScene = scene;
        StartCoroutine(FadeIn());
        
    }

    public void Exit()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private IEnumerator FadeIn()
    {
        panel.GetComponent<Image>().CrossFadeAlpha(1, fadeOutSpeed, true);
        yield return new WaitForSeconds(fadeOutSpeed);
        SceneManager.LoadScene(newScene, LoadSceneMode.Single);
    }

}
