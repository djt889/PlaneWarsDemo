using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Button LoginGameBtn;
    
    void Start()
    {
        LoginGameBtn.onClick.AddListener(LoginGameClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginGameClick()
    {
        GameManager.Instance.LoadScene_01_StartGame();
        GameManager.Instance.audioManager.Play(5, "buttonclick", false);
    }
}
