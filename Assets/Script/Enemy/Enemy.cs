using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    //敌人的速度
    private float _speed;
    //敌人的血量
    private float _hp;
    //敌人的攻击力
    private float _attack;
    //敌人的攻击计时器
    private float _attackTimer;
    //玩家对象
    public GameObject player;
    //子弹发射位置
    public GameObject shotPos;
    //子弹对象
    public GameObject bullet;
    //攻击间隔时间
    public float attackInterval = 1f;

    //刚体组件
    private Rigidbody _rigidbody;
    
    
    void Start()
    {
        //初始化敌人的血量、攻击力和速度
        init(200,10,0.5f);
        //获取敌人的刚体组件
        _rigidbody = GetComponent<Rigidbody>();
    }

    //初始化敌人的血量、攻击力和速度
    public void init(float hp,float attack, float speed)
    {
        _hp = hp;
        _attack = attack;
        _speed = speed;
    }

    void Update()
    {
        //移动
        Move();
        //攻击
        Attack();
    }

    //移动
    void Move()
    {
        //敌人面向玩家
        transform.LookAt(player.transform);

        //如果敌人的y坐标大于0，则向下施加力
        if (transform.position.y > 0)
        {
            _rigidbody.AddForce(Vector3.down * 10);
        }
        //如果敌人的y坐标小于0，则向上施加力
        if (transform.position.y < 0)
        {
            _rigidbody.AddForce(Vector3.up * 10);
        }

        //计算敌人与玩家的距离
        float dis = Vector3.Distance(transform.position, player.transform.position);
        //如果距离大于2，则向前移动
        if (dis >= 2)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        //如果距离小于1.9，则向后移动
        if (dis < 1.9f)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * _speed);
        }


    }

    //攻击
    void Attack()
    {
        //增加攻击计时器
        _attackTimer += Time.deltaTime;
        //如果攻击计时器大于攻击间隔时间，则发射子弹
        if (_attackTimer > attackInterval)
        {
            Instantiate(bullet, shotPos.transform.position, Quaternion.Euler(0, shotPos.transform.rotation.eulerAngles.y, 0));
            _attackTimer = 0;
        }
    }
    
    //碰撞检测
    private void OnCollisionEnter(Collision collider)
    {
        //如果碰撞的物体是子弹
        if (collider.gameObject.CompareTag("Bullet"))
        {
            //销毁子弹
            Destroy(collider.gameObject);
            Debug.Log("OnTriggerEnter");
            
            //播放爆炸动画 todoo
            
            //
            
        }
    }



}
