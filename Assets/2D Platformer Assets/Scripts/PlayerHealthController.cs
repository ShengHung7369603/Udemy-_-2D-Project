using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth, maxHealth;

    private SpriteRenderer theSR;
    private Animator playerAnim;
    private Rigidbody2D playerRb2D;
    [SerializeField] private float invincibleLength;
    private float invincibleCounter;

    //���`�ʵe
    public GameObject deathEffect;

    private void Awake()
    {
        theSR = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerRb2D = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        if(invincibleCounter == 0)
        {
            currentHealth -= 1;
            
            playerAnim.SetBool("isDamaged", true);
            
            UIController.instance.UpdateHeartDisplay();

            AudioManager.instance.PlaySFX(9);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameover�ARespawn Player;
                Instantiate(deathEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();

                AudioManager.instance.PlaySFX(8);

            }
            else
            {
                invincibleCounter = invincibleLength;
                //����z���׭��C��ܵL�Įɶ��}�l
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);
                StartCoroutine(InvincibleCountdown());
            }

            PlayerController.instance.StartCoroutine("KnockBack");
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�L��
        if(other.CompareTag("Instant Death"))
        {
            Debug.Log("Drop to Death");
            currentHealth = 0;
            UIController.instance.UpdateHeartDisplay();
            LevelManager.instance.RespawnPlayer();

            AudioManager.instance.PlaySFX(8);
        }
    }

    //����ˮ`�᪺�L�Įɶ��˼�
    IEnumerator InvincibleCountdown()
    {
        yield return new WaitForSeconds(invincibleCounter);
        //����z���צ^�_��ܵL�Įɶ�����
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1.0f);
        //�L�ĭ˼Ƶ���
        invincibleCounter = 0;
        playerAnim.SetBool("isDamaged", false);
    }


    //�ɦ�A�̦h���
    public void HealPlayer()
    {
        if (currentHealth != maxHealth)
        {
            if(currentHealth == 5)
            {
                currentHealth += 1;
            }
            else
            {
                currentHealth += 2;
            }
        }
        //��s���
        UIController.instance.UpdateHeartDisplay();
    }
}
