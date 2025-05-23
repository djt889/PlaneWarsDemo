using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject audioManagerPrefab;
    public SceneFader sceneFader; // 拖入 SceneFader 实例
    public GameObject[] player;
    
    public AudioManager audioManager;

    public int playerindex;
    
    
    
    public GameObject playered;
    public void InstantiatePlayer()
    {
        playered = Instantiate(player[playerindex], new Vector3(0, 0, 0), Quaternion.identity);
    }

    public GameObject GetGenPlayer()
    {
        return playered;
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (audioManagerPrefab != null)
            {
                var existing = FindObjectOfType<AudioManager>();
                if (existing == null)
                {
                    audioManager = Instantiate(audioManagerPrefab).GetComponent<AudioManager>();
                    Debug.Log("✅ AudioManager instantiated.");
                }
                else
                {
                    audioManager = existing;
                    Debug.Log("🔁 Reusing existing AudioManager.");
                }
            }
            else
            {
                Debug.LogError("❌ audioManagerPrefab is not assigned!");
            }

            // 初始化时间刻度
            Time.timeScale = 1f;

            // 监听场景加载完成
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        Debug.Log($"[GameManager] Time.timeScale restored to 1 after loading {scene.name}");

        if (scene.name == "_03_Battle01")
        {
            InstantiatePlayer();
        }

    }


    public void LoadScene_01_StartGame()
    {
        sceneFader.FadeToScene("_01_StartGame");
    }

    public void LoadScene_02_StartMenu()
    {
        sceneFader.FadeToScene("_02_StartMenu");
    }

    public void LoadScene_03_Battle01()
    {
        sceneFader.FadeToScene("_03_Battle01");
    }
    

}
