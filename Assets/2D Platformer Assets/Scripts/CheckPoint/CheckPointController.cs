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
        //將SCENE裡面ACTIVE且符合條件的物件拉近ARRAY中
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    private void Start()
    {
        //初始化PLAYER重生點
        spawnPos = GameObject.Find("Player").transform.position;
    }

    //Deactivate所有的CheckPoints
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
