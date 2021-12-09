using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverLord : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float startSpawn = 2f;
    public float spawnDelay = 0.5f;
    public AudioClip arriveSound;

    private Position[] positions;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnUntilFull), startSpawn, spawnDelay);
        positions = GetComponentsInChildren<Position>();    
    }

    public void SpawnUntilFull()
    {
        //создаем префаб врага на каждом элементе position с определенным промежутком
        Transform freePosition = NextFreePosition();
        float appearanceChance = Random.Range(0, 1f);
        if (freePosition) // если следующая позиция свободна, то
        {
            foreach (GameObject enemyPrefab in enemyPrefabs)
            {
                float appearanceRate = enemyPrefab.GetComponent<EnemyBehaviour>().appearanceRate;
                if (appearanceRate >= appearanceChance)
                {
                    Instantiate(enemyPrefab, transform.position, Quaternion.identity, freePosition);
                    AudioSource.PlayClipAtPoint(arriveSound, freePosition.position);
                    break;
                }
            }
        }
    }

    Transform NextFreePosition()
    {
        foreach (Position childPositionGameObject in positions)
        {
            if (childPositionGameObject.transform.childCount == 0)
            {
                return childPositionGameObject.transform;
            }
        }
        return null;
    }
}
