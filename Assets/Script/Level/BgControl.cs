using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgControl : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] bgs;
    public float speed = 3f;
    public float bgSize;
    private float _maxSize = 0;
    
    void Start()
    {
        // 计算背景的最大尺寸
        _maxSize = bgSize * bgs.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // 调用背景移动函数
        BgMove();
    }

    // 移动背景,背景有顺序
    private void BgMove(float dir = -1)
    {
        // 遍历所有背景
        for (int i = 0; i < bgs.Length; i++)
        {
            // 移动背景
            bgs[i].transform.Translate( new Vector3(0,0, dir * speed * Time.deltaTime));
            // 如果背景移动到边界，则将其位置重置到另一边
            if (bgs[i].transform.position.z <= dir * bgSize)
            {
                bgs[i].transform.position = bgs[i].transform.position + new Vector3(0,0, -dir * _maxSize);
            }
        }
    }

}
