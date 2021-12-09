using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTimer : MonoBehaviour
{
    public float time = 10;
    public AudioClip speedOffSound;

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
            PlayerController player = FindObjectOfType<PlayerController>();
            GameObject shapowPlayer = GameObject.Find("ShadowPlayer(Clone)");
            player.speed /= 2;
            player.ShadowPlayerOff(shapowPlayer);
            MagnetTimer magnetTimer = FindObjectOfType<MagnetTimer>();
            FireSpeedTimer fireSpeedTimer = FindObjectOfType<FireSpeedTimer>();
            if (magnetTimer || fireSpeedTimer)
            {
                if (magnetTimer)
                {
                    magnetTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
                }
                if (fireSpeedTimer)
                {
                    fireSpeedTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
                }
            }
            if (magnetTimer && fireSpeedTimer)
            {
                magnetTimer.transform.position = new Vector3(-5.3f, -2.25f, 0);
                fireSpeedTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
            }
            AudioSource.PlayClipAtPoint(speedOffSound, transform.position);
            Destroy(gameObject);
        }
    }
}
