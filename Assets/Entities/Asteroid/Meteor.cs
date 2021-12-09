using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Meteor : MonoBehaviour
{
    public float meteorBigHP;
    public float meteorBigRate;
    public float meteorBigSpeed;
    public float meteorBigRotaton;
    public AudioClip arriveBigSound;
    public GameObject meteorBigDestroy;
    public Sprite[] meteorBig;
    public float meteorMediumHP;
    public float meteorMediumRate;
    public float meteorMediumSpeed;
    public float meteorMediumRotaton;
    public AudioClip arriveMediumSound;
    public GameObject meteorMediumDestroy;
    public Sprite[] meteorMedium;
    public float meteorSmallHP;
    public float meteorSmallRate;
    public float meteorSmallSpeed;
    public float meteorSmallRotaton;
    public AudioClip arriveSmallSound;
    public GameObject meteorSmallDestroy;
    public Sprite[] meteorSmall;
    public float meteorTinyHP;
    public float meteorTinyRate;
    public float meteorTinySpeed;
    public float meteorTinyRotaton;
    public AudioClip arriveTinySound;
    public GameObject meteorTinyDestroy;
    public Sprite[] meteorTiny;
    public GameObject dropBonus;
    public AudioClip missileSound;
    public AudioClip collisionSound;
    public float health;
    public float meteorHP;
    public Color meteorColor;
    public GameObject megaExplosion;
    public GameObject damageInfo;

    private float meteorRotate = 45f;
    //private int missleCount;
    // Start is called before the first frame update
    void Start()
    {
        float size = Random.Range(0f, 1f);
        if (meteorBigRate >= size)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorBig[Random.Range(0, meteorBig.Length)];
            Invoke("ColliderOn", 1f);
            health = meteorBigHP;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, meteorBigSpeed, 0);
            meteorRotate = meteorBigRotaton;
            AudioSource.PlayClipAtPoint(arriveBigSound, transform.position);
        }
        else if (meteorMediumRate >= size)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorMedium[Random.Range(0, meteorMedium.Length)];
            Invoke(nameof(ColliderOn), 0.5f);
            health = meteorMediumHP;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, meteorMediumSpeed, 0);
            meteorRotate = meteorMediumRotaton;
            AudioSource.PlayClipAtPoint(arriveMediumSound, transform.position);
        }
        else if (meteorSmallRate >= size)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorSmall[Random.Range(0, meteorSmall.Length)];
            Invoke(nameof(ColliderOn), 0.25f);
            health = meteorSmallHP;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, meteorSmallSpeed, 0);
            meteorRotate = meteorSmallRotaton;
            AudioSource.PlayClipAtPoint(arriveSmallSound, transform.position);
        }
        else if (meteorTinyRate >= size)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorTiny[Random.Range(0, meteorTiny.Length)];
            Invoke(nameof(ColliderOn), 0.1f);
            health = meteorTinyHP;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, meteorTinySpeed, 0);
            meteorRotate = meteorTinyRotaton;
            AudioSource.PlayClipAtPoint(arriveTinySound, transform.position);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, meteorRotate) * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser missile = collision.gameObject.GetComponent<Laser>();
        LaserTarget missile2 = collision.gameObject.GetComponent<LaserTarget>();
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
        Mover superShot = collision.gameObject.GetComponent<Mover>();
        MegaExplosion megaexp = collision.gameObject.GetComponent<MegaExplosion>();
        meteorHP = health;
        if (missile)
        {
            float damage = missile.GetDamage();
            DamageInfo(damage);
            DropState(damage); //вычисляем сколько осколков должно выпасть и наносим урон
            missile.Hit();
            AudioSource.PlayClipAtPoint(missileSound, transform.position);
            MeteorStateLaserHit();
        }
        if (missile2)
        {
            float damage = missile2.GetDamage();
            DamageInfo(damage);
            DropState(damage); //вычисляем сколько осколков должно выпасть и наносим урон
            missile2.Hit();
            AudioSource.PlayClipAtPoint(missileSound, transform.position);
            MeteorStateLaserHit();
        }
        if (player)
        {
            Shield shieldPlayer = player.GetComponentInChildren<Shield>();
            float damage = player.health + shieldPlayer.shieldPow;
            DamageInfo(damage);
            MeteorDestroyState();
            DropState(damage);
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            MeteorState();
        }
        if (enemy)
        {
            ShieldEnemy shieldEnemy = enemy.GetComponentInChildren<ShieldEnemy>();
            float damage = enemy.health + shieldEnemy.shieldPow;
            DamageInfo(damage);
            MeteorDestroyState();
            DropState(damage);
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            MeteorState();
        }
        if (superShot)
        {
            Destroy(collision.gameObject);
            GetComponent<PolygonCollider2D>().enabled = false;
            float damage = 10000f;
            DropState(damage);
            Instantiate(megaExplosion, transform.position, Quaternion.identity);
            Die();
        }
        if (megaexp)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            float damage = 10000f;
            DropState(damage);
            Instantiate(megaExplosion, transform.position, Quaternion.identity);
            Die();
        }
    }

    private void DamageInfo(float damage)
    {
        if (meteorHP >= damage)
        {
            damageInfo.GetComponentInChildren<Text>().text = "-" + damage;
            Instantiate(damageInfo, transform.position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
        }
        else
        {
            damageInfo.GetComponentInChildren<Text>().text = "-" + meteorHP;
            Instantiate(damageInfo, transform.position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
        }
    }

    public void DropState(float damage)
    {
        Transform bonusPool = GameObject.Find("BonusPool").transform;
        int damageDrop = Mathf.RoundToInt(damage / 100);
        int meteorDrop = Mathf.RoundToInt(health / 100);
        if (damageDrop >= meteorDrop)
        {
            for (int i = 0; i < meteorDrop; i++)
            {
                Instantiate(dropBonus, transform.position, Quaternion.identity, bonusPool);
            }
        }
        else
        {
            for (int i = 0; i < damageDrop; i++)
            {
                Instantiate(dropBonus, transform.position, Quaternion.identity, bonusPool);
            }
        }
        health -= damage;
    }

    private void MeteorStateLaserHit()
    {
        if (health <= 0)
        {
            Die();
        }
        else if (health <= meteorTinyHP)
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorTiny[Random.Range(0, meteorTiny.Length)];
            ColliderOn();
        }
        else if (health <= meteorSmallHP)
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorSmall[Random.Range(0, meteorSmall.Length)];
            ColliderOn();
        }
        else if (health <= meteorMediumHP)
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorMedium[Random.Range(0, meteorMedium.Length)];
            ColliderOn();
        }
    }

    public void MeteorState()
    {
        if (health <= 0)
        {
            Die();
        }
        else if (health <= meteorTinyHP)
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorTiny[Random.Range(0, meteorTiny.Length)];
            Invoke("ColliderOn", 0.3f);
        }
        else if (health <= meteorSmallHP)
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorSmall[Random.Range(0, meteorSmall.Length)];
            Invoke("ColliderOn", 0.1f);
        }
        else if (health <= meteorMediumHP)
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            gameObject.GetComponent<SpriteRenderer>().sprite = meteorMedium[Random.Range(0, meteorMedium.Length)];
            Invoke("ColliderOn", 0.05f);
        }
    }
    public void MeteorDestroyState()
    {
        if (health > meteorMediumHP)
        {
            meteorBigDestroy.GetComponent<ParticleSystem>().startColor = meteorColor;
            Instantiate(meteorBigDestroy, transform.position, Quaternion.identity);
        }
        else if (health > meteorSmallHP)
        {
            meteorMediumDestroy.GetComponent<ParticleSystem>().startColor = meteorColor;
            Instantiate(meteorMediumDestroy, transform.position, Quaternion.identity);
        }
        else if (health > meteorTinyHP)
        {
            meteorSmallDestroy.GetComponent<ParticleSystem>().startColor = meteorColor;
            Instantiate(meteorSmallDestroy, transform.position, Quaternion.identity);
        }
        else if (health <= meteorTinyHP)
        {
            meteorTinyDestroy.GetComponent<ParticleSystem>().startColor = meteorColor;
            Instantiate(meteorTinyDestroy, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    void ColliderOn()
    {
        PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();
        pc.isTrigger = true;
    }
}