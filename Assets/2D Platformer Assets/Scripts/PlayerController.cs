using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D playerRb;
    private float horizontalInput;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    //控制角色動畫
    private Animator playerAnim;
    private SpriteRenderer playerSpRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSpRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();

        // 檢查是否重疊(以groundCheckPoint的位置畫一個小圓，檢查是否與whatIsGround內指定的圖層物件重疊)
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, whatIsGround);

        if (isGrounded)
        {
            canDoubleJump = true;
        }
        
        //Jump
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {
            Jumping();    
        }
        else if(Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !isGrounded)
        {
            Jumping();
            canDoubleJump = false;
        }
        
        SetAnimatorParameters();
        //角色面向方向調整
        FlipXAxis();
    }

    void Moving()
    {
        //GetAxisRaw會得到0與1的結果，可以解決角色移動後滑行的問題
        horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(moveSpeed * horizontalInput, playerRb.velocity.y);
    }

    void Jumping()
    {
        playerRb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
    }


    //設定動畫參數
    void SetAnimatorParameters()
    {
       

        playerAnim.SetFloat("MovingSpeed", Mathf.Abs(playerRb.velocity.x)); //取速度絕對值讓動畫不被速度正負值方向影響
        playerAnim.SetFloat("FallingSpeed", playerRb.velocity.y);
        playerAnim.SetBool("isGrounded", isGrounded);

    }

    void FlipXAxis()
    {
        if (playerRb.velocity.x < 0.0f)
        {
            playerSpRenderer.flipX = true;
        }
        if (playerRb.velocity.x > 0.0f)
        {
            playerSpRenderer.flipX = false;
        }
    }

}
