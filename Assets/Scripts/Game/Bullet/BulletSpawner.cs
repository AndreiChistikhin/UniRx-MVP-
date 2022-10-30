using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private Transform _playerTransform;

    private void Start()
    {
        SetFactory(_bulletFactory);
        StartSpawn();
    }

    protected override Vector3 SetObjectPosition()
    {
        return _playerTransform.localPosition;
    }
}