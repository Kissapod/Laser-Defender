using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static int score = 0;
    public AudioClip dingSound;
    private Text myText;
    // Start is called before the first frame update

    void Start()
    {
        myText = GetComponent<Text>();
        Reset();
    }
    public void Score(int points)
    {
        score += points;
        /*if (score % 1500 == 0)
        {
            AudioSource.PlayClipAtPoint(dingSound, transform.position);
        }*/
        myText.text = score.ToString();

    }
    public static void Reset()
    {
        score = 0;
    }
}
