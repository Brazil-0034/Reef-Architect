using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CursorScript : MonoBehaviour
{
    public GameObject currentBlock;
    public GameObject bubbles;
    public Quaternion[] standardRotation;
    public static int coralCount = 0;
    public GameObject eater;
    public List<Vector3> usedSlots = new List<Vector3>();
    // Update is called once per frame

    public void SetCoral(GameObject g)
    {
        currentBlock = g;
    }
    
    void Update()
    {
        Camera c = Camera.main;
        Vector2 mPos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        bool mb = false;
        bool rmb = false;

        if (Input.GetMouseButton(0))
        {
            mb = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rmb = true;
        }

        if (Physics.Raycast(ray, out hit))
        {
            if (rmb)
            {
                Instantiate(eater, RoundVector(hit.point), hit.transform.rotation);
            }
            switch (hit.transform.tag)
            {
                case "SeaBed":
                    transform.position = Vector3.Lerp(transform.position, RoundVector(hit.point), 25 * Time.deltaTime);
                    if (mb)
                    {
                        if (GameObject.Find("SeabedHelpText")) GameObject.Find("SeabedHelpText").GetComponent<TextMeshPro>().enabled = false;
                        Vector3 objPoint = new Vector3(hit.point.x, hit.point.y-0.5f, hit.point.z);
                        objPoint = RoundVector(objPoint);
                        if (CoralPlacedAtPoint(objPoint) == false)
                        {
                            coralCount++;
                            usedSlots.Add(objPoint);
                            Quaternion rot = standardRotation[Random.Range(0, standardRotation.Length)];
                            Instantiate(currentBlock, RoundVector(objPoint), rot);
                            AudioSource a = GameObject.Find("SandSFX").GetComponent<AudioSource>();
                            a.pitch = Random.Range(0.5f, 2);
                            a.Play();
                            GameObject.Destroy(Instantiate(bubbles, RoundVector(objPoint), rot), 1);
                        }

                        if (coralCount > 4)
                        {
                            GameObject.Find("StartSimHelpText").GetComponent<TextMeshPro>().enabled = true;
                        }
                    }
                    break;
                case "UI":
                    if (mb) 
                    {
                        hit.transform.gameObject.SendMessage("Click");
                    }
                    break;
            }
        }
    }
    
    public bool CoralPlacedAtPoint(Vector3 v)
    {
        for (int i = 0; i < usedSlots.Count; i++)
        {
            if (Vector3.Distance(usedSlots[i],v) < 1)
            {
                return true;
            }
        }
        return false;
    }

    public void removeFromUsed(Vector3 v)
    {
        for (int i = 0; i < usedSlots.Count; i++)
        {
            if (Vector3.Distance(usedSlots[i], v) < 1)
            {
                usedSlots[i] = new Vector3(80085,8008135,80085);
                //lazy? nah
            }
        }
    }

    Vector3 RoundVector(Vector3 vec)
    {
        return new Vector3(Mathf.Round(vec.x), vec.y+0.1f, Mathf.Round(vec.z));
    }
}
