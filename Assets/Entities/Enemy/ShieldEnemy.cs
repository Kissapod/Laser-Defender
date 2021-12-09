using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldEnemy : MonoBehaviour
{
    public float shieldPow;
    public float shieldPowerMax = 500f;
    public Sprite shield1;
    public Sprite shield2;
    public Sprite shield3;
    public float regenerationValue = 1f;
    public float regenerationSpeed = 1f;

    private SpriteRenderer shieldSprite;
    private float shieldPowerPiece;

    // Start is called before the first frame update
    void Start()
    {
        shieldSprite = GetComponent<SpriteRenderer>();
        shieldSprite.enabled = false;
        shieldPow = shieldPowerMax;
        ShieldState();
        InvokeRepeating("RegenerationShield", 0.000001f, 1/regenerationSpeed);
    }

    void RegenerationShield()
    {
        shieldPow += regenerationValue;
            if (shieldPow >= shieldPowerMax)
            {
                shieldPow = shieldPowerMax;
            }
            ShieldState();
    }
    public void ShieldState()
    {
        shieldPowerPiece = shieldPowerMax / 3;
        if (shieldPow > 0 && shieldPow <= shieldPowerPiece)
        {
            shieldSprite.enabled = true;
            shieldSprite.sprite = shield1;
        }
        else if (shieldPow > shieldPowerPiece && shieldPow <= shieldPowerPiece*2)
        {
            shieldSprite.enabled = true;
            shieldSprite.sprite = shield2;
        }
        else if (shieldPow > shieldPowerPiece*2 && shieldPow <= shieldPowerPiece * 3+1)
        {
            shieldSprite.enabled = true;
            shieldSprite.sprite = shield3;
        }
        else if (shieldPow == 0)
        {
            shieldSprite.enabled = false;
            shieldSprite.sprite = null;
        }
    }
}
