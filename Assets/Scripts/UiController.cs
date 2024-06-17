using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    public TMP_Text clickPerResourceCostText;
    public TMP_Text autoClickSpeedCostText;
    public GameObject shopPanel;
    public GameObject clickParticleSystem;
    public GameObject MainBtn;

    public AudioSource clickAudio;
    public AudioSource bgmMusic;

    private int clickCount = 0;
    private int maxClickCount = 10;
    private float rotationSpeed = 0f;
    private float rotationAcceleeration = 30f;
    private float timeOfNoTouch = 0f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCosts();

        clickAudio = gameObject.GetComponent<AudioSource>();
        bgmMusic.Play();
    }

    void Update()
    {
        RollingBtn();
        NoTouchRotationReduce();
    }

    public void OnClickMain()
    {
        ScoreManager.instance.Click();
        clickAudio.Play();
        SpawnClickParticles();
        RollingBtn();

        if(clickCount < maxClickCount)
        {
            clickCount++;
            rotationSpeed += rotationAcceleeration;
        }
        timeOfNoTouch = 0f;
    }

    public void ResourceUp()
    {
        ScoreManager.instance.UpgradeClickPerResource();
        UpdateCosts();
    }

    public void ResourceSpeed()
    {
        ScoreManager.instance.UpgradeAutoClickSpeed();
        UpdateCosts();
    }

    private void UpdateCosts()
    {
        clickPerResourceCostText.text = $"Cost: {NumberFormatter.FormatNumber((int)Mathf.Pow(10, ScoreManager.instance.clickPerResourceLevel + 1))}";
        autoClickSpeedCostText.text = $"Cost: {NumberFormatter.FormatNumber((int)Mathf.Pow(10, ScoreManager.instance.autoClickSpeedLevel + 1))}";
    }

    public void OnClickShop()
    {
        UpdateCosts();
        shopPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnClickShopOff()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ResetBtn()
    {
        ScoreManager.instance.Reset();
    }

    private void SpawnClickParticles()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject particleInstance = Instantiate(clickParticleSystem, MousePos, Quaternion.identity);
       
        ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();

        Destroy(particleInstance, 2.0f);

    }

    private void RollingBtn()
    {
        MainBtn.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void NoTouchRotationReduce()
    {
        timeOfNoTouch += Time.deltaTime;

        if (timeOfNoTouch >= 1.0f)
        {
            if (clickCount > 0)
            {
                clickCount--;
                rotationSpeed -= rotationAcceleeration;
            }

            timeOfNoTouch = 0f;
        }

        if (rotationSpeed < 0)
        {
            rotationSpeed = 0;
        }
    }
}
