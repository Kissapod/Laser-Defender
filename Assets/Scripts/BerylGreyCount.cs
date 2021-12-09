using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerylGreyCount : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        GetComponent<Slider>().maxValue = player.berylGreyMax;
        GetComponent<Slider>().value = player.berylGrey;
    }

    void Update()
    {
        float playerBeryl = FindObjectOfType<PlayerController>().berylGrey;
        if (playerBeryl < GetComponent<Slider>().value)
        {
            GetComponent<Slider>().value -= 1;
        }
        else if (playerBeryl > GetComponent<Slider>().value)
        {
            GetComponent<Slider>().value += 1f;
        }
    }
}
