using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MeteorSpawner : Spawner<Meteor>
{
    [SerializeField] private MeteorFactory _meteorFactory;
    [SerializeField] private CanvasScaler _canvasScaler;

    private const int MeteorAppearanceYPosition = 600;

    private void Start()
    {
        SetFactory(_meteorFactory);
        StartSpawn();
    }

    protected override Vector3 SetObjectPosition()
    {
        float halfOfCanvasWidth = _canvasScaler.referenceResolution.x / 2;
        float randomXPosition = Random.Range(-halfOfCanvasWidth, halfOfCanvasWidth);
        return new Vector3(randomXPosition, MeteorAppearanceYPosition, 0);
    }
}