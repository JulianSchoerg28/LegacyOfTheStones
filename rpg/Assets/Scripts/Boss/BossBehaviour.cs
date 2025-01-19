using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour
{
    public Transform player; // Referenz auf den Spieler
    public float followSpeed = 2f; // Geschwindigkeit des Bosses
    public float attackRange = 3f; // Reichweite für den Angriff
    public float specialAttackRange = 5f; // Reichweite für Spezialangriffe
    public float health = 300f; // Lebenspunkte des Bosses
    public float attackDamage = 20f; // Schaden des normalen Angriffs
    public float specialAttackDamage = 40f; // Schaden des Spezialangriffs
    public float attackCooldown = 2f; // Abklingzeit für normale Angriffe
    public float specialAttackCooldown = 5f; // Abklingzeit für Spezialangriffe

    private float nextAttackTime = 0f;
    private float nextSpecialAttackTime = 0f;

    private Animator animator;

    void Start()
    {
        if (player == null) // Spieler automatisch finden, wenn nicht zugewiesen
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Spieler gefunden: " + player.name);
            }
            else
            {
                Debug.LogError("Kein Spieler gefunden! Überprüfe den Tag des Spieler-Objekts.");
            }
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // Berechne die Distanz zum Spieler
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Wenn der Spieler in der Spezialangriff-Range ist, Spezialangriff starten
        if (distanceToPlayer <= specialAttackRange && Time.time >= nextSpecialAttackTime)
        {
            SpecialAttack();
        }
        // Wenn der Spieler in der normalen Angriffsreichweite ist, Angriff starten
        else if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }
        // Wenn der Spieler außerhalb der Angriffsreichweite ist, bewege dich auf ihn zu
        else if (distanceToPlayer > attackRange)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Bewegung in Richtung des Spielers
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);

        Debug.Log("Boss bewegt sich in Richtung des Spielers.");
    }

    void AttackPlayer()
    {
        nextAttackTime = Time.time + attackCooldown;

        Debug.Log("Boss führt einen normalen Angriff aus!");

        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
        }

        // Schaden an Spieler zufügen
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            GameManager.Instance.TakeDamage(Mathf.RoundToInt(attackDamage));
            Debug.Log($"Boss hat dem Spieler {attackDamage} Schaden zugefügt!");
        }

        // Zurücksetzen des Angriffs
        StartCoroutine(ResetAttack());
    }

    void SpecialAttack()
    {
        nextSpecialAttackTime = Time.time + specialAttackCooldown;

        Debug.Log("Boss führt einen Spezialangriff aus!");

        if (animator != null)
        {
            animator.SetBool("isAttacking", true); // Gleiche Animation für normalen und Spezialangriff
        }

        // Schaden an Spieler im Spezialangriffsbereich
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, specialAttackRange);
        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                GameManager.Instance.TakeDamage(Mathf.RoundToInt(specialAttackDamage));
                Debug.Log($"Boss hat dem Spieler {specialAttackDamage} Schaden durch Spezialangriff zugefügt!");
            }
        }

        // Zurücksetzen des Angriffs
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Wartezeit, bis der Angriff abgeschlossen ist
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log($"Boss hat {damage} Schaden erhalten. Verbleibende Gesundheit: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss wurde besiegt!");

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Entferne den Boss aus der Szene nach einer kurzen Verzögerung
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualisiere die Angriffsdistanzen
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, specialAttackRange);
    }
}
