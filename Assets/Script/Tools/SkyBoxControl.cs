using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxControl : MonoBehaviour
{
    // Start is called before the first frame update

    // 定义旋转角度
    private float rot = 0;
    // 定义旋转速度
    public float rote = 0.7f;
    void Start()
    {
        // 获取天空盒的旋转角度
        rot = RenderSettings.skybox.GetFloat("_Rotation");
    }

    // Update is called once per frame
    void Update()
    {
        // 每帧更新旋转角度
        rot += rote * Time.deltaTime;
        // 限制旋转角度在0-360之间
        rot %= 360;
        // 设置天空盒的旋转角度
        RenderSettings.skybox.SetFloat("_Rotation", rot);
    }
}
