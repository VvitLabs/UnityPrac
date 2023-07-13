using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public AudioClip Sound1;
    public AudioClip Sound2;
    public Sprite dmgSprite;
    public float hp = 4;
    public float DesroyWithSec = 0.01f;

    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public void DamageWall(float loss)
    {
        //SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);
        //spriteRenderer.sprite = dmgSprite;
        hp -= loss;
        if (hp <= 0)
            gameObject.SetActive(false);
    }


}
