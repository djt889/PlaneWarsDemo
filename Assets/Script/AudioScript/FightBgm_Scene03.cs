using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBgm_Scene03 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.audioManager.StopWithFade(0);
        GameManager.Instance.audioManager.PlayWithFade(0, "bgmFight", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
