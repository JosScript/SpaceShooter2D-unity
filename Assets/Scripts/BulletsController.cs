using UnityEngine;
using UnityEngine.Pool;

public class BulletsController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private ObjectPool<BulletController> _bulletPool;

    private void Awake()
    {
        _bulletPool = new ObjectPool<BulletController>(CreateBullet, OnGetBullet, OnReturnBullet);
    }

    private BulletController CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab).GetComponent<BulletController>();
        bullet.Pool = _bulletPool;
        return bullet;
    }
    
    private void OnGetBullet(BulletController bullet) => bullet.gameObject.SetActive(true);
    private void OnReturnBullet(BulletController bullet) => bullet.gameObject.SetActive(false);
    public ObjectPool<BulletController> GetBulletPool() => _bulletPool;
}
