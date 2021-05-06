using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIdentityScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 original;
    public List<Vector3> targets = new List<Vector3>();
    public GameObject Bubbles;
    private Vector3 nextTarget;
    void Start()
    {
        original = transform.position;

        for (int i = 0; i < 5; i++)
        {
            Vector3 vec = (Random.insideUnitSphere * 3) + original;
            vec.y = -27;
            targets.Add(vec);
        }

        nextTarget = targets[0];
    }

    public void Kill()
    {
        PlayButtonScript.deadfish++;
        Destroy(Instantiate(Bubbles, transform.position, Quaternion.identity), 0.5f);
        Destroy(gameObject, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == nextTarget)
        {
            nextTarget = targets[Random.Range(0, targets.Count)];
        }

        transform.position = Vector3.MoveTowards(transform.position, nextTarget, 0.45f * Time.deltaTime);

        Vector3 dir = (nextTarget - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 2);
    }
}
