using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGamePanel : MonoBehaviour
{
    public Button StartGameBtn;
    // Start is called before the first frame update
    void Start()
    {
        StartGameBtn.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        GameManager.Instance.LoadScene_03_Main();
    }

}
