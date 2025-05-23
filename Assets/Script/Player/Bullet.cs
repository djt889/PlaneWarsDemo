using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 从 ProjectileMove 继承的功能
    public float speed = 50f;                   // 子弹速度
    public float fireRate = 0.2f;               // 发射频率（可配置）
    public GameObject hitPrefab;                // 命中特效

    // 新增：从原 Bullet.cs 中的属性
    public float damage = 20f;                  // 子弹造成的伤害
    private float _timer;                       // 计时器
    private readonly float _destroyTime = 8f;              // 自动销毁时间

    private void Start()
    {
        _timer = 0f;
    }

    private void Update()
    {
        Move();
        CheckDestroyTimer();
    }

    private void Move()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void CheckDestroyTimer()
    {
        _timer += Time.deltaTime;
        if (_timer >= _destroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.CompareTag("Enemy"))
        {
            speed = 0;

            ContactPoint contact = co.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rot);
                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }
            
            Enemy enemy = co.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // 新增：调用敌人受伤方法
                GameManager.Instance.audioManager.Play(4,"seenemyhit",false);
            }
            
            Destroy(gameObject);
        }
    }
}
