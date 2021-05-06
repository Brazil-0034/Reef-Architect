using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject currentChallenge;
    public static bool finished = false;
    public Material damagedMaterial;
    public Material bleachedMaterial;
    public GameObject Polluter;
    private Vector3 startPos;
    private bool clicked = false;
    public Quaternion standardRotation;
    public GameObject tint;
    public GameObject score;
    public static int deadfish;
    public static int deadcorals;
    public static int healedcorals;
    public static int newfish;

    void Start()
    {
        score.SetActive(false);
        startPos = transform.position;
        currentChallenge.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown("r")){
            GameObject.Find("Untransitioner").SendMessage("Tween");
            StartCoroutine(DelayReset());
        }
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(1);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
    void Click()
    {
        if (!clicked)
        {
            clicked = true;
            GameObject.Find("TweenPanel").SendMessage("Tween");
            StartCoroutine(EnableDrills());
            StartCoroutine(HideMainUI());
        }
    }

    IEnumerator EnableDrills()
    {
        yield return new WaitForSeconds(10);
        GameObject.Find("Drills").SendMessage("Tween");
        currentChallenge.SetActive(true);
        setCurrentChallenge("Current Challenge - Undersea Drilling");
        yield return new WaitForSeconds(8);
        GameObject.Find("Drills").SendMessage("UnTween");
        StartCoroutine(EnablePollution());
    }

    IEnumerator EnablePollution()
    {
        yield return new WaitForSeconds(2);
        setCurrentChallenge("Current Challenge - Water Pollution");
        Vector3 vec = GameObject.Find("Border").transform.position;
        vec.y +=2;
        GameObject.Destroy(Instantiate(Polluter, vec, standardRotation), 8);
        yield return new WaitForSeconds(8);
        StartCoroutine(EnableClimateChange());
    }

    IEnumerator EnableClimateChange()
    {
        yield return new WaitForSeconds(2);
        setCurrentChallenge("Current Challenge - Climate Change");
        tint.SendMessage("Tween");
        GameObject.Destroy(tint, 8);
        yield return new WaitForSeconds(8);
        setCurrentChallenge("You made it through! [R] To Try Again.");
        finished = true;
        score.SetActive(true);

        /*
- X Fish Population Died.
- X Corals were killed or bleached.
- X Corals were healed by Brain Corals.
- X New Fish were born.

Press [R] To Try Again!
        */


        string scoreTxt = "- <color=#03cafc>" + deadfish + "</color> Fish Population Died.\n";
        scoreTxt += "- <color=#03cafc>" + deadcorals + "</color> Corals were killed or bleached.\n";
        scoreTxt += "- Corals were healed <color=#03cafc>" + healedcorals + "</color> times by Brain Corals.\n";
        scoreTxt += "- <color=#03cafc>" + newfish + "</color> New Fish were born from Staghorn Corals. \n";
        scoreTxt += "<color=#03cafc>Total Score = " + getTotalScore() + " </color>";

        GameObject.Find("ScoreDesc").GetComponent<TextMeshPro>().text = scoreTxt;

    }

    int getTotalScore()
    {
        return healedcorals-deadcorals + 10 + newfish;
    }

    void setCurrentChallenge(string s)
    {
        currentChallenge.GetComponent<TextMeshPro>().text = s;
    }

    IEnumerator HideMainUI()
    {
        yield return new WaitForSeconds(2);
        if (GameObject.Find("Cursor")) GameObject.Find("Cursor").SetActive(false);
        if (GameObject.Find("MainUI")) GameObject.Find("MainUI").SetActive(false);
        transform.position = new Vector3(1000, 1000, 1000);
    }
}
