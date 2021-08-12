using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampleBox : MonoBehaviour
{
    [SerializeField] private GameObject destroyEffect;


    //掉落物
    public GameObject droppedItem;

    //測定100%以內的範圍
    [Range(0,100f)][SerializeField] private float chanceToDrop;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit the enemy");
            //觸發敵人消失特效
            Instantiate(destroyEffect, other.transform.position, destroyEffect.transform.rotation);
            other.transform.parent.gameObject.SetActive(false);
            AudioManager.instance.PlaySFX(3);

            //反彈
            PlayerController.instance.TrampleBounceUp();



            float dropSelect = Random.Range(0, 100f);
            if (dropSelect <= chanceToDrop)
            {
                Instantiate(droppedItem, other.transform.position, droppedItem.transform.rotation);
            }
        } 
    }
}

