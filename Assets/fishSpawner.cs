using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject newFish;
    void Start()
    {
        StartCoroutine(SpawnFish());
    }

    // Update is called once per frame
    IEnumerator SpawnFish()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 20));
            if (Random.Range(0, 10) <= 3) Instantiate(newFish); PlayButtonScript.newfish++;
        }
    }
}
