using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBeh : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroAfterSeconds;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;
    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroAfterSeconds);
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Wall"))
        {
            Wall wall = col.collider.GetComponent<Wall>();
            wall.DamageWall(currentDamage);
        }
        else if (col.collider.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.collider.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
        }
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
}
