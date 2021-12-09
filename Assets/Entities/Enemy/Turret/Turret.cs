using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float firingStartMin = 1.5f;
    public float firingStartMax = 5f;
    public float firingDelay = 0.2f;
    public int firingCounter = 10;
    public GameObject[] guns;

    private int k = 0;
    private GameObject laserPrefab;
    private AudioClip fireSound;
    private float laserSpeed;
    private GameObject laserPool;
    void Start()
    {
        laserPrefab = GetComponent<EnemyBehaviour>().laserPrefab;
        fireSound = GetComponent<EnemyBehaviour>().fireSound;
        laserSpeed = GetComponent<EnemyBehaviour>().laserSpeed;
        laserPool = GameObject.Find("LaserPool");
        Invoke (nameof(FireSerial), Random.Range(firingStartMin,firingStartMax));
    }

    // Update is called once per frame
    public void FireSerial()
    {
        if (k < firingCounter)
        {
            for (int i = 0; i < guns.Length; i++)
            {
                Vector3 firePos = guns[i].transform.position;
                GameObject laser = Instantiate(laserPrefab, firePos, Quaternion.identity, laserPool.transform);
                laser.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * laserSpeed, ForceMode2D.Impulse);
            }
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
            k += 1;
            Invoke(nameof(FireSerial), firingDelay);
        }
        else
        {
            k = 0;
            Invoke(nameof(FireSerial), Random.Range(firingStartMin, firingStartMax));

        }
    }
}
