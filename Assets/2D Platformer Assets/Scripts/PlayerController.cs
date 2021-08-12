using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //角色移動
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D playerRb;
    private float horizontalInput;

    //跳躍
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private bool canDoubleJump;

    //控制角色動畫
    private Animator playerAnim;
    private SpriteRenderer playerSpRenderer;

    //受傷後退
    [SerializeField]private float KnockBackLength, KnockBackForce;
    private float KnockBackCounter;
    private bool isKnockedBack = false;

    //踩敵人反彈
    public float bounceForce;

    private void Awake()
    {
        instance = this;
    }
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
        if (!isKnockedBack)
        {
            Moving();

            // 檢查是否重疊(以groundCheckPoint的位置畫一個小圓，檢查是否與whatIsGround內指定的圖層物件重疊)
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, whatIsGround);

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jumping();

                AudioManager.instance.PlaySFX(10);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !isGrounded)
            {
                Jumping();
                canDoubleJump = false;

                AudioManager.instance.PlaySFX(10);
            }

            SetAnimatorParameters();
            //角色面向方向調整
            FlipXAxis();
        }
        
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


    //調整面向方向
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

    public IEnumerator KnockBack()
    {
        isKnockedBack = true;
        playerRb.velocity = new Vector2(0f, 0f);
        //設定後退時間
        KnockBackCounter = KnockBackLength;

        //施加後退位移，並調整後退方向
        if (playerSpRenderer.flipX)
        {
            playerRb.velocity = new Vector2(KnockBackForce, 0);
        }
        else
        {
            playerRb.velocity = new Vector2(-KnockBackForce, 0);
        }
        
        
        
        yield return new WaitForSeconds(KnockBackCounter);
        //歸零後退計時
        KnockBackCounter = 0; 
        isKnockedBack = false;
    }

    public void TrampleBounceUp()
    {
        playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }
}
