using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private float respawnCounter;


    [SerializeField] private string levelToLoad;

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
        //���⦺�`
        PlayerHealthController.instance.gameObject.SetActive(false);
        //�µe��
        UIController.instance.ShouldFadeToBlack();
        yield return new WaitForSeconds(respawnCounter);

        //�µe���^�_
        UIController.instance.ShouldFadeFromBlack();

        PlayerHealthController.instance.gameObject.transform.position = CheckPointController.instance.spawnPos;
        PlayerHealthController.instance.gameObject.SetActive(true);
        //���]��q�P���
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHeartDisplay();
    }

    //���d�q��
    public void EndLevel()
    {
        StartCoroutine(EndLevelCoroutine());
    }

    private IEnumerator EndLevelCoroutine()
    {
        PlayerController.instance.isComplete = true;
        UIController.instance.levelEndText.gameObject.SetActive(true);
        AudioManager.instance.bgm.Stop();
        AudioManager.instance.victoryMusic.Play();

        yield return new WaitForSeconds(2);
        UIController.instance.ShouldFadeToBlack();

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelToLoad);

    }
}
