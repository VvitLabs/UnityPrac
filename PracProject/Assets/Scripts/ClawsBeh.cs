using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClawsBeh : MeleeWeaponBeh {
    List<GameObject> markedEnemies;
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.collider.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);

            markedEnemies.Add(col.gameObject);
        }
       else if (col.collider.CompareTag("Wall"))
        {
            Wall wall = col.collider.GetComponent<Wall>();
            wall.DamageWall(currentDamage);
        }

    }

}
