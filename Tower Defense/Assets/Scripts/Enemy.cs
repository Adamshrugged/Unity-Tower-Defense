using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private int scoreValue;

    [SerializeField] UIController uIController;

    // Damage over time effect
    float damageOverTime = 0f;
    float damageOverTimeDuration = 0f;

    // Enemy Movement scripts
    private EnemyMovement enemyMovement;

    // Money rewarded to player upon death
    private int killReward;

    // Damage inflicted when end reached
    private int damage;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
        uIController = GameObject.FindGameObjectWithTag("UIUpdateText").GetComponent<UIController>();
    }

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void takeDamage(float amount)
    {
        enemyHealth -= amount;

        if(enemyHealth <= 0)
        {
            die();
        }
    }

    // Sets a damage over time effect
    public void takeDamageOverTime(float damage, float time)
    {
        // [TODO] - pick higher of existing or new damage (and time?)
        damageOverTime = damage;
        damageOverTimeDuration = time;
    }
    

    private void die()
    {
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);

        // reward player for death
        uIController.setScore(scoreValue);
    }

    // Decrease lives and destroy object
    public void reachedEnd()
    {
        PlayerStats.lives -= (int)enemyHealth;
        uIController.setLives();
        Destroy(gameObject);
    }

    private void Update()
    {
        // Apply damage over time if effective
        if (damageOverTimeDuration > 0f)
        {
            takeDamage(damageOverTime * Time.deltaTime);
            damageOverTimeDuration -= Time.deltaTime;
        }
    }
}
