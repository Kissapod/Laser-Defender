using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        GetComponent<Slider>().value = player.ammoCounter;
    }

    void Update()
    {
        float playerAmmo = player.ammoCounter;
        if (playerAmmo < GetComponent<Slider>().value)
        {
            GetComponent<Slider>().value -= 1;
        }
        else if (playerAmmo > GetComponent<Slider>().value)
        {
            GetComponent<Slider>().value += 1f;
        }
    }
}
