using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeCoralScript : MonoBehaviour
{
    public GameObject coral;
    [TextArea(15,20)]
    public string description;
    // Start is called before the first frame update
    void Click()
    {
        GameObject.Find("Cursor").SendMessage("SetCoral", coral);
        GameObject.Find("highlight").transform.position = transform.position;
        GameObject.Find("Header").GetComponent<TextMeshPro>().text = gameObject.name;
        GameObject.Find("Description").GetComponent<TextMeshPro>().text = description;
    }
}
