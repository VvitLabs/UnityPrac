using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPGem : MonoBehaviour, ICollec
{
    public int xpGranted;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncExp(xpGranted);
        Destroy(gameObject);

    }

}
