using System;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public RaySixDirCollision RaySixDirCollision;
    private PlayerControl _playerControl;
    
    //敌人的移动速度
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
    public GameObject[] shotPoss;
    //子弹对象
    public GameObject[] bullets;
    //攻击间隔时间
    
    public float attackInterval;
    //刚体组件
    private Rigidbody _rigidbody;
    public GameObject explosionPrefab;
    

    public void Start()
    {
        _playerControl = GameManager.Instance.GetGenPlayer().GetComponent<PlayerControl>();
        //获取敌人的刚体组件
        _rigidbody = GetComponent<Rigidbody>();
        RaySixDirCollision = new RaySixDirCollision(~(1 << 9));
        RaySixDirCollision.AddRayLayer(Vector3.zero,2.5f,Color.red);
        RaySixDirCollision.AddRayLayer(new Vector3(0,0,1),2.5f,Color.green);
        var bulletProject = bullets[0].GetComponent<EnemyBullet>();
        attackInterval = bulletProject.fireRate;
    }

    public void Init(float hp,float attack, float speed)
    {
        _hp = hp;
        _attack = attack;
        _speed = speed;
    }
    
    private bool _isDie ;
    private readonly float _timer = 3;
    void Update()
    {
        
        if (_hp<= 0 && !_isDie)
        {
            Die();
        }
        
        if (!_isDie)
        {
            
            //移动
            Move();
            //攻击
            if (_playerControl.isplayerDead == false)
            {
                Attack();
            }
            RayCheck();
            
        }
        
    }

    //移动
    void Move()
    {
        transform.eulerAngles = new Vector3(-_rigidbody.velocity.z * 3, transform.eulerAngles.y, _rigidbody.velocity.x * 10);
        
        // 敌人面向玩家
        Vector3 directionToPlayer = player.transform.position - transform.position;
        if (directionToPlayer.sqrMagnitude > 0.01f) // 避免零向量
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1f);
        }

        
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
        if (dis >= 20f)
        {
            gameObject.transform.Translate(Vector3.forward * 4f * Time.deltaTime);
        }
        
        if (dis >= 7f && dis < 20f)
        {
            gameObject.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        //如果距离小于1.9，则向后移动
        if (dis < 7f)
        {
            gameObject.transform.Translate(Vector3.back * _speed * Time.deltaTime);
        }


    }

    //攻击
    void Attack()
    {
        
        // 计算敌人与玩家之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // 增加攻击计时器
        _attackTimer += Time.deltaTime;

        // 如果距离小于设定的阈值，并且攻击计时器超过间隔时间，则发射子弹
        if (distanceToPlayer < 10f && _attackTimer > attackInterval)
        {
            Shoot();
            _attackTimer = 0;
        }
        
    }

    public virtual void Shoot()
    {
        int index = 0;
        Instantiate(bullets[index], shotPoss[index].transform.position, Quaternion.Euler(0, shotPoss[index].transform.rotation.eulerAngles.y, 0));
        GameManager.Instance.audioManager.Play(2, "seenemyfx14Shoot", false);
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
    // 在 Enemy.cs 中添加以下方法
    public void TakeDamage(float amount)
    {
        _hp -= amount;
        Debug.Log($"Enemy HP: {_hp}");

        if (_hp <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        _isDie = true; // 防止重复触发
        GameObject expobj = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        GameManager.Instance.audioManager.Play(3, "explode", false);
        
        // 设置一个初始的旋转角速度
        _rigidbody.angularVelocity = new Vector3(0, 0, 5f); // 绕Z轴旋转

        // 启用重力，让物体自然下落
        _rigidbody.useGravity = true; 

        // 关闭碰撞器，防止死亡后继续碰撞
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Destroy(gameObject,_timer); 
        Destroy(expobj, _timer);
        
    }







}
