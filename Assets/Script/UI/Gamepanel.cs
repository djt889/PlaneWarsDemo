using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamepanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    public PlayerControl playerControl;
    public Button restartButton;
    
    void Start()
    {
        gameOverPanel.SetActive(false);
        restartButton.onClick.AddListener(Restart);
    }

    
    void Update()
    {
        if (playerControl.hp <= 0)
        {
            gameOverPanel.SetActive(true);
            playerControl.enabled = false;
            Time.timeScale = 0.3f;
            Invoke("Stoptime",1f);
        }
    }

    public void Restart()
    {
        //restart the game
        CancelInvoke();
        Time.timeScale = 1f;
        GameManager.Instance.LoadScene_02_StartMenu();        
    }

}
