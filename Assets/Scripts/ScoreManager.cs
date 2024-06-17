using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText;
    public TMP_Text clickPerResourceText;
    public TMP_Text autoClickSpeedText;

    private int totalScore;
    public int clickPerResourceLevel;
    public int autoClickSpeedLevel;

    // start num
    private int clickPerResource = 1;
    private int autoClickInterval = 10;

    Data data;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadData();

        StartCoroutine(AutoClickCoroutine());

        UpdateUI();
    }

    private void LoadData()
    {
        data = DataManager.Instance.Load();
        totalScore = data.score;

        clickPerResourceLevel = data.clickPerResourceLevel;
        autoClickSpeedLevel = data.autoClickSpeedLevel;

        // mathf.max () - in 2 or more number -> lagerst return
        autoClickInterval = Mathf.Max(1, 10 - autoClickSpeedLevel);
        clickPerResource = 1 + clickPerResourceLevel;
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = NumberFormatter.FormatNumber(totalScore);
        clickPerResourceText.text = $"Click Per Resource: {clickPerResource}";
        autoClickSpeedText.text = $"Auto Click Speed: {autoClickInterval}s";
    }

    public void UpgradeClickPerResource()
    {
        // pow(x,y) => x power of y
        int cost = (int)Mathf.Pow(10, clickPerResourceLevel + 1);

        if (totalScore >= cost)
        {
            totalScore -= cost;
            clickPerResourceLevel++;
            clickPerResource = 1 + (clickPerResourceLevel* clickPerResourceLevel);
            DataManager.Instance.Save(new Data(totalScore, clickPerResourceLevel, autoClickSpeedLevel));
            UpdateUI();
        }
    }

    public void UpgradeAutoClickSpeed()
    {
       
        int cost = (int)Mathf.Pow(10, autoClickSpeedLevel + 1);

        if (totalScore >= cost && autoClickSpeedLevel < 20)
        {
            totalScore -= cost;
            autoClickSpeedLevel++;
            autoClickInterval = Mathf.Max(1, 10 - autoClickSpeedLevel);
            DataManager.Instance.Save(new Data(totalScore, clickPerResourceLevel, autoClickSpeedLevel));
            UpdateUI();
        }
    }

    public void Click()
    {
        UpdateScore(clickPerResource);
    }

    public void BalloonClick()
    {
        UpdateScore(clickPerResource*10);
    }
       

    private IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoClickInterval);
            UpdateScore(clickPerResource);
        }
    }

    public void Reset()
    {
        totalScore = 0;
        clickPerResourceLevel = 0;
        autoClickSpeedLevel = 0;
        clickPerResource = 1;
        autoClickInterval = 10;
        UpdateUI();
    }
}
