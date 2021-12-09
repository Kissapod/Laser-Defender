using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBonus : MonoBehaviour
{
    public GameObject shieldUp;
    public GameObject scoreBonus;
    public AudioClip upSound;
    public AudioClip bonusSound;
    public float shieldPower;
    public int points;

    private float shieldPow;
    private GameObject parent;
    private void Start()
    {
        parent = GameObject.Find("Game Canvas");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Shield shield = FindObjectOfType<Shield>().GetComponent<Shield>();
        if (player && shield.shieldPow != shield.shieldPowerMax)
        {
            ShieldUp(shield);
            Destroy(gameObject);
        }
        else if (player && shield.shieldPow == shield.shieldPowerMax)
        {
            ScoreBonus();
            Destroy(gameObject);
        }
    }

    public void ShieldUp(Shield shield)
    {
        Text shieldCount = GameObject.Find("ShieldCounter").GetComponent<Text>();
        if (shieldPower == 1)
        {
            shieldPow = Mathf.RoundToInt((shield.shieldPowerMax / 100) * 30);
        }
        else if (shieldPower == 2)
        {
            shieldPow = Mathf.RoundToInt((shield.shieldPowerMax / 100) * 40);
        }
        else if (shieldPower == 3)
        {
            shieldPow = Mathf.RoundToInt((shield.shieldPowerMax / 100) * 50);
        }
        if (shieldPow > shield.shieldPowerMax - shield.shieldPow)
        {
            shieldPow = shield.shieldPowerMax - shield.shieldPow;
        }
        shield.shieldPow += shieldPow;
        shield.ShieldState();
        if (shield.shieldPow >= shield.shieldPowerMax)
        {
            shield.shieldPow = shield.shieldPowerMax;
            shield.ShieldState();
        }
        shieldCount.text = shield.shieldPow.ToString();
        shieldUp.GetComponentInChildren<Text>().text = shieldPow.ToString();
        Instantiate(shieldUp, transform.position, Quaternion.identity, parent.transform);
        AudioSource.PlayClipAtPoint(upSound, transform.position);
    }
    void ScoreBonus()
    {
        ScoreKeeper scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        scoreKeeper.Score(points);
        AudioSource.PlayClipAtPoint(bonusSound, transform.position);
        scoreBonus.GetComponentInChildren<Text>().text = points.ToString();
        Instantiate(scoreBonus, transform.position, Quaternion.identity, parent.transform);
    }
}
