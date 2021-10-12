using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private int scoreValue;

    [SerializeField] UIController uIController;
    [SerializeField] public Image healthBar;

    private float maxHealth;

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
        maxHealth = enemyHealth;
    }

    public void takeDamage(float amount)
    {
        enemyHealth -= amount;

        healthBar.fillAmount = enemyHealth / maxHealth;

        if (enemyHealth <= 0)
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

        // Decrease count of enemies alive
        RoundController.EnemiesAlive--;
    }

    // Decrease lives and destroy object
    public void reachedEnd()
    {
        PlayerStats.lives -= (int)enemyHealth;
        uIController.setLives();
        Destroy(gameObject);

        // Decrease count of enemies alive
        RoundController.EnemiesAlive--;
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
