using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBonus : MonoBehaviour
{
    public GameObject timer;
    public GameObject shadowPlayer;
    public AudioClip magnetOnSound;
    public AudioClip speedOnSound;
    public AudioClip fireSpeedOnSound;
    public AudioClip bonusSound;

    private Vector3 positionTimer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            MagnetTimer magnetTimer = FindObjectOfType<MagnetTimer>();
            SpeedTimer speedTimer = FindObjectOfType<SpeedTimer>();
            FireSpeedTimer fireSpeedTimer = FindObjectOfType<FireSpeedTimer>();
            if (magnetTimer && timer.GetComponent<MagnetTimer>())
            {
                magnetTimer.time += 10;
                AudioSource.PlayClipAtPoint(bonusSound, transform.position);
            } else if (timer.GetComponent<MagnetTimer>())
            {
                player.GetComponent<PlayerController>().magnetState = true;
                player.GetComponent<PlayerController>().MagnetFieldOn();
                if (!speedTimer && !fireSpeedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -2.75f, 0);
                }
                if (speedTimer || fireSpeedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -2.25f, 0);
                }
                if (speedTimer && fireSpeedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -1.75f, 0);
                }
                Instantiate(timer, positionTimer, Quaternion.identity, GameObject.Find("Game Canvas").transform);
                AudioSource.PlayClipAtPoint(magnetOnSound, transform.position);
            }
            if (speedTimer && timer.GetComponent<SpeedTimer>())
            {
                speedTimer.time += 10;
                AudioSource.PlayClipAtPoint(bonusSound, transform.position);
            }
            else if (timer.GetComponent<SpeedTimer>())
            {
                player.speed *= 2;
                if (!magnetTimer && !fireSpeedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -2.75f, 0);
                }
                if (magnetTimer || fireSpeedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -2.25f, 0);
                }
                if (magnetTimer && fireSpeedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -1.75f, 0);
                }
                Instantiate(timer, positionTimer, Quaternion.identity, GameObject.Find("Game Canvas").transform);
                Instantiate(shadowPlayer, player.transform.position, Quaternion.identity, player.transform);
                AudioSource.PlayClipAtPoint(speedOnSound, transform.position);
            }
            if (fireSpeedTimer && timer.GetComponent<FireSpeedTimer>())
            {
                fireSpeedTimer.time += 10;
                AudioSource.PlayClipAtPoint(bonusSound, transform.position);
            }
            else if (timer.GetComponent<FireSpeedTimer>())
            {
                player.firingRate /= 2;
                player.FireSpeedReset();
                if (!magnetTimer && !speedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -2.75f, 0);
                }
                if (magnetTimer || speedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -2.25f, 0);
                }
                if (magnetTimer && speedTimer)
                {
                    positionTimer = new Vector3(-5.3f, -1.75f, 0);
                }
                Instantiate(timer, positionTimer, Quaternion.identity, GameObject.Find("Game Canvas").transform);
                AudioSource.PlayClipAtPoint(fireSpeedOnSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
