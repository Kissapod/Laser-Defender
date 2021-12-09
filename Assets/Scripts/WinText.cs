using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    
    private Text[] texts;
    // Start is called before the first frame update
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            Color temp = text.color;
            temp.a = 0;
            Debug.Log("Сброс альфы");
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Text text in texts)
        {
            Color temp = text.color;
            temp.a += 0.0001f;
        }
    }
}
