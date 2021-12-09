using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetTimer : MonoBehaviour
{
    public float time = 10;
    public AudioClip magnetOff;

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
            GameObject[] bonuses = GameObject.FindGameObjectsWithTag("Bonus");
            FindObjectOfType<PlayerController>().magnetState = false;
            foreach (GameObject bonus in bonuses)
            {
                bonus.GetComponent<Rigidbody2D>().gravityScale = bonus.GetComponent<MoverBonus>().newGravity;
            }
            FindObjectOfType<PlayerController>().MagnetFieldOff();
            AudioSource.PlayClipAtPoint(magnetOff, transform.position);
            SpeedTimer speedTimer = FindObjectOfType<SpeedTimer>();
            FireSpeedTimer fireSpeedTimer = FindObjectOfType<FireSpeedTimer>();
            if (speedTimer || fireSpeedTimer)
            {
                if (speedTimer)
                {
                    speedTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
                }
                if (fireSpeedTimer)
                {
                    fireSpeedTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
                }
            }
            if (speedTimer && fireSpeedTimer)
            {
                speedTimer.transform.position = new Vector3(-5.3f, -2.25f, 0);
                fireSpeedTimer.transform.position = new Vector3(-5.3f, -2.75f, 0);
            }
            Destroy(gameObject);
        }
    }
}
