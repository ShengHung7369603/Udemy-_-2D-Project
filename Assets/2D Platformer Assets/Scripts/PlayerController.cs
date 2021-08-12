using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //���Ⲿ��
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D playerRb;
    private float horizontalInput;

    //���D
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private bool canDoubleJump;

    //�����ʵe
    private Animator playerAnim;
    private SpriteRenderer playerSpRenderer;

    //���˫�h
    [SerializeField]private float KnockBackLength, KnockBackForce;
    private float KnockBackCounter;
    private bool isKnockedBack = false;

    //��ĤH�ϼu
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

            // �ˬd�O�_���|(�HgroundCheckPoint����m�e�@�Ӥp��A�ˬd�O�_�PwhatIsGround�����w���ϼh�����|)
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
            //���⭱�V��V�վ�
            FlipXAxis();
        }
        
    }

    void Moving()
    {
        //GetAxisRaw�|�o��0�P1�����G�A�i�H�ѨM���Ⲿ�ʫ�Ʀ檺���D
        horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(moveSpeed * horizontalInput, playerRb.velocity.y);
    }

    void Jumping()
    {
        playerRb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
    }


    //�]�w�ʵe�Ѽ�
    void SetAnimatorParameters()
    {
       

        playerAnim.SetFloat("MovingSpeed", Mathf.Abs(playerRb.velocity.x)); //���t�׵�������ʵe���Q�t�ץ��t�Ȥ�V�v�T
        playerAnim.SetFloat("FallingSpeed", playerRb.velocity.y);
        playerAnim.SetBool("isGrounded", isGrounded);

    }


    //�վ㭱�V��V
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
        //�]�w��h�ɶ�
        KnockBackCounter = KnockBackLength;

        //�I�[��h�첾�A�ýվ��h��V
        if (playerSpRenderer.flipX)
        {
            playerRb.velocity = new Vector2(KnockBackForce, 0);
        }
        else
        {
            playerRb.velocity = new Vector2(-KnockBackForce, 0);
        }
        
        
        
        yield return new WaitForSeconds(KnockBackCounter);
        //�k�s��h�p��
        KnockBackCounter = 0; 
        isKnockedBack = false;
    }

    public void TrampleBounceUp()
    {
        playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }
}
