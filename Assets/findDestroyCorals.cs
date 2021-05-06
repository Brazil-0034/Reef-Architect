using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class findDestroyCorals : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Destroy(gameObject, 0.1f);
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag.Equals("Coral"))
        {
            Destroy(col.gameObject);
            GameObject.Find("Cursor").SendMessage("removeFromUsed", col.gameObject.transform.position);
            CursorScript.coralCount--;
            if (CursorScript.coralCount == 0)
            {
                GameObject.Find("SeabedHelpText").GetComponent<TextMeshPro>().enabled = true;
            }
            Destroy(gameObject);
        }
    }
}
