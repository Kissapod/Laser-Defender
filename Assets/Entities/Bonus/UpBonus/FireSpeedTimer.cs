using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSpeedTimer : MonoBehaviour
{
    public float time = 10;
    public AudioClip fireSpeedOffSound;

    void Start()
    {
        InvokeRepeating(nameof(TimerON), 1f, 0.01f);
    }


    void TimerON()
    {
        time -= 0.01f;
        GetComponentInChildren<Text>().text = Mathf.RoundToInt(time).ToString();
        if (time <= 0)
        {
            FindObjectOfType<PlayerController>().firingRate *= 2;
            FindObjectOfType<PlayerController>().FireSpeedReset();
            MagnetTimer magnetTimer = FindObjectOfType<MagnetTimer>();
            SpeedTimer speedTimer = FindObjectOfType<SpeedTimer>();
            if (magnetTimer || speedTimer)
            {
                if (magnetTimer)
                {
                    magnetTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
                }
                if (speedTimer)
                {
                    speedTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
                }
            }
            if (magnetTimer && speedTimer)
            {
                magnetTimer.transform.position = new Vector3(-5.3f, -2.25f, 0);
                speedTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
            }
            AudioSource.PlayClipAtPoint(fireSpeedOffSound, transform.position);
            Destroy(gameObject);
        }
    }
}
