using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillScript : MonoBehaviour
{
    public static List<GameObject> bleachedCorals = new List<GameObject>();
    public Material bleachedMaterial;
    public GameObject bubbles;
    public GameObject bleached;

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Substring(0, 6).Equals("Pillar"))
        {
            if (Random.Range(0, 10) <= 3) StartCoroutine(DamageCoral(col.gameObject));
        }
        else if (col.gameObject.tag.Equals("Coral"))
        {
            if (Random.Range(0, 10) <= 8) StartCoroutine(DamageCoral(col.gameObject));
        }
        if (col.gameObject.tag.Equals("Fish"))
        {
            if (Random.Range(0, 3) <= 2) col.gameObject.SendMessage("Kill");
        }
    }

    IEnumerator DamageCoral(GameObject g)
    {
        PlayButtonScript.deadcorals++;
        yield return new WaitForSeconds(Random.Range(2, 8));
        MeshRenderer meshRend = g.GetComponent<MeshRenderer>();


        if (meshRend.material.mainTexture == bleachedMaterial.mainTexture)
        {
            Instantiate(bubbles, g.transform.position, g.transform.rotation);
            GameObject.Destroy(g);
            PlayClip();
        }
        else
        {
            PlayClip();
            Vector3 gPos = g.transform.position;
            Vector3 bleachPos = new Vector3(gPos.x, gPos.y+2, gPos.z);
            Instantiate(bleached, bleachPos, g.transform.rotation);
            g.GetComponent<MeshRenderer>().material.mainTexture = bleachedMaterial.mainTexture;
            g.GetComponent<MeshRenderer>().material.color = Color.white;
            bleachedCorals.Add(g);


            foreach (Transform child in g.transform)
            {
                foreach (Material m in child.GetComponent<MeshRenderer>().materials)
                {
                    m.mainTexture = bleachedMaterial.mainTexture;
                    m.color = Color.white;
                }
            }
        }
    }
    
    void PlayClip()
    {
        AudioSource a = GameObject.Find("SandSFX").GetComponent<AudioSource>();
        a.pitch = Random.Range(0.5f, 2);
        a.Play();
    }
}
