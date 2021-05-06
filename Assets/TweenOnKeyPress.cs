using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TweenOnKeyPress : MonoBehaviour
{
    public GameObject objectToTween;
    public KeyCode key;
    public string sceneToLoad;
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            objectToTween.SendMessage("Tween");
            StartCoroutine(SwapScene());
        }
    }

    IEnumerator SwapScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
