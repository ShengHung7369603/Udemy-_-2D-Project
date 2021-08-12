using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private float respawnCounter;

    private void Awake()
    {
        instance = this;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCountDown());
    }

    private IEnumerator RespawnCountDown()
    {
        PlayerHealthController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnCounter);

        PlayerHealthController.instance.gameObject.transform.position = CheckPointController.instance.spawnPos;
        PlayerHealthController.instance.gameObject.SetActive(true);
        //重設血量與顯示
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHeartDisplay();


    }
}
