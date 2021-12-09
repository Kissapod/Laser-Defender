using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public float laserDelay;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", laserDelay);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
