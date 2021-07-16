using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Singleton
    public static ObjectPool Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    private readonly Dictionary<string, Queue<GameObject>> _objectPoolDictionary=new Dictionary<string, Queue<GameObject>>();
    
    public GameObject GetObject(GameObject gameObject)
    {
        if (_objectPoolDictionary.TryGetValue(gameObject.name,out var objectList))
        {
            if (objectList.Count==0)
            {
                return CreateNewObject(gameObject);
            }
            else
            {
                GameObject _object = objectList.Dequeue();
                _object.SetActive(true);
                return _object;
            }
        }
        else
        {
            return CreateNewObject(gameObject);
        }
    }
    public GameObject GetObject(GameObject gameObject,Transform parent)
    {
        if (_objectPoolDictionary.TryGetValue(gameObject.name,out var objectList))
        {
            if (objectList.Count==0)
            {
                return CreateNewObject(gameObject,parent);
            }
            else
            {
                GameObject _object = objectList.Dequeue();
                gameObject.transform.parent = parent;
                _object.SetActive(true);
                return _object;
            }
        }
        else
        {
            return CreateNewObject(gameObject,parent);
        }
    }
    private GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newGameObject = Instantiate(gameObject);
        newGameObject.name = gameObject.name;
        return newGameObject;
    }
    private GameObject CreateNewObject(GameObject gameObject,Transform parent)
    {
        GameObject newGameObject = Instantiate(gameObject,parent);
        newGameObject.name = gameObject.name;
        return newGameObject;
    }
    public void ReturnGameObject(GameObject gameObject)
    {
        if (_objectPoolDictionary.TryGetValue(gameObject.name,out Queue<GameObject> objectList))
        {
            gameObject.transform.parent = transform;
            objectList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            gameObject.transform.parent = transform;
            newObjectQueue.Enqueue(gameObject);
            _objectPoolDictionary.Add(gameObject.name,newObjectQueue);
        }
        gameObject.SetActive(false);
    }
}