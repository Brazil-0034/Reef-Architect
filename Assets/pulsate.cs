using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsate : MonoBehaviour
{
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float sin = (Mathf.Sin(Time.time) / 2.5f) + 1;
        transform.localScale = new Vector3(scale.x + sin, scale.y + sin, scale.z + sin) / 2;
    }
}
