using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public float health = 200f;
    public GameObject laserPrefab;
    public float laserSpeed;
    public float shotPerSecont = 0.5f;
    public int scoreValue = 150;
    public float appearanceRate = 1f;
    public AudioClip fireSound;
    public AudioClip deathSound;
    public GameObject[] guns;

    public float dropChanceMin, dropChanceMax;
    public GameObject[] scoreBonus;
    public GameObject[] healthBonus;
    public GameObject[] shieldBonus;
    public GameObject[] ammoBonus;
    public GameObject[] maxBonus;
    public GameObject boom;
    public GameObject megaExplosion;
    public GameObject damageInfo;

    private Animator animator;
    private GameObject laserPool;
    private ShieldEnemy shield;

    private void Awake()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        laserPool = GameObject.Find("LaserPool");
        shield = GetComponentInChildren<ShieldEnemy>();
        InvokeRepeating("ColliderState", 1.25f, 0.01f);
    }
    private void ColliderState()
    {
        if (health <= 0)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        if (health > 0 && shield.shieldPow <= 0)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        if (health > 0 && shield.shieldPow > 0)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    void Update()
    {
        float probability = Time.deltaTime * shotPerSecont;
        if (!IsAnimationPlaying("arrival") && !GetComponent<Turret>())
        {
                if (Random.value < probability)
                {
                    Fire();
                }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Laser missile = collision.gameObject.GetComponent<Laser>();
        LaserTarget missile2 = collision.gameObject.GetComponent<LaserTarget>();
        Meteor meteor = collision.gameObject.GetComponent<Meteor>();
        Mover superShot = collision.gameObject.GetComponent<Mover>();
        MegaExplosion megaexp = collision.gameObject.GetComponent<MegaExplosion>();
        if (missile)
        {
            DamagePlayerLaser(missile);
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
        if (missile2)
        {
            DamagePlayerLaser2(missile2);
            missile2.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
        if (meteor)
        {
            DamageMeteor(meteor);
            if (health <= 0)
            {
                Die();
            }
        }
        if (superShot)
        {
            health = 0;
            Destroy(collision.gameObject);
            Die2();
        }
        if (megaexp)
        {
            health = 0;
            Die2();
        }
    }

    private void DamagePlayerLaser(Laser missile) //нанесение урона игроку
    {
        if (health + shield.shieldPow >= missile.GetDamage())
        {
            damageInfo.GetComponentInChildren<Text>().text = "-" + missile.GetDamage();
            Instantiate(damageInfo, transform.position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
        }
        else
        {
            damageInfo.GetComponentInChildren<Text>().text = "-" + (health + shield.shieldPow);
            Instantiate(damageInfo, transform.position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
        }
        shield.shieldPow -= missile.GetDamage();
        ShieldStateHit();
    }
    private void DamagePlayerLaser2(LaserTarget missile) //нанесение урона игроку
    {
        if (health + shield.shieldPow >= missile.GetDamage())
        {
            damageInfo.GetComponentInChildren<Text>().text = "-" + missile.GetDamage();
            Instantiate(damageInfo, transform.position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
        }
        else
        {
            damageInfo.GetComponentInChildren<Text>().text = "-" + (health + shield.shieldPow);
            Instantiate(damageInfo, transform.position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
        }
        shield.shieldPow -= missile.GetDamage();
        ShieldStateHit();
    }
    private void DamageMeteor(Meteor meteor) //нанесение урона игроку
    {
        shield.shieldPow -= meteor.meteorHP;
        ShieldStateHit();
    }

    private void ShieldStateHit()
    {
        if (shield.shieldPow < 0)
        {
            health += shield.shieldPow;
            shield.shieldPow = 0;
            if (health < 0)
            {
                health = 0;
            }
        }
        shield.ShieldState();
    }
    void Fire()
    {
        SpriteRenderer playerSprite = FindObjectOfType<PlayerController>().GetComponentInChildren<SpriteRenderer>();
        if (playerSprite.enabled == true)
        {
            for (int i = 0; i < guns.Length; i++)
            { 
                Vector3 firePos = guns[i].transform.position;
                GameObject laser = Instantiate(laserPrefab, firePos, transform.rotation, laserPool.transform);
                laser.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * laserSpeed, ForceMode2D.Impulse);
            }
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }
    }

    void Die()
    {
        Drop();
        ScoreKeeper scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        scoreKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);
        Instantiate(boom, transform.position, Quaternion.identity);
        if (GetComponent<OverLord>())
        {
            Position[] positions = GetComponentsInChildren<Position>();
            foreach (Position position in positions)
            {
                Instantiate(boom, position.transform.position, Quaternion.identity);
            }
        }
        FindObjectOfType<EnemyFormation>().EnemyCounter();
        Destroy(gameObject);
    }
    void Die2()
    {
        Drop();
        ScoreKeeper scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        scoreKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);
        Instantiate(boom, transform.position, Quaternion.identity);
        if (GetComponent<OverLord>())
        {
            Position[] positions = GetComponentsInChildren<Position>();
            foreach (Position position in positions)
            {
                Instantiate(boom, position.transform.position, Quaternion.identity);
            }
        }
        Instantiate(megaExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Drop()
    {
        if (scoreBonus.Length != 0
            && healthBonus.Length != 0
            && shieldBonus.Length != 0
            && ammoBonus.Length != 0
            && maxBonus.Length != 0)
        {
            Transform bonusPool = GameObject.Find("BonusPool").transform;
            float dropChance = Random.Range(dropChanceMin, dropChanceMax);
            GameObject scoreUp = scoreBonus[Random.Range(0, scoreBonus.Length)];
            float scoreDropRate = scoreUp.GetComponent<MoverBonus>().dropRate;
            if (scoreDropRate >= dropChance)
            {
                Instantiate(scoreUp, transform.position, Quaternion.identity, bonusPool);
            }
            GameObject healthUp = healthBonus[Random.Range(0, healthBonus.Length)];
            float healthDropRate = healthUp.GetComponent<MoverBonus>().dropRate;
            if (healthDropRate >= dropChance)
            {
                Instantiate(healthUp, transform.position, Quaternion.identity, bonusPool);
            }
            GameObject shieldUp = shieldBonus[Random.Range(0, shieldBonus.Length)];
            float shieldDropRate = shieldUp.GetComponent<MoverBonus>().dropRate;
            if (shieldDropRate >= dropChance)
            {
                Instantiate(shieldUp, transform.position, Quaternion.identity, bonusPool);
            }
            GameObject ammoUp = ammoBonus[Random.Range(0, ammoBonus.Length)];
            float ammoDropRate = ammoUp.GetComponent<MoverBonus>().dropRate;
            if (ammoDropRate >= dropChance)
            {
                Instantiate(ammoUp, transform.position, Quaternion.identity, bonusPool);
            }
            GameObject maxUp = maxBonus[Random.Range(0, maxBonus.Length)];
            float maxDropRate = maxUp.GetComponent<MoverBonus>().dropRate;
            if (maxDropRate >= dropChance)
            {
                Instantiate(maxUp, transform.position, Quaternion.identity, bonusPool);
            }
        }
    }

    public bool IsAnimationPlaying(string animationName)
    {
        // берем информацию о состоянии
        var animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // смотрим, есть ли в нем имя какой-то анимации, то возвращаем true
        if (animatorStateInfo.IsName(animationName))
            return true;
        return false;
    }
}