using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Meteor : SpawnedObject
{
    [SerializeField] private Collider2D _collider;

    private void Start()
    {
        _collider.OnTriggerEnter2DAsObservable().Where(t => t.TryGetComponent(out Bullet bullet))
            .Subscribe(_ => MeteorDestroyed()).AddTo(this);
    }

    protected override void Move()
    {
        transform.DOLocalMove(transform.localPosition + Vector3.down, ObjectSpeed).SetSpeedBased()
            .SetLoops(-1, LoopType.Incremental);
    }

    private void MeteorDestroyed()
    {
        MessageBroker.Default.Publish(new MessageBase(MessageType.BulletHitMeteor));
        CancellationTokenSource.Cancel();
        DestroyAction.Invoke();
    }
}