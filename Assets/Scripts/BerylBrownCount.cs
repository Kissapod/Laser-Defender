using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerylBrownCount : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        GetComponent<Slider>().maxValue = player.berylBrownMax;
        GetComponent<Slider>().value = player.berylBrown;
    }

    void Update()
    {
        float playerBeryl = FindObjectOfType<PlayerController>().berylBrown;
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
