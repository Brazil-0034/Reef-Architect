using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPushDownScript : MonoBehaviour
{
    public int count;
    public GameObject target;
    public string method;
    // Update is called once per frame
    void Click()
    {
        StartCoroutine(PushMe());
    }

    IEnumerator PushMe()
    {
        for (int i = 0; i < count; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        target.SendMessage(method);
    }
}
