using UnityEngine;

[CreateAssetMenu(fileName = "SpawnedObjectConfig", menuName = "Configs/SpawnedObjectConfig")]
public class SpawnedObjectConfig : ScriptableObject
{
    [SerializeField] private float _appearIntervalInSeconds;
    [SerializeField] private int _lifeTimeInMilliseconds;
    [SerializeField] private int _speed;

    public float AppearIntervalInSeconds => _appearIntervalInSeconds;
    public int LifeTimeInMilliseconds => _lifeTimeInMilliseconds;
    public int Speed => _speed;
}