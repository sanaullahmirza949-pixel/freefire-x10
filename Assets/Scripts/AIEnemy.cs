using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;
    public float detectionRange = 25f;
    public float attackRange = 10f;
    public float attackDamage = 10f;
    public float attackInterval = 1.2f;

    Transform player;
    NavMeshAgent agent;
    float lastAttackTime = 0f;

    void Start()
    {
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (dist <= attackRange)
            {
                agent.isStopped = true;
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

                if (Time.time - lastAttackTime >= attackInterval)
                {
                    // Simple melee/ranged hit
                    var playerHealth = player.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                        playerHealth.TakeDamage(attackDamage);
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            // wander
            if (!agent.hasPath || agent.remainingDistance < 1f)
            {
                Vector3 randomDir = Random.insideUnitSphere * 10f;
                randomDir += transform.position;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDir, out hit, 10f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
            }
        }
    }

    public void TakeDamage(float d)
    {
        health -= d;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        // TODO: death VFX / ragdoll
        Destroy(gameObject);
        GameManager.Instance.OnEnemyKilled();
    }
}
