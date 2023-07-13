using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    public int wallDamage = 1;
    public float hppermed = 10;
    public float restartLevelDelay = 2f;

    public Animator animator;
    private int score;
    private BoardManager boardScr;
    private Rigidbody2D rb;
    public CharacterScriptableObject characterData;
    public Vector2 moveDir;
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        score = GameManager.instance.playerScore;
        boardScr = GetComponent<BoardManager>();
        base.Start();
        rb = GetComponent<Rigidbody2D>();

    }
   /* private void OnDisable()
    {
        GameManager.instance.playerHp = hp;
        GameManager.instance.playerScore = score;
    }
   */
    private void Update()
    {
        //if (!GameManager.instance.playersTurn) return;

        float horizontal = 0;
        float vertical = 0;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log($"{horizontal} {vertical}");
        moveDir = new Vector2(horizontal, vertical).normalized;
        if (moveDir.x != 0 || moveDir.y != 0) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
        //Debug.Log($"{moveDir.x} {moveDir.y}");
        rb.velocity = new Vector2(moveDir.x * characterData.MoveSpeed, moveDir.y * characterData.MoveSpeed);
    }
    protected override void AttemptMove<T>(int xDir, int yDir)
    {

        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            animator.SetTrigger("playerRun");
        }
        //GameManager.instance.playersTurn = false;

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Weapon") && !col.collider.CompareTag("Enemy") && !col.collider.CompareTag("Bullet"))
            rb.MovePosition(new Vector2(rb.position.x-moveDir.x*0.25f, rb.position.y-moveDir.y*0.25f));
    }
    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerAttack");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door")
        {
            
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Health")
        {
            PlayerStats meds = GetComponent<PlayerStats>();
            meds.Heal(hppermed);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Bullet")
        {
            PlayerStats bullet = GetComponent<PlayerStats>();
            animator.SetTrigger("playerTakingDMG");
            other.gameObject.SetActive(false);
            bullet.TakeDamage(10);
        }
    }

    private void Restart()
    {

        SceneManager.LoadScene(0);
        
    }
}
