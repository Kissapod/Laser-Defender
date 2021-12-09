using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaExplosion : MonoBehaviour
{
    public AudioClip explosiobSound;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(explosiobSound, transform.position);
        Destroy(gameObject, 0.7f);
    }
}
