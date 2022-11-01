using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class SpawnedObject : MonoBehaviour
{
    private int _objectLifeTimeInMilliseconds;
    private int _objectSpeed;
    private CancellationTokenSource _cancellationTokenSource;
    private Action _destroyAction;

    public int ObjectSpeed => _objectSpeed;
    public CancellationTokenSource CancellationTokenSource => _cancellationTokenSource;
    public Action DestroyAction => _destroyAction;

    public void Init(int objectLifetimeInMilliseconds, int objectSpeed, Action destroyAction)
    {
        _objectLifeTimeInMilliseconds = objectLifetimeInMilliseconds;
        _objectSpeed = objectSpeed;
        _destroyAction = destroyAction;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async void StartMove()
    {
        Move();
        bool isCancelled = await UniTask
            .Delay(_objectLifeTimeInMilliseconds, cancellationToken: _cancellationTokenSource.Token)
            .SuppressCancellationThrow();
        if(isCancelled)
            return;
        _destroyAction.Invoke();
    }

    protected abstract void Move();

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}