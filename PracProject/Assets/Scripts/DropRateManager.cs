using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;

    }

    public List<Drops> drops;

    private void OnDestroy()
    {
        float randomNum = Random.Range(0f, 100f);
        List<Drops> posDrops = new List<Drops>();
        foreach ( Drops rate in drops)
        {
            if (randomNum <= rate.dropRate)
            {
                posDrops.Add(rate);
                
            }
        }
        if (posDrops.Count > 0)
        {
            Drops drops = posDrops[Random.Range(0, posDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }

    }
}
