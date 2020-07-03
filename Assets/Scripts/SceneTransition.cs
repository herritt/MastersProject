using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public GameObject panel;
    public System.String newScene;
    public float fadeInSpeed = 2f;
    public float fadeOutSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        panel.GetComponent<Image>().CrossFadeAlpha(0, fadeInSpeed, true);
    }

    public void TransitionOut()
    {
        StartCoroutine(FadeThenLoadScene());
    }

    IEnumerator FadeThenLoadScene()
    {
        // start fading
        yield return StartCoroutine(FadeIn());
        // code here will run once the fading coroutine has completed
        SceneManager.LoadScene(newScene, LoadSceneMode.Single);
    }

    private IEnumerator FadeIn()
    {
        panel.GetComponent<Image>().CrossFadeAlpha(1, fadeOutSpeed, true);
        yield return new WaitForSeconds(fadeOutSpeed);
        
 
    }

}
