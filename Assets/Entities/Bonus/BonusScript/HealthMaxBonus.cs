using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMaxBonus : MonoBehaviour
{
    public GameObject healthMaxUp;
    public GameObject scoreBonus;
    public AudioClip upSound;
    public AudioClip bonusSound;
    public float healthUp;
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
            if (player.powerLvlCount != 95)
            {
                HealthMaxUp(player);
                Destroy(gameObject);
            }
            else
            {
                ScoreBonus();
                Destroy(gameObject);
            }
        }

    }
    public void HealthMaxUp(PlayerController player)
    {
        Slider powerlvl = GameObject.Find("PowerLVL").GetComponent<Slider>();
        Slider sliderHealth = GameObject.Find("Health Player").GetComponent<Slider>();
        Text powerlvlCounter = GameObject.Find("PowerLvlCounter").GetComponent<Text>();
        player.healthMax += healthUp;
        sliderHealth.maxValue = player.healthMax;
        player.powerLvlCount += 1;
        powerlvlCounter.text = "lvl " + player.powerLvlCount.ToString();
        powerlvl.value = player.powerLvlCount;
        Instantiate(healthMaxUp, player.transform.position, Quaternion.identity, parent.transform);
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
