using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerylBonusGrey : MonoBehaviour
{
    public GameObject berylUp;
    public GameObject scoreBonus;
    public AudioClip upSound;
    public AudioClip bonusSound;
    public float berylAmount;
    public int points;

    private float berylOver;
    private GameObject parent;
    private void Start()
    {
        parent = GameObject.Find("Game Canvas");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Slider berylGrey = FindObjectOfType<BerylGreyCount>().GetComponent<Slider>();
        if (player && berylGrey.maxValue != berylGrey.value)
        {
            BerylUp(berylGrey, player);
            Destroy(gameObject);
        }
        else if (player && berylGrey.maxValue == berylGrey.value)
        {
            ScoreBonus();
            Destroy(gameObject);
        }
    }
    public void BerylUp(Slider beryl, PlayerController player)
    {
        berylOver = berylAmount;
        if (berylAmount > beryl.maxValue - beryl.value)
        {
                berylOver = beryl.maxValue - beryl.value;
        }
        player.berylGrey += berylOver;
        Text berylGreyCount = GameObject.Find("BerylCounterG").GetComponent<Text>();
        berylGreyCount.text = player.berylGrey.ToString();
        beryl.value = player.berylGrey;
        berylUp.GetComponentInChildren<Text>().text = berylOver.ToString();
        Instantiate(berylUp, transform.position, Quaternion.identity, parent.transform);
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
