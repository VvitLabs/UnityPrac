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
    #region CurrentStats
    public float CurrentHealth
    {
        get { return currentHp; }
        set
        {
            if (currentHp != value)
            {
                currentHp = value;
            }
        }
    }
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
            }
        }
    }
    public float CurrentProjSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
            }
        }
    }
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
            }
        }
    }
    #endregion

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
        //characterData = CharacterSelector.GetData();
        //Debug.Log(characterData.MaxHealth);

        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentProjSpeed = characterData.ProjectileSpeed;
        CurrentMight = characterData.Might;
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
        CurrentHealth = CurrentHealth + amount > characterData.MaxHealth ? characterData.MaxHealth : CurrentHealth + amount;
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvinc)
        {
            animator.SetTrigger("playerTakingDMG");
            CurrentHealth -= dmg;
            invincTimer = invincDur;
            isInvinc = true;
        }
        //currentHp -= dmg;
        if (CurrentHealth <= 0)
           Dead();
    }

    public void Dead()
    {
        Debug.Log("GAME_OVER");
        animator.SetTrigger("playerDead");
        Time.timeScale = 0f;
        //Time.unscaledTime

        animator.ResetTrigger("playerDead");
    }
}
