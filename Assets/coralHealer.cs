using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coralHealer : MonoBehaviour
{
    public Material healed;
    public GameObject heals;
    private int healCount = 0;
    public GameObject bubbles;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Healer());
    }

    // Update is called once per frame
    IEnumerator Healer()
    {
        while (healCount < 2)
        {
            yield return new WaitForSeconds(Random.Range(4, 6));
            if (DrillScript.bleachedCorals.Count > 0 && PlayButtonScript.finished == false)
            {
                GameObject n = DrillScript.bleachedCorals[DrillScript.bleachedCorals.Count-1];
                HealCoral(n);
                DrillScript.bleachedCorals.Remove(n);
                healCount++;
            }
        }
        Instantiate(bubbles, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }

    void HealCoral(GameObject g)
    {
        PlayButtonScript.healedcorals++;
        Vector3 hvec = g.transform.position;
        hvec.y++;
        Destroy(Instantiate(heals, hvec, Quaternion.identity), 0.5f);
        g.GetComponent<MeshRenderer>().material = healed;
        Debug.Log(1);

        foreach (Transform child in g.transform)
        {
            foreach (Material m in child.GetComponent<MeshRenderer>().materials)
            {
                Debug.Log(2);
                m.color = Color.green;
            }
        }
    }
}
