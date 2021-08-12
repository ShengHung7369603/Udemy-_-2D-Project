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
            //關閉所有的CheckPoint
            CheckPointController.instance.DeactivateCheckPoints();
            //開啟Trigger的CheckPoint
            theSR.sprite = CheckPointOn;

            //設置新的重生點
            CheckPointController.instance.SetSpawnPosition(gameObject.transform.position);
        }
    }

    public void ResetCheckPoint()
    {
        theSR.sprite = CheckPointOff;
    }

}
