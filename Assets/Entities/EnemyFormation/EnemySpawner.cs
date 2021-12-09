using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;
    public float spawnDelay = 0.5f;

    private float appearanceChanceMin, appearanceChanceMax;
    private AudioClip arriveSound;
    private bool movingRight = true;
    private float xmin, xmax;
    // Start is called before the first frame update
    void Start()
    {
        // создаем переменные xmin и xmax для опеределения краев экрана
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmin = leftBoundary.x;
        xmax = rightBoundary.x;
        arriveSound = FindObjectOfType<EnemyFormation>().arriveSound;
        SpawnUntilFull();
    }

    public void OnDrawGizmos()
    {
        //создаем визуальное отображение границ группы врагов в виде прозрачного куба
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update()
    {
        // создаем условие, при котором враги движутся либо вправо, либо влево
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        // создаем две переменных, в которых хванятся данные о левом и правом крае группы врагов
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        // создаем условие, в котором группа врагов меняет направление движения если касается одного из краев экрана
        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        } else if (rightEdgeOfFormation > xmax) {
            movingRight = false;
        }
        //если вся группа уничнотежа, то создаем новую
        if (AllMembersDead())
        {
            FindObjectOfType<EnemyFormation>().EnemyFormationOn();
            Destroy(gameObject);
        }
    }
    // Функция возвращающая позицию противника если она пустует
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    Transform NextOverlordPosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0 && childPositionGameObject.CompareTag("OverlordPosition"))
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    void OverlordPositionCreate(GameObject enemyPrefab)
    {
        Transform freePosition = NextOverlordPosition();
        if (freePosition)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, freePosition);
            AudioSource.PlayClipAtPoint(arriveSound, freePosition.position);
        }
    }

    public void SpawnUntilFull()
    {
        //создаем префаб врага на каждом элементе position с определенным промежутком
        Transform freePosition = NextFreePosition();
        appearanceChanceMin = FindObjectOfType<EnemyFormation>().appearanceChanceMin;
        appearanceChanceMax = FindObjectOfType<EnemyFormation>().appearanceChanceMax;
        float appearanceChance = Random.Range(appearanceChanceMin, appearanceChanceMax);
        if (freePosition) // если следующая позиция свободна, то
        {
            foreach (GameObject enemyPrefab in enemyPrefabs) // для каждого префаба из массива 
            {
                float appearanceRate = enemyPrefab.GetComponent<EnemyBehaviour>().appearanceRate;
                if (appearanceRate >= appearanceChance && !enemyPrefab.GetComponent<OverLord>())
                {
                    Debug.Log("Враг");
                    Instantiate(enemyPrefab, transform.position, Quaternion.identity, freePosition);
                    AudioSource.PlayClipAtPoint(arriveSound, freePosition.position);
                    break;
                } else if (appearanceRate >= appearanceChance && enemyPrefab.GetComponent<OverLord>())
                {
                    OverlordPositionCreate(enemyPrefab);
                    break;
                }
            }
        }
        if (NextFreePosition()) // если следующая позиция свободна, то повторяем метод
        {
        Invoke("SpawnUntilFull", spawnDelay); 
        }
    }
    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return (false);
            }
        }
        return (true);
    }
}
