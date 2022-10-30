using UnityEngine;
using UnityEngine.Pool;

public class GenericFactory<T> : MonoBehaviour where T : SpawnedObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _parentObject;

    private ObjectPool<T> _objectPool;

    private void Awake()
    {
        _objectPool = new ObjectPool<T>(
            () => Instantiate(_prefab, _parentObject), 
            t => t.gameObject.SetActive(true), 
            t => t.gameObject.SetActive(false));
    }

    public T GetObject()
    {
        return _objectPool.Get();
    }

    public void ReleaseObject(T poolObject)
    {
        _objectPool.Release(poolObject);
    }
}