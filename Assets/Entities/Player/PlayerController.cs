using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public int life = 3;
    public float health = 500f;
    public float healthMax = 500f;
    public float berylBrown = 0f, berylGrey = 0f;
    public float ammoCounter = 0f;
    public float berylBrownMax = 50f, berylGreyMax = 50f;
    public float speed = 15.0f;
    public float padding = 1f;
    public float costSuperFire = 100f;
    public GameObject laserPrefab1;
    public GameObject laserPrefab2;
    public GameObject laserPrefab3;
    public GameObject laserTarget;
    public GameObject superShot;
    public GameObject fireCharge1;
    public GameObject fireCharge2;
    public float laserSpeed;
    public float laserPower;
    public float firingRate = 0.2f;
    public AudioClip fireSound1;
    public AudioClip fireSound2;
    public AudioClip fireSound3;
    public AudioClip chargedFire;
    public AudioClip chargedFire2;
    public AudioClip downFire2;
    public AudioClip superFireSound;
    public AudioClip deathSound;
    public AudioClip playerRelife;
    public AudioClip powerUpSound;
    public AudioClip regenerationSound;
    public AudioClip ammoUpSound;
    public GameObject boom;
    public GameObject shieldObject;
    public GameObject healthCount;
    public GameObject shieldCount;
    public GameObject berylCountBrown;
    public GameObject berylCountGrey;
    public GameObject powerUp;
    public GameObject shieldUp;
    public GameObject shieldRegUp;
    public GameObject laserPowerUp;
    public GameObject damageInfo;
    public GameObject healthInfo;
    public GameObject shieldInfo;
    public GameObject ammoUp;
    public float powerUpCost = 20f;
    public int powerLvlCount = 0;
    public int shieldLvlCount = 0;
    public int laserLvlCount = 0;
    public int shieldRegLvlCount = 0;
    public bool magnetState = true;

    private float xMin, xMax;
    private Text lifeCounter;
    private GameObject engineFire;
    private Shield shield;
    private float regenerationHealthCost = 1f;
    private float regenerationShieldCost = 1f;
    private Slider powerlvl;
    private Slider shieldlvl;
    private Slider laserlvl;
    private Slider shieldReglvl;
    private Text powerlvlCounter;
    private Text shieldlvlCounter;
    private Text laserlvlCounter;
    private Text shieldReglvlCounter;
    private bool chargeStatus = false;
    private GameObject magnetField;
    private GodShield godShield;

    void Start()
    {
        laserPower = 100f;
        health = healthMax;
        lifeCounter = GameObject.Find("LifeCounter").GetComponent<Text>();
        shield = shieldObject.GetComponent<Shield>();
        engineFire = GameObject.Find("EngineFire");
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;
        powerlvl = GameObject.Find("PowerLVL").GetComponent<Slider>();
        shieldlvl = GameObject.Find("ShieldLVL").GetComponent<Slider>();
        laserlvl = GameObject.Find("LaserLVL").GetComponent<Slider>();
        shieldReglvl = GameObject.Find("ShieldRegLVL").GetComponent<Slider>();
        powerlvlCounter = GameObject.Find("PowerLvlCounter").GetComponent<Text>();
        shieldlvlCounter = GameObject.Find("ShieldLvlCounter").GetComponent<Text>();
        laserlvlCounter = GameObject.Find("LaserLvlCounter").GetComponent<Text>();
        shieldReglvlCounter = GameObject.Find("ShieldRegLvlCounter").GetComponent<Text>();
        magnetField = GameObject.Find("Magnet");
        godShield = FindObjectOfType<GodShield>();
        CounterReset();
    }

    private void CounterReset()
    {
        lifeCounter.text = life.ToString();
        healthCount.GetComponent<Text>().text = health.ToString();
        shieldCount.GetComponent<Text>().text = shield.shieldPow.ToString();
        berylCountBrown.GetComponent<Text>().text = berylBrown.ToString();
        berylCountGrey.GetComponent<Text>().text = berylGrey.ToString();
        powerlvl.maxValue = 95;
        powerlvl.value = 0;
        shieldlvl.maxValue = 95;
        shieldlvl.value = 0;
        laserlvl.maxValue = 95;
        laserlvl.value = 0;
        shieldReglvl.maxValue = 95;
        shieldReglvl.value = 0;
        powerlvlCounter.text = "lvl " + powerLvlCount.ToString();
        shieldlvlCounter.text = "lvl " + shieldLvlCount.ToString();
        laserlvlCounter.text = "lvl " + laserLvlCount.ToString();
        shieldReglvlCounter.text = "lvl " + shieldRegLvlCount.ToString();
        Text ammoCount = GameObject.Find("AmmoCounter").GetComponent<Text>();
        ammoCount.text = ammoCounter.ToString();
        magnetField.SetActive(false);
        godShield.GetComponent<CircleCollider2D>().enabled = false;
    }

    void Update()
    {
        // при нажатии на пробел создаем игровой объект лазера и придаем ему скорость
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && GetComponent<PolygonCollider2D>().enabled == true && chargeStatus == false)
        {
            CancelInvoke("Fire3");
            InvokeRepeating(nameof(Fire), 0.000001f, firingRate);
        }
        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            CancelInvoke("Fire");
        }
        
        // управляем движением корабля
        if (chargeStatus == false)
        {
            float x = CrossPlatformInputManager.GetAxis("Horizontal");
            transform.position += new Vector3(x,0,0) * speed * Time.deltaTime;
        }

        // активация супервыстрела
        if  (CrossPlatformInputManager.GetButtonDown("SuperFire")
            && GetComponent<PolygonCollider2D>().enabled == true 
            && ammoCounter >= costSuperFire
            && chargeStatus == false)
        {
            chargeStatus = true;
            CancelInvoke("Fire");
            CancelInvoke("Fire3");
            ammoCounter -= costSuperFire;
            Text ammoCount = GameObject.Find("AmmoCounter").GetComponent<Text>();
            ammoCount.text = ammoCounter.ToString();
            Vector3 offset = new Vector3(0, 1f, 0);
            Vector3 chargePos = transform.position + offset;
            if (firingRate == 0.2f)
            {
                Instantiate(fireCharge1, chargePos, Quaternion.identity, transform);
                AudioSource.PlayClipAtPoint(chargedFire, transform.position);
            } else
            {
                Instantiate(fireCharge2, chargePos, Quaternion.identity, transform);
                AudioSource.PlayClipAtPoint(chargedFire2, transform.position);
            }
            godShield.GetComponent<CircleCollider2D>().enabled = true;
            Invoke(nameof(Fire2), firingRate*15);
        }

        // стрельба самонаводящимися лазерами
        if (CrossPlatformInputManager.GetButtonDown("Fire2")
            && GetComponent<PolygonCollider2D>().enabled == true
            && ammoCounter > 0
            && chargeStatus == false)
        {
            CancelInvoke("Fire");
            InvokeRepeating(nameof(Fire3), 0.000001f, firingRate);
        }
        if (CrossPlatformInputManager.GetButtonUp("Fire2"))
        {
            CancelInvoke("Fire3");
        }

        // лечение
        if (CrossPlatformInputManager.GetButtonDown("Healing") && GetComponent<PolygonCollider2D>().enabled == true)
        {
            InvokeRepeating(nameof(Regeneration), 0.000001f, firingRate);
        }
        if (CrossPlatformInputManager.GetButtonUp("Healing"))
        {
            CancelInvoke("Regeneration");
        }

        //перекачка руды в оружие
        if (Input.GetKeyDown(KeyCode.X) && GetComponent<PolygonCollider2D>().enabled == true)
        {
            InvokeRepeating(nameof(Swipe), 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            CancelInvoke("Swipe");
        }

        // прокачка максимального уровня жизни
        if (Input.GetKeyDown(KeyCode.Alpha1) && GetComponent<PolygonCollider2D>().enabled == true)
        {
            HealthUpMax();
        }
        // прокачка максимальной энергии щита
        if (Input.GetKeyDown(KeyCode.Alpha2) && GetComponent<PolygonCollider2D>().enabled == true)
        {
            ShieldUpMax();
        }
        // прокачка урона лазером
        if (Input.GetKeyDown(KeyCode.Alpha3) && GetComponent<PolygonCollider2D>().enabled == true)
        {
            LaserPowerUp();
        }
        // прокачка скорости восстановления щита
        if (Input.GetKeyDown(KeyCode.Alpha4) && GetComponent<PolygonCollider2D>().enabled == true)
        {
            ShieldRegenerationUp();
        }

        // ограничиваем игрока игровым пространством
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        ShieldState();
    }
    void Fire()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        Vector3 firePos = transform.position + offset;
        GameObject laserPool = GameObject.Find("LaserPool");
        GameObject laserPrefab = laserPrefab1;
        AudioClip fireSound = fireSound1;
        if (laserLvlCount >= 50)
        {
            laserPrefab = laserPrefab2;
            fireSound = fireSound2;
        }
        if (laserLvlCount == 95)
        {
            laserPrefab = laserPrefab3;
            fireSound = fireSound3;
        }
        GameObject laser = Instantiate(laserPrefab, firePos, Quaternion.identity, laserPool.transform);
        laser.GetComponent<Laser>().damage = laserPower;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
    void Fire2()
    {
        godShield.GetComponent<CircleCollider2D>().enabled = false;
        Vector3 offset = new Vector3(0, 1f, 0);
        Vector3 firePos = transform.position + offset;
        GameObject laserPool = GameObject.Find("LaserPool");
        if (GetComponent<PolygonCollider2D>().enabled == true)
        {
            Instantiate(superShot, firePos, Quaternion.identity, laserPool.transform);
            AudioSource.PlayClipAtPoint(superFireSound, transform.position);
            chargeStatus = false;
            if (Input.GetKey(KeyCode.Space) && GetComponent<PolygonCollider2D>().enabled == true && chargeStatus == false)
            {
                InvokeRepeating("Fire", 0.000001f, firingRate);
            }
        }
    }
    void Fire3()
    {
        ammoCounter -= 1;
        Text ammoCount = GameObject.Find("AmmoCounter").GetComponent<Text>();
        ammoCount.text = ammoCounter.ToString();
        if (ammoCounter <= 0)
        {
            CancelInvoke("Fire3");
        }
        Transform nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Target"))
        {
            float currDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if (currDistance < nearestEnemyDistance)
            {
                nearestEnemy = enemy.transform;
                nearestEnemyDistance = currDistance;
                break;
            }
        }
        if (nearestEnemy != null)
        {
            Vector3 offset = new Vector3(0, 0.5f, 0);
            Vector3 firePos = transform.position + offset;
            GameObject laserPool = GameObject.Find("LaserPool");
            GameObject laser = Instantiate(laserTarget, firePos, Quaternion.identity, laserPool.transform);
            laser.GetComponent<LaserTarget>().damage = laserPower;
            laser.GetComponent<LaserTarget>().SetTarget(nearestEnemy);
            AudioSource.PlayClipAtPoint(fireSound1, transform.position);
        }
    }

    private void ShieldState()
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

    void OnTriggerEnter2D(Collider2D collision) //попадание лазером в игрока
    {
        Laser missile = collision.gameObject.GetComponent<Laser>();
        Meteor meteor = collision.gameObject.GetComponent<Meteor>();
        if (missile)
        {
            if (godShield.GetComponent<CircleCollider2D>().enabled == false)
            {
                DamageEnemyLaser(missile);
            }
            if (health <= 0)
            {
                Die();
            }
            missile.Hit();
        }
        if (meteor)
        {
            if (godShield.GetComponent<CircleCollider2D>().enabled == false)
            {
                DamageMeteor(meteor);
            }
            if (health <= 0)
            {
                Die();
            }
        }
    }
    
    private void DamageEnemyLaser(Laser missile) //нанесение урона игроку
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
        ShieldStateHit(shield);
    }
    private void DamageMeteor(Meteor meteor) //нанесение урона игроку
    {
        shield.shieldPow -= meteor.meteorHP;
        ShieldStateHit(shield);
    }

    private void ShieldStateHit(Shield shield)
    {
        AudioSource.PlayClipAtPoint(shield.shieldDown, transform.position);
        if (shield.shieldPow < 0)
        {
            health += shield.shieldPow;
            shield.shieldPow = 0;
            if (health < 0)
            {
                health = 0;
            }
            healthCount.GetComponent<Text>().text = health.ToString();
        }
        shieldCount.GetComponent<Text>().text = shield.shieldPow.ToString();
        shield.ShieldState();
    }

    void LoadWinScreen() //game over
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("Win Screen");
    }
    void Die() //смерть игрока
    {
        life -= 1;
        lifeCounter.text = life.ToString();
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        engineFire.SetActive(false);
        Instantiate(boom, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        MagnetOff();

        if (FindObjectOfType<MagnetTimer>() != null)
        {
            FindObjectOfType<MagnetTimer>().time = 0;
        }
        if (FindObjectOfType<SpeedTimer>() != null)
        {
            FindObjectOfType<SpeedTimer>().time = 0;
        }
        if (FindObjectOfType<FireSpeedTimer>() != null)
        {
            FindObjectOfType<FireSpeedTimer>().time = 0;
        }

        CancelInvoke("Fire");
        CancelInvoke("Fire3");
        if (life > 0)
        {
            Invoke(nameof(ReLife), deathSound.length);
        }
        else if (life <= 0) 
        {
            Invoke(nameof(LoadWinScreen), deathSound.length);
        }
    }
    void ReLife() //возрождение игрока
    {
        transform.position = new Vector3(0, -2.75f, 0);
        health = healthMax;
        shield.shieldPow = shield.shieldPowerMax;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        engineFire.SetActive(true);
        shield.ShieldState();
        AudioSource.PlayClipAtPoint(playerRelife, transform.position);
        healthCount.GetComponent<Text>().text = health.ToString();
        shieldCount.GetComponent<Text>().text = shield.shieldPow.ToString();
        if (Input.GetKey(KeyCode.Space) && GetComponent<PolygonCollider2D>().enabled == true && chargeStatus == false)
        {
            CancelInvoke("Fire3");
            InvokeRepeating(nameof(Fire), 0.000001f, firingRate);
        }
    }

    void Regeneration()
    {
        regenerationShieldCost = Mathf.RoundToInt(shield.shieldPowerMax / 500);
        regenerationHealthCost = Mathf.RoundToInt(healthMax / 1000);
        if (shield.shieldPow < shield.shieldPowerMax && berylGrey >= regenerationShieldCost)
        {
            berylGrey -= regenerationShieldCost;
            berylCountGrey.GetComponent<Text>().text = berylGrey.ToString();
            float regenerationSpeed = (shield.shieldPowerMax / 100) * 10;
            if (regenerationSpeed >= shield.shieldPowerMax - shield.shieldPow)
            {
                regenerationSpeed = shield.shieldPowerMax - shield.shieldPow;
            }
            shieldInfo.GetComponentInChildren<Text>().text = "+" + regenerationSpeed;
            Instantiate(shieldInfo, transform.position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
            shield.shieldPow += regenerationSpeed;
            Text shieldCount = GameObject.Find("ShieldCounter").GetComponent<Text>();
            shieldCount.text = shield.shieldPow.ToString();
            AudioSource.PlayClipAtPoint(regenerationSound, transform.position);
        } else 
        {
            if (berylBrown >= regenerationHealthCost && health < healthMax)
            {
                berylBrown -= regenerationHealthCost;
                berylCountBrown.GetComponent<Text>().text = berylBrown.ToString();
                float regenerationSpeed = (healthMax / 100) * 10;
                if (regenerationSpeed >= healthMax - health)
                {
                    regenerationSpeed = healthMax - health;
                }
                healthInfo.GetComponentInChildren<Text>().text = "+" + regenerationSpeed;
                Instantiate(healthInfo, transform.position, Quaternion.identity, GameObject.Find("Game Canvas").transform);
                health += regenerationSpeed;
                if (health > healthMax)
                {
                    health = healthMax;
                }
                Text healthCount = GameObject.Find("HealthCounter").GetComponent<Text>();
                healthCount.text = health.ToString();
                AudioSource.PlayClipAtPoint(regenerationSound, transform.position);
            }
        }
    }

    void Swipe()
    {
        Slider ammoSlider = FindObjectOfType<AmmoCount>().GetComponent<Slider>();
        if (ammoCounter < ammoSlider.maxValue && berylGrey >= powerUpCost && berylBrown >= powerUpCost)
        {
            berylGrey -= powerUpCost;
            berylBrown -= powerUpCost;
            float upCost = powerUpCost;
            if (upCost > ammoSlider.maxValue - ammoCounter)
            {
                upCost = ammoSlider.maxValue - ammoCounter;
            }
            ammoCounter += upCost;
            berylCountBrown.GetComponent<Text>().text = berylBrown.ToString();
            berylCountGrey.GetComponent<Text>().text = berylGrey.ToString();
            Text ammoCount = GameObject.Find("AmmoCounter").GetComponent<Text>();
            ammoCount.text = ammoCounter.ToString();
            GameObject parent = GameObject.Find("Game Canvas");
            ammoUp.GetComponentInChildren<Text>().text = upCost.ToString();
            Instantiate(ammoUp, transform.position, Quaternion.identity, parent.transform);
            AudioSource.PlayClipAtPoint(ammoUpSound, transform.position);
        }
    }

    void HealthUpMax()
    {
        if (berylBrown >= powerUpCost && powerlvl.value < 95)
        {
            berylBrown -= powerUpCost;
            berylCountBrown.GetComponent<Text>().text = berylBrown.ToString();
            healthMax += 200f;
            Slider sliderHealth = GameObject.Find("Health Player").GetComponent<Slider>();
            sliderHealth.maxValue = healthMax;
            powerLvlCount += 1;
            powerlvlCounter.text = "lvl " + powerLvlCount.ToString();
            powerlvl.value = powerLvlCount;
            AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
            Instantiate(powerUp, transform.position, Quaternion.identity);
        } 
    }
    void ShieldUpMax()
    {
        if (berylGrey >= powerUpCost && shieldlvl.value < 95)
        {
            berylGrey -= powerUpCost;
            berylCountGrey.GetComponent<Text>().text = berylGrey.ToString();
            shield.shieldPowerMax += 100f;
            shield.regenerationSpeed += 0.2f;
            shield.RegenerationUpdate();
            Slider sliderShield = GameObject.Find("Shield Player").GetComponent<Slider>();
            sliderShield.maxValue = shield.shieldPowerMax;
            shieldLvlCount += 1;
            shieldlvlCounter.text = "lvl " + shieldLvlCount.ToString();
            shieldlvl.value = shieldLvlCount;
            AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
            Instantiate(shieldUp, transform.position, Quaternion.identity);
        }
    }
    void ShieldRegenerationUp()
    {
        if (berylBrown >= powerUpCost && berylGrey >= powerUpCost && shieldReglvl.value < 95)
        {
            berylBrown -= powerUpCost;
            berylCountBrown.GetComponent<Text>().text = berylBrown.ToString();
            berylGrey -= powerUpCost;
            berylCountGrey.GetComponent<Text>().text = berylGrey.ToString();
            shield.regenerationValue += 0.2f;
            shieldRegLvlCount += 1;
            shieldReglvlCounter.text = "lvl " + shieldRegLvlCount.ToString();
            shieldReglvl.value = shieldRegLvlCount;
            AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
            Instantiate(shieldRegUp, transform.position, Quaternion.identity);
        }
    }

    void LaserPowerUp()
    {
        if (berylBrown >= powerUpCost && berylGrey >= powerUpCost && laserlvl.value < 95)
        {
            berylBrown -= powerUpCost;
            berylCountBrown.GetComponent<Text>().text = berylBrown.ToString();
            berylGrey -= powerUpCost;
            berylCountGrey.GetComponent<Text>().text = berylGrey.ToString();
            laserPower += powerUpCost*2;
            laserLvlCount += 1;
            laserlvlCounter.text = "lvl " + laserLvlCount.ToString();
            laserlvl.value = laserLvlCount;
            AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
            Instantiate(laserPowerUp, transform.position, Quaternion.identity);
        }
    }

    [System.Obsolete]
    public void MagnetFieldOn()
    {
        magnetField.GetComponent<ParticleSystem>().loop = true;
        magnetField.SetActive(true);
    }

    [System.Obsolete]
    public void MagnetFieldOff()
    {
        magnetField.GetComponent<ParticleSystem>().loop = false;
        Invoke(nameof(MagnetOff), 2f);
    }

    private void MagnetOff()
    {
        magnetField.SetActive(false);
    }

    public void FireSpeedReset()
    {
        CancelInvoke("Fire");
        CancelInvoke("Fire3");
        if (Input.GetKey(KeyCode.Space) && GetComponent<PolygonCollider2D>().enabled == true && chargeStatus == false)
        {
            CancelInvoke("Fire3");
            InvokeRepeating(nameof(Fire), 0.000001f, firingRate);
        }
        if (Input.GetKey(KeyCode.LeftAlt)
            && GetComponent<PolygonCollider2D>().enabled == true
            && ammoCounter > 0
            && chargeStatus == false)
        {
            CancelInvoke("Fire");
            InvokeRepeating(nameof(Fire3), 0.000001f, firingRate);
        }
    }

    [System.Obsolete]
    public void ShadowPlayerOff(GameObject shapowPlayer)
    {
        shapowPlayer.GetComponent<ParticleSystem>().loop = false;
        Destroy(shapowPlayer, 0.5f);
    }
}