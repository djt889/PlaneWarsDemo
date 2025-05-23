using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeBgm_Scene01 : MonoBehaviour
{

    void Start()
    {
        GameManager.Instance.audioManager.StopWithFade(0);
        GameManager.Instance.audioManager.PlayWithFade(0, "bgmWelcome", true);
    }


    void Update()
    {
        
    }
}
