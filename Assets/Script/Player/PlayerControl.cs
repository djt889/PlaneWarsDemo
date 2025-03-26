using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 定义飞机的速度
    private readonly float _speed = 4f;
    public GameObject bulletPrefab;
    public GameObject shotPos;
    private float _jtimer = 0;
    private float _ktimer = 0;
    private float _fireFrequency = 0.3f;

    private void Start()
    {
    }

    private void Update()
    {
        Fire();
        Move();
    }

    private void Move()
    {
        // 获取水平和垂直方向的输入
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        // 如果有输入
        if (h != 0 || v != 0)
        {
            // 飞机移动
            transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * _speed, Space.World);

            // 飞机摇摆
            transform.eulerAngles = new Vector3(v * 15, 0, h * -30);
        }
    }

    // 射击
    private void Fire()
    {
        // 发射子弹
        KeyInvoke(KeyCode.J, 
            _fireFrequency,
            ref _jtimer,
            () =>
            {
                Instantiate(bulletPrefab, shotPos.transform.position, Quaternion.identity);
            }, 
            null);
        
        KeyInvoke(KeyCode.K, 
            _fireFrequency,
            ref _ktimer,
            () =>
            {
                Instantiate(bulletPrefab, shotPos.transform.position + new Vector3(1,0,0), Quaternion.identity);
                Instantiate(bulletPrefab, shotPos.transform.position + new Vector3(-1,0,0), Quaternion.identity);
            }, 
            null);
        
    }


    private void KeyInvoke(KeyCode keyCode, float invoke,ref float invokeTimer, Action action, Action actionUp)
    {
        if (Input.GetKey(keyCode))
        {
            invokeTimer += Time.deltaTime;
            if (invokeTimer >= invoke)
            {
                action();
                invokeTimer = 0;
            }
        }

        if (Input.GetKeyUp(keyCode))
        {
            invokeTimer = _fireFrequency;
            if (actionUp != null)
            {
                actionUp();
            }
        }
        
    }
}