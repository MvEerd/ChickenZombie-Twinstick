using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [System.Serializable]
    public class Lootobject
    {
        public string name;
        public GameObject prefab;
        public int dropRate;
    }

    public GameObject target;
    public NavMeshAgent agent;
    public List<Lootobject> lootTable = new List<Lootobject>();
    public int dropRate;
    private bool alive = true;
    private float _health = 100;
    public float maxHealth = 100;
    public float attackDelay = 1;
    float _lastAttack = 0;
    public GameObject healthbarCanvas;
    public Slider healthBar;
    public float _healthBarTime;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;

        if (!target) return;
        agent.destination = target.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (_health <= 0){
            alive = false;
            destroySelf();
            return;
        }

        if (healthbarCanvas.activeSelf)
        {
            _healthBarTime += Time.deltaTime;
            if (_healthBarTime > 2)
                healthbarCanvas.SetActive(false);
        }

        if (!target) return;
        agent.destination = target.transform.position;
    }
    
    private void OnCollisionStay(Collision collision)
    {
        
        if (!alive) return;

        if (collision.collider.tag=="Player")
        {
            PlayerControl playerControl = collision.collider.GetComponent<PlayerControl>();

            /*if (other.GetComponent<Collider>().bounds.min.y >= GetComponent<Collider>().bounds.center.y) {
                if (playerControl.jumpedEnemy > 0 && playerControl.jumpedEnemy < 0.1) return;
                playerControl.Hop();
                playerControl.jumpedEnemy = 0;
                destroySelf();
                return;
            }*/

            if (Time.time < _lastAttack + attackDelay) return;

            collision.collider.gameObject.GetComponent<PlayerControl>().Damage(10);
            _lastAttack = Time.time;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (!alive) return;

        if (collider.tag == "projectile")
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            Damage(bullet.damage);
            if(!bullet.piercing)
                Destroy(collider.gameObject); // Destroy projectile

            return;
        }

    }

    private void destroySelf() 
    {
        dropLoot();

        Destroy(gameObject);
    }

    public void Damage(float damage = 100)
    {
        _health -= damage;
        if (_health <= 0)
        {
            alive = false;
            destroySelf();
        }
        if(_health < maxHealth)
        {
            healthBar.value = _health;
            _healthBarTime = 0;
            healthbarCanvas.SetActive(true);
        }
    }

    private void dropLoot()
    {
        if (Random.Range(0, 100) > dropRate) return;


        for (int i = 0; i < lootTable.Count; i++)
        {
            if (Random.Range(0, 100) > lootTable[i].dropRate) return;

            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 0.6f;
            randomPosition.y = 0.25f;
            GameObject drop = Instantiate(lootTable[i].prefab, randomPosition, transform.rotation) as GameObject;
            }
        }


    }
