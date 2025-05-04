using System.Collections;
using UnityEngine;

public class BossController : EnemyController
{
    private readonly Vector3 _targetPosition = new(7f, 0f, 0f);
    [SerializeField] private Transform spawnPoint;
    private BulletsController _bulletsController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool _isVulnerable;
    
    public bool IsReady => Vector3.Distance(transform.position, _targetPosition) <= 1e-1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _bulletsController = GetComponent<BulletsController>();
        StartCoroutine(OnFire());
    }

    public bool IsBossDead() => Hp <= 0;
    
    private void Move()
    {
        if (Vector3.Distance(transform.position, _targetPosition) <= 1e-1) return;
        var direction = (_targetPosition - transform.position).normalized * speed;
        var velocity = transform.position + direction * Time.fixedDeltaTime;
        Rigidbody.MovePosition(new Vector2(velocity.x, velocity.y));
    }
    
    private void PointToPlayer(BulletController bulletController)
    {
        var direction = player.transform.position - bulletController.transform.position;
        var angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        bulletController.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    
    private IEnumerator OnFire()
    {
        yield return new WaitUntil(() => IsReady);
        _isVulnerable = true;
        var bullet = _bulletsController.GetBulletPool().Get();
        bullet.transform.position = spawnPoint.position;
        PointToPlayer(bullet);
        bullet.SetDirection(Vector3.left);
        yield return new WaitForSeconds(3f);
        if (!IsBossDead()) yield return OnFire();
        yield return null;
    }
    
    public IEnumerator OnTakeDamage()
    {
        var originalColor = spriteRenderer.color;
        _isVulnerable = false;
        yield return OnDamage(originalColor, Color.red, 0.2f);
        yield return OnDamage(Color.red, originalColor, 0.2f);
        _isVulnerable = true;
    }

    private IEnumerator OnDamage(Color startColor, Color endColor, float duration)
    {
        var elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = elapsedTime / duration;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        spriteRenderer.color = endColor;
    }

    private void TakeDamage(int damage)
    {
        if (!_isVulnerable) return;
        if (Hp - damage <= 0)
            Hp = 0;
        else
            Hp -= damage;
        StartCoroutine(OnTakeDamage());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerBullet")) return;
        TakeDamage(1);
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
    }
}
