using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private readonly float _speed = 50f;
    private float _timer;

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
        _timer += Time.deltaTime;
        if (_timer > 8)
        {
            Destroy(this.gameObject);
        }
        
        transform.position += new Vector3(0, 0, _speed) * Time.deltaTime;
        
        
        
    }

}