using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : SpawnedObject
{
    [SerializeField] private SpawnedObjectConfig _config;

    private GenericFactory<T> _factory;

    protected void SetFactory(GenericFactory<T> factory)
    {
        _factory = factory;
    }

    protected void StartSpawn()
    {
        Observable.Interval(TimeSpan.FromSeconds(_config.AppearIntervalInSeconds)).Subscribe(_ => SpawnObject())
            .AddTo(this);
    }

    private void SpawnObject()
    {
        T spawnedObject = _factory.GetObject();
        spawnedObject.transform.localPosition = SetObjectPosition();
        spawnedObject.Init(_config.LifeTimeInMilliseconds, _config.Speed, () =>
        {
            _factory.ReleaseObject(spawnedObject);
            spawnedObject.transform.DOKill();
        });
        spawnedObject.StartMove();
    }

    protected abstract Vector3 SetObjectPosition();
}