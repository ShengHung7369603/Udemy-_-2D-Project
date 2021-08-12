using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance;
    [SerializeField]private CheckPoint[] checkPoints;

    public Vector3 spawnPos;

    private void Awake()
    {
        instance = this;
        //�NSCENE�̭�ACTIVE�B�ŦX���󪺪���Ԫ�ARRAY��
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    private void Start()
    {
        //��l��PLAYER�����I
        spawnPos = GameObject.Find("Player").transform.position;
    }

    //Deactivate�Ҧ���CheckPoints
    public void DeactivateCheckPoints()
    {
        for(int i=0; i<checkPoints.Length ; i++)
        {
            checkPoints[i].ResetCheckPoint();
        }
    }

    public void SetSpawnPosition(Vector3 newSpawnPos )
    {
        spawnPos = newSpawnPos;
    }
}
