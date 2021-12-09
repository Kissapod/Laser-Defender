using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldRegenBonus : MonoBehaviour
{
    public GameObject shieldRegenUp;
    public GameObject scoreBonus;
    public AudioClip upSound;
    public AudioClip bonusSound;
    public float regenPow;
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
            if (player.shieldRegLvlCount != 95)
            {
                ShieldRegenUp(player, shield);
                Destroy(gameObject);
            }
            else
            {
                ScoreBonus();
                Destroy(gameObject);
            }
        }
    }

    public void ShieldRegenUp(PlayerController player, Shield shield)
    {
        Slider shieldReglvl = GameObject.Find("ShieldRegLVL").GetComponent<Slider>();
        Text shieldReglvlCounter = GameObject.Find("ShieldRegLvlCounter").GetComponent<Text>();
        shield.regenerationValue += regenPow;
        player.shieldRegLvlCount += 1;
        shieldReglvlCounter.text = "lvl " + player.shieldRegLvlCount.ToString();
        shieldReglvl.value = player.shieldRegLvlCount;
        AudioSource.PlayClipAtPoint(upSound, transform.position);
        Instantiate(shieldRegenUp, player.transform.position, Quaternion.identity);
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
