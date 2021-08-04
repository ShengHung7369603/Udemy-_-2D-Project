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

    //�����ʵe
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

        // �ˬd�O�_���|(�HgroundCheckPoint����m�e�@�Ӥp��A�ˬd�O�_�PwhatIsGround�����w���ϼh�����|)
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
        //���⭱�V��V�վ�
        FlipXAxis();
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
