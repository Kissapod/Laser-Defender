using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    public GameObject[] enemyFormations;
    public GameObject winLable;
    public AudioClip winSound;
    public float appearanceChanceMin = 1f, appearanceChanceMax = 1f;
    public float formationDelay = 2f;
    public float minusChance = 0.05f;
    public AudioClip arriveSound;
    public int lastWave = 5;
    public float timeWin = 5f;
    public int enemySum = 8000;
    [SerializeField]
    private int enemyLimit = 50;

    private int enemyDeadCounter;
    private int waveSale = 0;
    // Start is called before the first frame update
    void Start()
    {
        EnemyFormationOn();
    }
    public void EnemyFormationOn()
    {
        if (lastWave <= 0)
        {
            Vector3 position = new Vector3(0, 0, 0);
            Instantiate(winLable, position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
            AudioSource.PlayClipAtPoint(winSound, transform.position);
            Invoke(nameof(LoadWinScreen), timeWin);
        } else
        {
            if (waveSale > 0)
            {
                waveSale -= 1;
                Invoke(nameof(NewFormation), formationDelay);
                Debug.Log(waveSale);
            } else
            {
                if (appearanceChanceMin > 0.2f)
                {
                    if (appearanceChanceMin > 0.8f)
                    {
                        appearanceChanceMin -= minusChance;
                        Invoke(nameof(NewFormation), formationDelay);
                    }
                    else
                    {
                        appearanceChanceMin -= minusChance;
                        appearanceChanceMax -= minusChance;
                        Invoke(nameof(NewFormation), formationDelay);
                    }
                }
                else
                {
                    lastWave -= 1;
                    Invoke(nameof(NewFormation), formationDelay);
                    Debug.Log("Осталось " + lastWave + " волн");
                }
            }
        }
    }

    void NewFormation()
    {
        Instantiate(enemyFormations[Random.Range(0, enemyFormations.Length)], transform.position, Quaternion.identity, transform);
    }

    void LoadWinScreen() //game over
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("Win Screen 1");
    }

    public void EnemyCounter()
    {
        enemySum -= 1;
        enemyDeadCounter += 1;
        if (enemyDeadCounter > enemyLimit)
        {
            if (appearanceChanceMin > 0.2f)
            {
                if (appearanceChanceMin > 0.8f)
                {
                    appearanceChanceMin -= minusChance;
                }
                else
                {
                    appearanceChanceMin -= minusChance;
                    appearanceChanceMax -= minusChance;
                }
            }
            enemyDeadCounter = 0;
            waveSale += 1;
            Debug.Log(waveSale);
        }
    }
}
