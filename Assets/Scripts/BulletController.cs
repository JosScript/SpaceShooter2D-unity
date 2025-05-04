using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    private Vector3 _direction;

    public ObjectPool<BulletController> Pool { get; set; }
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SetDirection(Vector3.right); // default;
    }

    public void SetDirection(Vector3 direction) => _direction = direction;
    
    // Update is called once per frame
    private void Update()
    {
        var velocity = _direction * speed;
        if (transform.position.x is > -9 and < 9 && transform.position.y is > -6 and < 6)
            transform.Translate(velocity * Time.deltaTime);
        else
            Pool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Bullet")) return;
        Pool.Release(this);
    }
}
