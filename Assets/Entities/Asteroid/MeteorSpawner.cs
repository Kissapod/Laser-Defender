using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject[] meteorPrefabs;
    public float spawnDelay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnUntilFull();
    }

    Transform NextFreePosition()
    {
        Position[] childPosition = GetComponentsInChildren<Position>();
        Transform position = childPosition[Random.Range(0, childPosition.Length)].transform;
        if (position.childCount == 0)
            {
            return position;
            }
        return null;
    }
    void SpawnUntilFull()
    {
        //создаем префаб астероида на каждом элементе position с определенным промежутком
        Transform freePosition = NextFreePosition();
        if (freePosition) // если позиция свободна, то
        {
            GameObject meteorPrefab = meteorPrefabs[Random.Range(0, meteorPrefabs.Length)];
            Instantiate(meteorPrefab, freePosition.position, Quaternion.identity, freePosition);
        }
        Invoke("SpawnUntilFull", spawnDelay);
    }
}
