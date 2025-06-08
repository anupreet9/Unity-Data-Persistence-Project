using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance;
    public TMP_InputField NameInputField;
    public string PlayerName = "";
    public string BestPlayerName;
    public int BestScore = 0;

    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void StartGame()
    {
        PlayerName = NameInputField.text;
        SceneManager.LoadScene(1);
        LoadBestScoreAndName();
    }

    public void SaveBestScoreAndName(int score)
    {
        SaveData saveData = new SaveData();
        saveData.bestPlayerName = PlayerName;
        saveData.bestScore = score;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScoreAndName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPlayerName = data.bestPlayerName;
            BestScore = data.bestScore;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}