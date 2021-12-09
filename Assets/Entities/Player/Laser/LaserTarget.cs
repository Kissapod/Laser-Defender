using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    public float damage;
    public GameObject laserDestroy;
    public Color laserColor;
    public AudioClip laserDestroySound;
    public float speedMove;

    private Transform target;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.position += (dir.normalized * Time.deltaTime * speedMove);
            transform.up = dir.normalized;
        } else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Target");
            if (enemies.Length != 0)
            {
                Transform nearestEnemy = null;
                float nearestEnemyDistance = Mathf.Infinity;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Target"))
                {
                    float currDistance = Vector2.Distance(transform.position, enemy.transform.position);
                    if (currDistance < nearestEnemyDistance)
                    {
                        nearestEnemy = enemy.transform;
                        nearestEnemyDistance = currDistance;
                    }
                }
                SetTarget(nearestEnemy);
            } else 
            {
                Debug.Log("Целей нет");
                Hit();
            }
        }
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }
    public float GetDamage()
    {
        return damage;
    }


    public void Hit()
    {
        laserDestroy.GetComponent<ParticleSystem>().startColor = laserColor;
        Instantiate(laserDestroy, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(laserDestroySound, transform.position);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LaserTarget laserTarget = collision.gameObject.GetComponent<LaserTarget>();
        if (laserTarget)
        {
            Destroy(gameObject);
        }
    }
}
