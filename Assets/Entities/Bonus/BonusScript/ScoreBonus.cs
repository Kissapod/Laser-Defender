using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBonus : MonoBehaviour
{
    public GameObject scoreBonus;
    public AudioClip bonusSound;
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
            ScoreUp();
            Destroy(gameObject);
        }
    }
    void ScoreUp()
    {
        ScoreKeeper scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        scoreKeeper.Score(points);
        AudioSource.PlayClipAtPoint(bonusSound, transform.position);
        scoreBonus.GetComponentInChildren<Text>().text = points.ToString();
        Instantiate(scoreBonus, transform.position, Quaternion.identity, parent.transform);
    }
}
