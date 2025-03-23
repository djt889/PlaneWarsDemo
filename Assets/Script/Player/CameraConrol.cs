using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConrol : MonoBehaviour
{
    // Start is called before the first frame update
    // Player玩家
    public GameObject player;
    public float speed = 5f;

    // 相机旋转角度
    private readonly Vector3[] _cameraRotations = 
    {
        new Vector3(20,0,0),
        new Vector3(90,0,0)
    };

    // 相机位置偏移量
    private Vector3 _offset;

    private int state = 0;
    // 相机位置偏移量数组
    private readonly Vector3[] _offsets = 
    {
        new Vector3(0,3f,-5f),
        new Vector3(0,10f,1.7f)
    };

    void Start()
    {
         // 初始化相机位置偏移量
         _offset = transform.position - player.transform.position;
         // 将相机位置偏移量中的x和z值设置为0
         _offset = new Vector3(0,_offset.y,_offset.z);
         // 打印相机位置偏移量
         ChangeView(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeView(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeView(1);
        }

        if (state == 0)
        {
            // 计算相机跟随的目标位置
            Vector3 targetPosition = player.transform.position + _offset; // 相机跟随
            // 平滑移动相机位置
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }

        if (state == 1)
        {
            Vector3 targetPosition =  _offset; 
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
            if (Vector3.Distance(targetPosition,transform.position) < 0.1f)
            { 
                state = -1;
            }
        }
        
        
    }

    // 根据传入的索引值，改变视图
    private void ChangeView(int index)
    {
        // 将_offset的值设置为_offsets数组中对应索引的值
        _offset = _offsets[index];
        // 将Camera.main的transform的eulerAngles属性设置为_cameraRotations数组中对应索引的值
        Camera.main.transform.eulerAngles = _cameraRotations[index];
        state = index;
    }


}
