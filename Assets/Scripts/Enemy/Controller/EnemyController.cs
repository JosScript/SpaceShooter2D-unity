using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] protected int Hp;
    protected Rigidbody2D Rigidbody;

    public PlayerManager player;
    public float speed;
    public ObjectPool<EnemyController> Pool { get; set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void LookAtPlayer()
    {
        var direction = player.transform.position - transform.position;
        var angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    
    private void Move()
    {
        if (!player) return;
        var direction = (player.transform.position - transform.position).normalized * speed;
        var velocity = transform.position + direction * Time.fixedDeltaTime;
        Rigidbody.MovePosition(new Vector2(velocity.x, velocity.y));
        LookAtPlayer();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerBullet") && !other.CompareTag("Player")) return;
        Hp--;
        player.AddScore(100);
        if (Hp <= 0) Pool.Release(this);
    }
    
    private void FixedUpdate()
    {
        Move();
    }
}
