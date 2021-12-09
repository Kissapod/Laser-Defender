using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldMaxBonus : MonoBehaviour
{
    public GameObject shieldMaxUp;
    public GameObject scoreBonus;
    public AudioClip upSound;
    public AudioClip bonusSound;
    public float shieldUp;
    public int points;

    private GameObject parent;
    private void Start()
    {
        parent = GameObject.Find("Game Canvas");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Shield shield = FindObjectOfType<Shield>().GetComponent<Shield>();
        if (player)
        {
            if (player.shieldLvlCount != 95)
            {
                ShieldMaxUp(player, shield);
                Destroy(gameObject);
            }
            else
            {
                ScoreBonus();
                Destroy(gameObject);
            }
        }

    }
    public void ShieldMaxUp(PlayerController player, Shield shield)
    {
        Slider shieldlvl = GameObject.Find("ShieldLVL").GetComponent<Slider>();
        Slider sliderShield = GameObject.Find("Shield Player").GetComponent<Slider>();
        Text shieldlvlCounter = GameObject.Find("ShieldLvlCounter").GetComponent<Text>();
        GameObject parent = GameObject.Find("Game Canvas");
        shield.shieldPowerMax += shieldUp;
        shield.regenerationSpeed += (shieldUp / 100) * 0.2f;
        shield.RegenerationUpdate();
        player.shieldLvlCount += 1;
        shieldlvlCounter.text = "lvl " + player.shieldLvlCount.ToString();
        shieldlvl.value = player.shieldLvlCount;
        sliderShield.maxValue = shield.shieldPowerMax;
        Instantiate(shieldMaxUp, transform.position, Quaternion.identity, parent.transform);
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
