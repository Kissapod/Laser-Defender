using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerylBonusBrown : MonoBehaviour
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
        Slider berylBrown = FindObjectOfType<BerylBrownCount>().GetComponent<Slider>();
        if (player && berylBrown.maxValue != berylBrown.value)
        {
            BerylUp(berylBrown, player);
            Destroy(gameObject);
        }
        else if (player && berylBrown.maxValue == berylBrown.value)
        {
            ScoreBonus();
            Destroy(gameObject);
        }
    }

    void BerylUp(Slider beryl, PlayerController player)
    {
        berylOver = berylAmount;
        if (berylAmount > beryl.maxValue - beryl.value)
        {
            berylOver = beryl.maxValue - beryl.value;
        }
        player.berylBrown += berylOver;
        Text berylBrownCount = GameObject.Find("BerylCounterB").GetComponent<Text>();
        berylBrownCount.text = player.berylBrown.ToString();
        beryl.value = player.berylBrown;
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
