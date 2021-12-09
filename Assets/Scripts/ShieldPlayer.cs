using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPlayer : MonoBehaviour
{
    //private float maxHP;
    public GameObject shieldObject;
    private Shield playerShield;
    // Start is called before the first frame update
    void Start()
    {
        playerShield = shieldObject.GetComponentInChildren<Shield>();
        GetComponent<Slider>().maxValue = playerShield.shieldPowerMax;
        GetComponent<Slider>().value = playerShield.shieldPow;
    }

    // Update is called once per frame
    void Update()
    {
        float playerShieldValue = shieldObject.GetComponentInChildren<Shield>().shieldPow;
        if (playerShieldValue < GetComponent<Slider>().value)
        {
            GetComponent<Slider>().value -= playerShield.shieldPowerMax / 500;
            if (GetComponent<Slider>().value < playerShieldValue)
            {
                GetComponent<Slider>().value = playerShieldValue;
            }
        }
        else if (playerShieldValue > GetComponent<Slider>().value)
        {
            GetComponent<Slider>().value += playerShield.shieldPowerMax / 500;
            if (GetComponent<Slider>().value > playerShieldValue)
            {
                GetComponent<Slider>().value = playerShieldValue;
            }
        }
    }
}
