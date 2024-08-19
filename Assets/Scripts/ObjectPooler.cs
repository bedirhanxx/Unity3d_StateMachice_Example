using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
  public static ObjectPooler Instance;
  public GameObject CubePrefab;
  public int PoolSize = 5;

  private List<GameObject> _pooledObjects;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }

    _pooledObjects = new List<GameObject>();
    for (int i = 0; i < PoolSize; i++)
    {
      GameObject obj = Instantiate(CubePrefab);
      obj.SetActive(false);
      _pooledObjects.Add(obj);
    }
  }

  public GameObject GetPooledObject()
  {
    foreach (GameObject obj in _pooledObjects)
    {
      if (!obj.activeInHierarchy)
      {
        return obj;
      }
    }

    GameObject newObj = Instantiate(CubePrefab);
    newObj.SetActive(false);
    _pooledObjects.Add(newObj);
    return newObj;
  }

  public void ReturnToPool(GameObject obj)
  {
    obj.SetActive(false);
  }
}