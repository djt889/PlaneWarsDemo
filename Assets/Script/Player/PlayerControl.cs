using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float hp = 100;
    // 定义子弹的预制体
    public GameObject bulletPrefab;

    // 定义子弹发射的位置
    public GameObject[] shotPoss;

    // 定义飞机的速度
    private readonly float _speed = 5f;

    public bool isplayerDead = false;
    
    // 定义J键和K键的按键间隔
    private KeyInterval _jkey;
    private KeyInterval _kkey;     

    //射击频率
    private float _fireRate;

    public void Start()
    {
        var bulletProject = bulletPrefab.GetComponent<Bullet>();
        if (bulletPrefab != null)
        {
            _fireRate = bulletProject.GetComponent<Bullet>().fireRate;
        }else
        {
            Debug.Log("子弹预制体为空");
            _fireRate = 0.2f;
        }
        _jkey = new KeyInterval(KeyCode.J, _fireRate);
        _kkey = new KeyInterval(KeyCode.K, _fireRate);
    }

    public void Update()
    {
        // 调用射击和移动方法
        Fire();
        Move();
    }

    // 移动飞机
    public void Move()
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
    public virtual void Fire()
    {
        
        // 定义子弹发射的位置
        // 发射子弹
        _jkey.KeyInvoke(
            () => {
                for (int i = 0; i < shotPoss.Length; i++)
                {
                    Instantiate(bulletPrefab, shotPoss[i].transform.position, Quaternion.identity);
                    PlayShootSound();
                }
            },
            null);

        _kkey.KeyInvoke(
            () =>
            {
                for (int i = 0; i < shotPoss.Length; i++)
                {
                    Shrapnel(i);
                }
            },
            null);
    }

    private int _bulletnum = 4;
    private float _angle = 50;

    public virtual void Shrapnel(int index)
    {
        float inteal = _angle / _bulletnum;
        
        for (float i = -_angle / 2 ; i <= _angle / 2 ; i += inteal)
        {
            Instantiate(bulletPrefab, shotPoss[index].transform.position, Quaternion.Euler(0, i, 0));
        }

        PlayShootSound();
    }
    
    private void PlayShootSound()
    {
        AudioManager.Instance.Play(1, "seplayerfx01Shoot", false);
    }
    
    private void OnCollisionEnter(Collision collider)
    {
        //如果碰撞的物体是敌人子弹
        if (collider.gameObject.CompareTag("EnemyBullet"))
        {
            //销毁子弹
            Destroy(collider.gameObject);
            
            //计算伤害
            hp -= collider.gameObject.GetComponent<EnemyBullet>().damage;
            //
            Debug.Log(hp);
            
        }
    }

}