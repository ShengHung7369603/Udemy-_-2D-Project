using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //public static CheckPoint instance;
    [SerializeField] Sprite CheckPointOn, CheckPointOff;
    private SpriteRenderer theSR;

    private void Awake()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //�����Ҧ���CheckPoint
            CheckPointController.instance.DeactivateCheckPoints();
            //�}��Trigger��CheckPoint
            theSR.sprite = CheckPointOn;

            //�]�m�s�������I
            CheckPointController.instance.SetSpawnPosition(gameObject.transform.position);
        }
    }

    public void ResetCheckPoint()
    {
        theSR.sprite = CheckPointOff;
    }

}
