using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damage = 100f;
    public GameObject laserDestroy;
    public Color laserColor;
    public AudioClip laserDestroySound;

    public float GetDamage()
    {
        return damage;
    }

    [System.Obsolete]
    public void Hit()
    {
        laserDestroy.GetComponent<ParticleSystem>().startColor = laserColor;
        Instantiate(laserDestroy, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(laserDestroySound, transform.position);
        Destroy(gameObject);
    }
}
