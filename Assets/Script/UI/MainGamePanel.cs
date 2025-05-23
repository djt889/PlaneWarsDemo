using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGamePanel : MonoBehaviour
{
    public Button StartGameBtn;

    public Button leftbutton;
    public Button rightbutton;

    public GameObject[] player;
    public int showindex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartGameBtn.onClick.AddListener(StartGame);
        
        leftbutton.onClick.AddListener(Left);
        rightbutton.onClick.AddListener(Right);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        GameManager.Instance.LoadScene_03_Battle01();
        GameManager.Instance.audioManager.Play(5, "buttonclick", false);
    }

    void Left()
    {
        player[showindex].SetActive(false);
        showindex--;
        if (showindex < 0)
        {
            showindex = player.Length - 1;
            
        }
        player[showindex].SetActive(true);
        GameManager.Instance.audioManager.Play(5, "buttonclick", false);
        GameManager.Instance.playerindex = showindex;
    }


    void Right()
    {
        player[showindex].SetActive(false);
        showindex++;
        if (showindex >= player.Length)
        {
            showindex = 0;
            
        }
        player[showindex].SetActive(true);
        GameManager.Instance.audioManager.Play(5, "buttonclick", false);
        GameManager.Instance.playerindex = showindex;
    }
}
