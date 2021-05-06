using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSlide : MonoBehaviour
{
    public GameObject nextSlide;
    public bool isLastSlide;
    public bool isActive;
    // Start is called before the first frame update
    void toggleVisibility(bool visible)
    {
        StartCoroutine(DelayedAction(visible));
    }

    IEnumerator DelayedAction(bool visible)
    {
        yield return new WaitForEndOfFrame();
        isActive = visible;
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(visible);
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = visible;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            Debug.Log(gameObject.name + " - " + isActive);
            toggleVisibility(false);
            if (!isLastSlide) nextSlide.SendMessage("toggleVisibility", true);
            if (isLastSlide) GameObject.Find("TutorialOverlay").SendMessage("Tween");
        }
    }
}
