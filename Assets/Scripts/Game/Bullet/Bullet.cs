using UnityEngine;
using DG.Tweening;

public class Bullet : SpawnedObject
{
    protected override void Move()
    {
        transform.DOLocalMove(transform.localPosition + Vector3.up, ObjectSpeed).SetSpeedBased()
            .SetLoops(-1, LoopType.Incremental);
    }
}