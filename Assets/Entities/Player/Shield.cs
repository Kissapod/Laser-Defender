using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float shieldPow;
    public float shieldPowerMax = 500f;
    public AudioClip shieldDown;
    public Sprite shield1;
    public Sprite shield2;
    public Sprite shield3;
    public float regenerationValue = 1f;
    public float regenerationSpeed = 1f;
    public GameObject bodyObject;

    private SpriteRenderer shieldSprite;
    private float shieldPowerPiece;

    // Start is called before the first frame update
    void Start()
    {
        shieldSprite = GetComponent<SpriteRenderer>();
        shieldSprite.enabled = false;
        shieldPow = shieldPowerMax;
        ShieldState();
        RegenerationUpdate();
    }
     public void RegenerationShield()
     {
        if (bodyObject.GetComponent<SpriteRenderer>().enabled == true) { 
        shieldPow += Mathf.RoundToInt(regenerationValue);
            if (shieldPow > shieldPowerMax)
            {
                shieldPow = shieldPowerMax;
            }
            GameObject.Find("ShieldCounter").GetComponent<Text>().text = shieldPow.ToString();
            ShieldState();
        }
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

    public void RegenerationUpdate()
    {
        CancelInvoke("RegenerationShield");
        InvokeRepeating("RegenerationShield", 0.000001f, 1 / regenerationSpeed);
    }
}
