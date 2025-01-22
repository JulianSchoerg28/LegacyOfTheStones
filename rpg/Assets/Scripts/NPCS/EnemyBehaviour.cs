using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player; // Referenz auf den Spieler
    public float followSpeed = 2f; // Geschwindigkeit des Gegners
    public float attackRange = 1.5f; // Reichweite für den Angriff
    public float followRange = 3f; // Reichweite, ab der der Gegner dem Spieler folgt
    public float health = 100f; // Lebenspunkte des Gegners
    public float attackDamage = 10f; // Schaden, den der Gegner verursacht
    public float dropChance = 0.5f; // Wahrscheinlichkeit für einen Drop (50%)
    public GameObject coinPrefab; // Prefab für Münzen
    public GameObject healthPotionPrefab; // Prefab für Heiltränke

    public int xpReward = 20; // XP-Belohnung für das Besiegen des Gegners

    private float nextAttackTime = 2f; // Zeit, wann der nächste Angriff möglich ist
    public float attackCooldown = 2f; // Abklingzeit in Sekunden
    private Animator animator; // Animator des Gegners

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Spieler gefunden: " + player.name);
            }
            else
            {
                Debug.LogError("Kein Objekt mit dem Tag 'Player' gefunden!");
            }
        }

        animator = GetComponent<Animator>();
        Debug.Log("EnemyBehavior Script gestartet!");
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Spieler ist null. Überprüfe die Zuweisung oder den Tag des Spielers.");
            return;
        }

        // Distanz zwischen Spieler und Gegner berechnen
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (animator != null)
            {
                animator.SetBool("isAttacking", true);
            }
            AttackPlayer();
        }
        else if (distanceToPlayer <= followRange) // Spieler verfolgen, wenn innerhalb der Follow-Range
        {
            if (animator != null)
            {
                animator.SetBool("isAttacking", false);
            }
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Berechne die Richtung und bewege den Gegner
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);

        Debug.Log("Gegner bewegt sich in Richtung des Spielers.");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 100);
        Debug.Log("Gegner hat " + damage + " Schaden erhalten. Verbleibende HP: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Gegner ist gestorben!");

        // XP-Belohnung vergeben
        GiveXP();

        // Droppen von Items
        DropItem();

        Destroy(gameObject); // Gegner entfernen
    }

    void DropItem()
    {
        // Wahrscheinlichkeit für einen Drop
        if (Random.value < dropChance)
        {
            GameObject itemToDrop;

            // Zufällig entscheiden, ob Münzen oder Heiltrank gedroppt werden
            if (Random.value < 0.5f)
            {
                itemToDrop = coinPrefab;
            }
            else
            {
                itemToDrop = healthPotionPrefab;
            }

            // Objekt in der Szene erzeugen
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
            Debug.Log(itemToDrop.name + " wurde gedroppt.");
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                if (distanceToPlayer <= attackRange)
                {
                    GameManager.Instance.TakeDamage(Mathf.RoundToInt(attackDamage));
                    Debug.Log("Gegner hat dem Spieler " + attackDamage + " Schaden zugefügt!");
                }
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void GiveXP()
    {
        if (XPManager.instance != null)
        {
            XPManager.instance.AddXP(xpReward);
            Debug.Log("Spieler hat " + xpReward + " XP erhalten!");
        }
        else
        {
            Debug.LogError("XPManager nicht gefunden! Stelle sicher, dass ein XPManager in der Szene vorhanden ist.");
        }
    }
}
