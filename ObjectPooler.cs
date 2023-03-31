using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        _pooledObjects = new List<GameObject>();
        GameObject temp;
        for (var i = 0; i < _amountToPool; i++)
        {
            temp = Instantiate(_objectToPool);
            temp.SetActive(false);
            _pooledObjects.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (var i = 0; i < _amountToPool-1; i++)
        {
            Debug.Log(i);
            if (!_pooledObjects[i].activeInHierarchy)
                return _pooledObjects[i];
        }

        return null;
    }
}