using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float hp = 100;
    // 定义子弹的预制体
    public GameObject bulletPrefab;

    // 定义子弹发射的位置
    public GameObject shotPos;

    // 定义飞机的速度
    private readonly float _speed = 5f;

    // 定义J键和K键的按键间隔
    private readonly KeyInterval _jkey = new(KeyCode.J, 0.2f);
    private readonly KeyInterval _kkey = new(KeyCode.K, 0.2f);


    private void Start()
    {
    }

    private void Update()
    {
        // 调用射击和移动方法
        Fire();
        Move();
    }

    // 移动飞机
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
        _jkey.KeyInvoke(
            () => { Instantiate(bulletPrefab, shotPos.transform.position, Quaternion.identity); },
            null);

        _kkey.KeyInvoke(
            () =>
            {
                Shrapnel();
            },
            null);
    }

    private int _bulletnum = 4;
    private float _angle = 50;

    private void Shrapnel()
    {
        float inteal = _angle / _bulletnum;
        
        for (float i = -_angle / 2 ; i <= _angle / 2 ; i += inteal)
        {
            Instantiate(bulletPrefab, shotPos.transform.position, Quaternion.Euler(0, i, 0));
        }
    }
    
    private void OnCollisionEnter(Collision collider)
    {
        //如果碰撞的物体是敌人子弹
        if (collider.gameObject.CompareTag("EnemyBullet"))
        {
            //销毁子弹
            Destroy(collider.gameObject);
            
            //计算伤害
            hp -= collider.gameObject.GetComponent<Bullet>().damage;
            //
            Debug.Log(hp);
            
        }
    }

}