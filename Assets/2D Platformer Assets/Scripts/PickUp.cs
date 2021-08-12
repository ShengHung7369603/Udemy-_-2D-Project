using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private bool isGem , isCherry;
    
    private bool isCollected;

    public GameObject collectEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            //�����Y���p�۪��ƶq
            if (isGem)
            {
                UIController.instance.UpdateGemDisplay();
                Destroy(gameObject);

                AudioManager.instance.PlaySFX(6);
            }
            //���Y��i�H�ɦ�
            if (isCherry)
            {
                PlayerHealthController.instance.HealPlayer();
                Destroy(gameObject);

                AudioManager.instance.PlaySFX(7);
            }

            Instantiate(collectEffect, gameObject.transform.position, collectEffect.transform.rotation);
            isCollected = true;
        }
    }

 
}
