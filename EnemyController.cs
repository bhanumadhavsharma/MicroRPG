using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public int curHp;
    public int maxHp;
    public int xpToGive;

    [Header("Target")]
    public float chaseRange;
    public float attackRange;
    Player player;

    [Header("Attack")]
    public int damage;
    public float attackRate;
    float lastAttackTime;

    //components
    Rigidbody2D rgb;

    private void Awake()
    {
        rgb = this.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDist = Vector2.Distance(transform.position, player.transform.position);

        if (playerDist <= attackRange)
        {
            if (Time.time - lastAttackTime >= attackRate)
            {
                Attack();
            }
        }
        else if (playerDist <= chaseRange)
        {
            Chase();
        }
        else
        {
            rgb.velocity = Vector2.zero;
        }
    }

    void Chase()
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;

        rgb.velocity = dir * moveSpeed;
    }

    void Attack()
    {
        lastAttackTime = Time.time;
        player.TakeDamage(damage);
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
        player.AddXP(xpToGive);
        Destroy(this.gameObject);
    }
}
