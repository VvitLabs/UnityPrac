using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    // Start is called before the first frame update
    public float currentMoveSpeed;
    public float currentHealth;
    public float currentDamage;
    public bool currentFlying;
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        currentFlying = enemyData.Flying;
    }
    public void TakeDamage(float dmg)
    {
        currentHealth = currentHealth - dmg;
        if (currentHealth <= 0) Kill();

    }
    public void Kill()
    {
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log($"{collision.collider.tag}");
        if (collision.collider.tag == "Player")
        {
            PlayerStats pl = collision.collider.GetComponent<PlayerStats>();
            pl.TakeDamage(currentDamage);

        }
    }




}
