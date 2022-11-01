using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    private void Start()
    {
        _collider.OnTriggerEnter2DAsObservable().Subscribe(_ =>
            MessageBroker.Default.Publish(new MessageBase(MessageType.MeteorHitFinish)));
    }
}