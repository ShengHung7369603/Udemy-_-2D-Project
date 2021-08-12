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

    //死亡動畫
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
                //gameover，Respawn Player;
                Instantiate(deathEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();

                AudioManager.instance.PlaySFX(8);

            }
            else
            {
                invincibleCounter = invincibleLength;
                //角色透明度降低表示無敵時間開始
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);
                StartCoroutine(InvincibleCountdown());
            }

            PlayerController.instance.StartCoroutine("KnockBack");
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //摔死
        if(other.CompareTag("Instant Death"))
        {
            Debug.Log("Drop to Death");
            currentHealth = 0;
            UIController.instance.UpdateHeartDisplay();
            LevelManager.instance.RespawnPlayer();

            AudioManager.instance.PlaySFX(8);
        }
    }

    //受到傷害後的無敵時間倒數
    IEnumerator InvincibleCountdown()
    {
        yield return new WaitForSeconds(invincibleCounter);
        //角色透明度回復表示無敵時間結束
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1.0f);
        //無敵倒數結束
        invincibleCounter = 0;
        playerAnim.SetBool("isDamaged", false);
    }


    //補血，最多兩血
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
        //更新血條
        UIController.instance.UpdateHeartDisplay();
    }
}
