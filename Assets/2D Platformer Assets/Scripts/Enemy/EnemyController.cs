using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isFrog;

    private Rigidbody2D enemyRb;
    private Animator enemyAnim;
    [SerializeField] private SpriteRenderer enemySR;
    

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform leftPos, rightPos;
    private bool isMovingRight = true;
    private float moveCounter = 2.0f;
    private float pauseCounter;
    

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        

        leftPos.parent = null;
        rightPos.parent = null;
    }

    private void Update()
    {
        if (moveCounter > 0)
        {
            //倒數移動時間
            moveCounter -= Time.deltaTime;

            enemyAnim.SetBool("isMoving", true);
            //移動
            if (isMovingRight)
            {
                enemyRb.velocity = new Vector2(moveSpeed, enemyRb.velocity.y);
                enemySR.flipX = true;

                if (transform.position.x > rightPos.position.x)
                {
                    isMovingRight = false;
                }
            }
            else
            {
                enemyRb.velocity = new Vector2(-moveSpeed, enemyRb.velocity.y);
                enemySR.flipX = false;

                if (transform.position.x < leftPos.position.x)
                {
                    isMovingRight = true;
                }
            }

            if (moveCounter <= 0)
            {
                pauseCounter = Random.Range(0.5f, 2);
            }
        }
        if(pauseCounter>0)
        {
            //倒數暫停時間
            pauseCounter -= Time.deltaTime;

            enemyAnim.SetBool("isMoving", false);

            enemyRb.velocity = new Vector2(0f,0f);

            if (pauseCounter <= 0)
            {
                moveCounter = Random.Range(1, 3);
            }
        }
    }

}

