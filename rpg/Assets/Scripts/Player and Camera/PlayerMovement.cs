using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float normalSpeed = 4f;
    public float sprintSpeed = 7f;
    private float moveSpeed = 4f;
    private float playerHealth = 100;
    public float playerAttackDamage = 10f;
    

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start() {
        //get all components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {

        if (!GameManager.Instance.PlayerCanMove)
        {
            return;
        }
        
        //get input of horizontal (A/D or controller) and vertical (W/S) axis. 
        //with getaxisraw you�ll get 1/0/-1
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //set 2D Vector to the values you want to move the character 
        movement.x = horizontal;
        movement.y = vertical;

        //set horizontal and vertical values in the animator to choose the right animation
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        //flip Sprite if you want to go the other direction of which the sprite is showing
        //so flip it if you want to go right bc the sprite normally shows the player going left 
        if (horizontal > 0) {
            spriteRenderer.flipX = true; 
        } else if (horizontal < 0) {
            spriteRenderer.flipX = false; 
        }

        //normalize the vector so you�ll have a new vector with the same direction as the original but without a speed
        //without this you would have a different speed if you press for example w and a at the same time. 
        
        movement = movement.normalized;


        //change speed for sprint when Control is pressed 
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            moveSpeed = sprintSpeed; 
        } else {
            moveSpeed = normalSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackEnemy();
        }

    }

    void FixedUpdate() {
        //move rigit body 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void AttackEnemy()
    {
        // Definiere einen LayerMask für Gegner, falls du Layer benutzt (optional)
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1.5f); // Nahkampfradius

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log($"Getroffenes Objekt: {enemy.name}, Tag: {enemy.tag}");

            // Nur Objekte mit dem Tag 'Enemy' weiter verarbeiten
            if (!enemy.CompareTag("Enemy"))
            {
                Debug.LogWarning($"Überspringe Objekt: {enemy.name}, da es nicht den Tag 'Enemy' hat.");
                continue; // Fahre mit dem nächsten Treffer fort
            }

            // Prüfe, ob es ein normaler Gegner oder ein Boss ist
            EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
            BossEnemy bossEnemy = enemy.GetComponent<BossEnemy>();

            if (enemyBehavior != null)
            {
                enemyBehavior.TakeDamage(playerAttackDamage);
                Debug.Log($"Spieler hat dem Gegner {playerAttackDamage} Schaden zugefügt!");
            }
            else if (bossEnemy != null)
            {
                bossEnemy.TakeDamage(playerAttackDamage);
                Debug.Log($"Spieler hat dem Boss {playerAttackDamage} Schaden zugefügt!");
            }
            else
            {
                Debug.LogError($"Das Objekt {enemy.name} hat den Tag 'Enemy', aber keine gültige Gegner-Komponente.");
            }
        }
    }






    public void ApplySpeedBoost(float multiplier)
    {
        moveSpeed = normalSpeed * multiplier;
        Debug.Log("Speed Boost angewendet! Neue Geschwindigkeit: " + moveSpeed);
    }


    
    void Die()
    {
        Debug.Log("Spieler ist gestorben!");
        // Hier kannst du das Spiel neu starten oder den Spieler respawnen lassen
    }
    
}
