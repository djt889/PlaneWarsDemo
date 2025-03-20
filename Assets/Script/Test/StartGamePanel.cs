using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Button StartGameBtn;
    
    void Start()
    {
        StartGameBtn.onClick.AddListener(StartGameClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameClick()
    {
        GameManager.Instance.LoadScene_03_Main();
    }
}
