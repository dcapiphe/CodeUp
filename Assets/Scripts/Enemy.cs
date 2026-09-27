using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int maxHP = 5;

    private int currentHP;

    public int CurrentHP
    {
        get { return currentHP; }
    }

    public bool IsDead
    {
        get { return currentHP <= 0; }
    }

    private void Awake()
    {
        ResetEnemy();
    }

    public void ResetEnemy()
    {
        currentHP = maxHP;
        gameObject.SetActive(true);
    }

    public void TakeDamage()
    {
        if (IsDead)
            return;

        currentHP--;

        if (currentHP < 0)
            currentHP = 0;

        Debug.Log(gameObject.name + " HP: " + currentHP);

        if (IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died!");
        gameObject.SetActive(false);
    }
}