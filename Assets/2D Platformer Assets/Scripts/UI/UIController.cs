using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private Image heart1, heart2, heart3;
    [SerializeField] private Sprite heartFull,heartHalf, heartEmpty;

    //�Ȱ�
    public bool isPause = false;
    [SerializeField]private GameObject pauseScene;

    //���`��µe��
    [SerializeField]private Image fadeScreen;
    [SerializeField] private float fadeSpeed;
    [SerializeField]private bool shouldFadeToBlack, shouldFadeFromBlack;

    //GEM �`��
    public int gemAmount;
    public Text GemCollectedTxt;

    //�q���S��
    public Text levelEndText;

    private void Awake()
    {
        instance = this;

        fadeScreen.gameObject.SetActive(true);
        
    }

    private void Start()
    {
        gemAmount = 0;
        GemCollectedTxt.text = gemAmount.ToString();

        //�}�l�C����e���H�J
        ShouldFadeFromBlack();
    }

    //����Ȱ��B�~��B�[�t�A���`�µe��
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isPause)
            {
                Time.timeScale = 0;
                isPause = true;
                pauseScene.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                isPause = false;
                pauseScene.SetActive(false);
            }  
        }
        if (Input.GetKey(KeyCode.Z)&&!isPause)
        {
            Time.timeScale = 2;
        }

        //���`�µe��
        if (shouldFadeToBlack)
        {
            //mathf.MoveTowards(�_�l�A�ؼСA���ܳt��) �q�_�l�ȫ�����ܪ���F��ؼЭ�
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void UpdateHeartDisplay()
    {
        switch (PlayerHealthController.instance.currentHealth)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    public void UpdateGemDisplay()
    {
        gemAmount++;
        GemCollectedTxt.text = gemAmount.ToString();
    }

   public void ShouldFadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void ShouldFadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
