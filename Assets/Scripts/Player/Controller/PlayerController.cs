using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private BulletsController bulletsController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<AudioSource> audioSource = new();
    [SerializeField] private List<AudioClip> audioClips = new();
    private bool _isVulnerable = true;
    private bool _isShooting;
    
    public bool IsVulnerable => _isVulnerable;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    private void PlayerMovement()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, moveVertical, 0.0f) * speed;
        transform.Translate(movement * Time.deltaTime);
        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -7.5f, 7.5f);
        pos.y = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
    
    private void OnFire()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (_isShooting) { _isShooting = false; return; }

        audioSource[0].clip = audioClips[0];
        audioSource[0].Play();
        
        foreach (var spawnPoint in spawnPoints)
        {
            if (!spawnPoint.gameObject.activeInHierarchy) continue;
            var bullet = bulletsController.GetBulletPool().Get();
            bullet.transform.position = spawnPoint.position;
        }
        _isShooting = true;
    }

    public IEnumerator OnTakeDamage()
    {
        var originalColor = spriteRenderer.color;
        _isVulnerable = false;
        audioSource[1].clip = audioClips[1];
        audioSource[1].Play();
        for (int i = 0; i < 3; i++)
        {
            yield return OnDamage(originalColor, Color.red, 0.2f);
            yield return OnDamage(Color.red, originalColor, 0.2f);
        }
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
    
    // Update is called once per frame
    private void Update()
    {
        PlayerMovement();
        OnFire();
    }
}
