using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBonus : MonoBehaviour
{
    public GameObject powerUp;
    public GameObject scoreBonus;
    public AudioClip upSound;
    public AudioClip bonusSound;
    public float healthPower;
    public int points;

    private float health;
    private GameObject parent;
    private void Start()
    {
        parent = GameObject.Find("Game Canvas");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player && player.health != player.healthMax)
        {
            HealthUp(player);
            Destroy(gameObject);
        }
        else if (player && player.health == player.healthMax)
        {
            ScoreBonus();
            Destroy(gameObject);
        }
    }

    public void HealthUp(PlayerController player)
    {
        Text healthCount = GameObject.Find("HealthCounter").GetComponent<Text>();
        if (healthPower == 1)
        {
            health = Mathf.RoundToInt((player.healthMax / 100) * 30);
        }
        else if (healthPower == 2)
        {
            health = Mathf.RoundToInt((player.healthMax / 100) * 40);
        }
        else if (healthPower == 3)
        {
            health = Mathf.RoundToInt((player.healthMax / 100) * 50);
        }
        if (health > player.healthMax - player.health)
        {
            health = player.healthMax - player.health;
        }
        player.health += health;
        if (player.health >= player.healthMax)
        {
            player.health = player.healthMax;
        }
        healthCount.text = player.health.ToString();
        powerUp.GetComponentInChildren<Text>().text = health.ToString();
        Instantiate(powerUp, transform.position, Quaternion.identity, parent.transform);
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