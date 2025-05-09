using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 50f; // 子弹的速度
    private float _timer; // 计时器
    public float attack;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        _timer += Time.deltaTime; // 每帧增加时间
        if (_timer > 8) // 如果时间超过8秒
        {
            Destroy(this.gameObject); // 销毁子弹
        }

        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime); // 子弹向前移动

    }

    

}