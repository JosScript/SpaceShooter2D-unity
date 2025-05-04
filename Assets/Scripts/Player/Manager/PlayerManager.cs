using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int hp;
    private int _score;
    private bool _levelOver;
    private PlayerController _playerController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }
    
    public PlayerController GetPlayerController()
    {
        return _playerController;
    }

    public bool IsPlayerDead() => hp <= 0;

    private void CheckGameOver()
    {
        if (!IsPlayerDead() && !_levelOver) return;
        _levelOver = true;
        _playerController.enabled = false;
    }
    
    private void TakeDamage(int damage)
    {
        if (!_playerController.IsVulnerable) return;
        if (hp - damage <= 0)
            hp = 0;
        else
            hp -= damage;
        StartCoroutine(_playerController.OnTakeDamage());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            TakeDamage(1);
        else if (other.CompareTag("EnemyBullet"))
            TakeDamage(1);
    }

    public void AddScore(int score)
    {
        _score += score;
    }
    
    public int GetScore() => _score;
    public int GetHp() => hp;
    
    // Update is called once per frame
    private void Update()
    {
        CheckGameOver();
    }
}
