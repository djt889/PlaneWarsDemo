using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public RaySixDirCollision RaySixDirCollision;
    //敌人的速度
    private float _speed = 1f;
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
        RaySixDirCollision = new RaySixDirCollision(~(1 << 9));
        RaySixDirCollision.AddRayLayer(Vector3.zero,2.5f,Color.red);
        RaySixDirCollision.AddRayLayer(new Vector3(0,0,1),2.5f,Color.green);
        
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
        RayCheck();
        
    }

    //移动
    void Move()
    {
        transform.eulerAngles = new Vector3(-_rigidbody.velocity.z * 3, transform.eulerAngles.y, _rigidbody.velocity.x * 10);
        
        //敌人面向玩家
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(player.transform.position - transform.position),Time.deltaTime * 1f);
        
        //如果敌人的y坐标大于0，则向下施加力
        if (transform.position.y > 0)
        {
            _rigidbody.AddForce(Vector3.down * 3f);
        }
        //如果敌人的y坐标小于0，则向上施加力
        if (transform.position.y < 0)
        {  
            _rigidbody.AddForce(Vector3.up * 3f);
        }

        //计算敌人与玩家的距离
        float dis = Vector3.Distance(transform.position, player.transform.position);
        //如果距离大于2，则向前移动
        if (dis >= 7f)
        {
            _rigidbody.AddRelativeForce(Vector3.forward * _speed); 
        }

        //如果距离小于1.9，则向后移动
        if (dis < 6.9f)
        {
            _rigidbody.AddRelativeForce(Vector3.back * _speed);
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

    void RayCheck()
    {
        //检测前方是否有障碍物
        RaySixDirCollision.SixRaycast((DRI dRi, RaycastHit hit) =>
        {
            switch (dRi)
            {
                case DRI.FORNT:
                    _rigidbody.AddRelativeForce(Vector3.right * 1f);
                    break;
                case DRI.LEFT:
                    _rigidbody.AddRelativeForce(Vector3.right * 1f);
                    break;
                case DRI.RIGHT:
                    _rigidbody.AddRelativeForce(Vector3.left * 1f);
                    break;
            }
            
            
        },null);
        RaySixDirCollision.RaySixDirCollisionUpdate(transform);
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
            _hp -= 1;
            //
            
        }
    }
    
    



}
