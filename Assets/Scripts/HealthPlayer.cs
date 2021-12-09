using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    //private float maxHP;
    private PlayerController playerHP;
    // Start is called before the first frame update
    void Start()
    {
        playerHP = FindObjectOfType<PlayerController>();
        GetComponent<Slider>().maxValue = playerHP.healthMax;
        GetComponent<Slider>().value = playerHP.health;
    }

    // Update is called once per frame
    void Update()
    {
        float playerHPValue = FindObjectOfType<PlayerController>().health;
        if (playerHPValue < GetComponent<Slider>().value)
        {
            GetComponent<Slider>().value -= playerHP.healthMax / 500;
            if (GetComponent<Slider>().value < playerHPValue)
            {
                GetComponent<Slider>().value = playerHPValue;
            }
        } else if (playerHPValue > GetComponent<Slider>().value)
        {
            GetComponent<Slider>().value += playerHP.healthMax / 500;
            if (GetComponent<Slider>().value > playerHPValue)
            {
                GetComponent<Slider>().value = playerHPValue;
            }
        }
    }
}
