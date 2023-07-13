using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawsController : WeaponControl
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedClaws = Instantiate(weaponData.Prefab);
        spawnedClaws.transform.position = transform.position;
        spawnedClaws.transform.parent = transform;
    }
}
