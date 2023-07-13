using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0;
        float vertical = 0;
        float posX = transform.position.x - player.position.x;
        float posY = transform.position.y - player.position.y;
        horizontal = posX > 0 ? -1 : 1;
        vertical = posY > 0 ? -1 : 1;
        Vector2 moveDir = new Vector2(horizontal, vertical).normalized;
        rb.velocity = new Vector2(moveDir.x* enemyData.MoveSpeed, moveDir.y*enemyData.MoveSpeed);
    }
   
}

