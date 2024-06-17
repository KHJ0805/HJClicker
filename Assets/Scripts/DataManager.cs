using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data
{
    public int score;
    public int clickPerResourceLevel;
    public int autoClickSpeedLevel;

    public Data(int score = 0, int clickPerResourceLevel = 0, int autoClickSpeedLevel = 0)
    {
        this.score = score;
        this.clickPerResourceLevel = clickPerResourceLevel;
        this.autoClickSpeedLevel = autoClickSpeedLevel;
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private string path;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        path = Application.dataPath + "/save.json";
    }

    public void Save(Data data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public Data Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<Data>(json);
        }
        else
        {
            return new Data();
        }
    }
}
