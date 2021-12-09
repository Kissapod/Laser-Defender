using UnityEngine;

public class Miner : MonoBehaviour
{
    public GameObject mineGun;
    public GameObject minePrefab;
    public float firingStartMin = 1.5f;
    public float firingStartMax = 10f;
    public float firingDelay = 0.2f;
    public int firingCounterAll = 10;
    public int firingCounterMin = 10;
    public int firingCounterMax = 10;
    public AudioClip mineSound;

    private GameObject laserPool;
    private int k = 0;
    // Start is called before the first frame update
    void Start()
    {
        laserPool = GameObject.Find("LaserPool");
        Invoke("Mine", Random.Range(firingStartMin, firingStartMax));
    }
    public void Mine()
    {
        if (k < firingCounterAll)
        {
            Vector3 minePos = mineGun.transform.position;
            Instantiate(minePrefab, minePos, transform.rotation, laserPool.transform);
            AudioSource.PlayClipAtPoint(mineSound, transform.position);
            k += 1;
            Invoke("Mine", firingDelay);
        }
        else
        {
            k = 0;
            firingCounterAll = Random.Range(firingCounterMin, firingCounterMax);
            Invoke("Mine", Random.Range(firingStartMin, firingStartMax));
        }
    }
}
