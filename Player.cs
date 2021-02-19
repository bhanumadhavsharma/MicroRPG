using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public int curHp;
    public int maxHp;
    public int damage;
    public List<string> inventory = new List<string>();

    [Header("Interact")]
    public float interactRange;

    [Header("Experience")]
    public int curLevel;
    public int curXp;
    public int xpToNextLevel;
    public float levelXpModifier;

    [Header("Combat")]
    public KeyCode attackKey;
    public float attackRange;
    public float attackRate;
    float lastAttackTime;

    [Header("Sprites")]
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    // components
    Rigidbody2D rgb;
    SpriteRenderer sr;
    ParticleSystem hitEffect;
    PlayerUI ui;

    //other
    Vector2 facingDirection;

    private void Awake()
    {
        // get the components
        rgb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        hitEffect = gameObject.GetComponentInChildren<ParticleSystem>();
        ui = FindObjectOfType<PlayerUI>();
    }

    private void Update()
    {
        Movement();
        if (Input.GetKeyDown(attackKey))
        {
            if (Time.time - lastAttackTime >= attackRate)
            {
                Attack();
            }
        }
        CheckInteract();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 velocity = new Vector2(x, y);

        if (velocity.magnitude != 0)
        {
            facingDirection = velocity;
        }

        UpdateSpriteDirection();
        rgb.velocity = velocity * moveSpeed;
    }

    void UpdateSpriteDirection()
    {
        if (facingDirection == Vector2.up)
        {
            sr.sprite = upSprite;
        }
        else if (facingDirection == Vector2.down)
        {
            sr.sprite = downSprite;
        }
        else if (facingDirection == Vector2.right)
        {
            sr.sprite = rightSprite;
        }
        else if (facingDirection == Vector2.left)
        {
            sr.sprite = leftSprite;
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, attackRange, 1 << 6);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<EnemyController>().TakeDamage(damage);

            hitEffect.transform.position = hit.collider.transform.position;
            hitEffect.Play();
        }
    }
    public void TakeDamage(int damageTaken)
    {
        curHp -= damageTaken;
        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void AddXP(int xp)
    {
        curXp += xp;
        if (curXp >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        curXp -= xpToNextLevel;
        curLevel++;
        xpToNextLevel = Mathf.RoundToInt((float)xpToNextLevel * levelXpModifier);

        damage++;
    }

    void CheckInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, interactRange, 1 << 7);
        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            ui.SetInteractText(hit.collider.transform.position, interactable.interactDescription);

            if (Input.GetKeyDown(attackKey))
            {
                interactable.Interact();
            }
        }
        else
        {
            ui.DisableInteractText();
        }
    }

    public void AddItemToInventory(string item)
    {
        inventory.Add(item);
    }
}
