using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    private float _speed;
    private float _hp;
    private float _attack;
    private float _attackTimer;
    public GameObject shotPos;
    public GameObject bullet;
    
    void Start()
    {
        init(200,10,0);
    }

    public void init(float hp,float attack, float speed)
    {
        _hp = hp;
        _attack = attack;
        _speed = speed;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.back * Time.deltaTime * _speed);
    }

    void Attack()
    {
        
    }
    
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
            Debug.Log("OnTriggerEnter");
            
            //播放爆炸动画 todoo
            
            //
            
        }
    }



}
