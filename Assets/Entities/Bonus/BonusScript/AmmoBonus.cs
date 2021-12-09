using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBonus : MonoBehaviour
{
    public GameObject ammoUp;
    public GameObject scoreBonus;
    public AudioClip upSound;
    public AudioClip bonusSound;
    public float ammoPow;
    public int points;

    private float ammoOver;
    private GameObject parent;

    private void Start()
    {
        parent = GameObject.Find("Game Canvas");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Slider ammo = GameObject.Find("Ammo").GetComponent<Slider>();
        if (player && ammo.maxValue != ammo.value)
        {
            AmmoUp(ammo, player);
            Destroy(gameObject);
        } else if (player && ammo.maxValue == ammo.value)
        {  
            ScoreBonus();
            Destroy(gameObject);
        }
    }

    void AmmoUp(Slider ammo, PlayerController player)
    {
        Text ammoCount = ammo.GetComponentInChildren<Text>();
        ammoOver = ammoPow;
        if (ammoPow > ammo.maxValue - player.ammoCounter)
        {
                ammoOver = ammo.maxValue - player.ammoCounter;
        }
        player.ammoCounter += ammoOver;
        ammoCount.text = player.ammoCounter.ToString();
        ammoUp.GetComponentInChildren<Text>().text = ammoOver.ToString();
        Instantiate(ammoUp, transform.position, Quaternion.identity, parent.transform);
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
