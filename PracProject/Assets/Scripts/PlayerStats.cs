using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    private float currentHp;
    private float currentRecovery;
    private float currentMoveSpeed;
    private float currentProjectileSpeed;
    private float currentMight;
    private Animator animator;
    
    [Header("I-F")]
    public float invincDur;
    float invincTimer;
    bool isInvinc;

    [Header("Experience/Level")]
    public int exp = 0;
    public int lvl = 1;
    public int expCap;

    [System.Serializable]
    public class lvlRange
    {
        public int startLvl;
        public int endLvl;
        public int expCapInc;
    }
    public List<lvlRange> LvlRanges;
    private void Awake()
    {
        currentHp = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMight = characterData.Might;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        expCap = LvlRanges[0].expCapInc; 
    }

    public void IncExp(int amount)
    {
        exp += amount;
        LevelUpCheck();
    }
    public void LevelUpCheck()
    {
        if (exp >= expCap)
        {
            lvl++;
            exp -= expCap;

            int expCapInc = 0;
            foreach (lvlRange range in LvlRanges)
            {
                if (lvl >= range.startLvl && lvl <= range.endLvl)
                {
                    expCapInc = range.expCapInc;
                    break;
                }
            }
            expCap = (int)(expCap + expCapInc*(1+0.1*lvl));
        }
    }
    void Update()
    {
        if (invincTimer > 0) invincTimer -= Time.deltaTime;
        else isInvinc = false;
    }
    public void Heal(float amount)
    {
        currentHp = currentHp+amount > characterData.MaxHealth ? characterData.MaxHealth : currentHp+amount;
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvinc)
        {
            animator.SetTrigger("playerTakingDMG");
            currentHp -= dmg;
            invincTimer = invincDur;
            isInvinc = true;
        }
        //currentHp -= dmg;
        if (currentHp <= 0)
           Dead();
    }

    public void Dead()
    {
        Debug.Log("GAME_OVER");
        animator.SetTrigger("playerDead");
        //new WaitForSeconds(1);
        //GameManager.instance.GameO();
    }
}
