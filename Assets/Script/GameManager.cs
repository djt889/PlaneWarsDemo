using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1f;
    }

    void Start()
    {
        
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Init()
    {
        //初始化游戏
        
    }

    #region 场景加载
        public void LoadScene_01_StartGame()
        {
            SceneManager.LoadScene("_01_StartGame");
        }
        
        public void LoadScene_02_StartMenu()
        {
            SceneManager.LoadScene("_02_StartMenu");
        }
        
        public void LoadScene_03_Main()
        {
            SceneManager.LoadScene("_03_Battle01");
        }
        
    #endregion

    
}
