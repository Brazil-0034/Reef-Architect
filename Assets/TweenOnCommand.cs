using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenOnCommand : MonoBehaviour
{
    public bool start = false;
    public bool startLeave;
    public Vector3 targetFromHere;
    private Vector3 target;
    private Vector3 targetLeave;
    private Vector3 startPos;
    public bool willLeave = true;
    private bool left = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        target = transform.position + targetFromHere;
        targetLeave = transform.position + targetFromHere + targetFromHere;
    }
    public void Tween()
    {
        start = true;
    }
    public void UnTween()
    {
        willLeave = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 5);
            if (Vector3.Distance(transform.position, target) < 0.2f && left == false)
            {
                left = true;
                StartCoroutine(PrepareToLeave());
            }
        }

        if (startLeave && willLeave)
        {
            transform.position = Vector3.Lerp(transform.position, targetLeave, Time.deltaTime * 5);
            if (Vector3.Distance(transform.position, targetLeave) < 0.2f)
            {
                //re-init
                startLeave = false;
                start = false;
                left = false;
                transform.position = startPos;
            }
        }
    }

    IEnumerator PrepareToLeave()
    {
        yield return new WaitForSeconds(8);
        start = false;
        startLeave = true;
    }
}
