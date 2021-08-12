using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampleBox : MonoBehaviour
{
    [SerializeField] private GameObject destroyEffect;


    //������
    public GameObject droppedItem;

    //���w100%�H�����d��
    [Range(0,100f)][SerializeField] private float chanceToDrop;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit the enemy");
            //Ĳ�o�ĤH�����S��
            Instantiate(destroyEffect, other.transform.position, destroyEffect.transform.rotation);
            other.transform.parent.gameObject.SetActive(false);
            AudioManager.instance.PlaySFX(3);

            //�ϼu
            PlayerController.instance.TrampleBounceUp();



            float dropSelect = Random.Range(0, 100f);
            if (dropSelect <= chanceToDrop)
            {
                Instantiate(droppedItem, other.transform.position, droppedItem.transform.rotation);
            }
        } 
    }
}

