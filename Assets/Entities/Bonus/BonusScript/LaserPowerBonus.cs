using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserPowerBonus : MonoBehaviour
{
    public GameObject laserPowerUp;
    public GameObject scoreBonus;
    public AudioClip upSound;
    public AudioClip bonusSound;
    public float laserPow;
    public int points;

    private GameObject parent;
    private void Start()
    {
        parent = GameObject.Find("Game Canvas");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            if (player.laserLvlCount != 95)
            {
                LaserPowerUp(player);
                Destroy(gameObject);
            }
            else
            {
                ScoreBonus();
                Destroy(gameObject);
            }
        }

    }
    public void LaserPowerUp(PlayerController player)
    {
        Slider laserlvl = GameObject.Find("LaserLVL").GetComponent<Slider>();
        Text laserlvlCounter = GameObject.Find("LaserLvlCounter").GetComponent<Text>();
        player.laserPower += laserPow;
        player.laserLvlCount += 1;
        laserlvlCounter.text = "lvl " + player.laserLvlCount.ToString();
        laserlvl.value = player.laserLvlCount;
        AudioSource.PlayClipAtPoint(upSound, transform.position);
        Instantiate(laserPowerUp, player.transform.position, Quaternion.identity);
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
