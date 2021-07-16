using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCubeCollider : MonoBehaviour,IPoolObject
{
    private bool _fallStarted;
    private float _fallSpeed;
    private ObjectPool _objectPool;
    private void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
    }
    public void OnDisable()
    {
        _fallStarted = false;
        _fallSpeed = 1.5f;
    }
    private void Update()
    {
        if(!_fallStarted)
            return;
        //fall cube with acceleration
        if (_fallSpeed<10)
            _fallSpeed += Time.deltaTime*10;
        transform.Translate(Vector3.down * (Time.deltaTime * _fallSpeed));
    }
    //When player enters a cube
    private void OnCollisionEnter(Collision other)
    {
        PathManager.Instance.InstantiateCube();
        GameManager.Instance.ONRoadCubeCollisionEnter?.Invoke();
    }    
    //When player exit from cube, start fall of road cube
    private void OnCollisionExit(Collision other)
    {
        StartCoroutine(StartDestroy());
    }
    //Cube wait before fall and after 1 second return to pool
    private IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(.3f);
        _fallStarted = true;
        yield return new WaitForSeconds(1f);
        _objectPool.ReturnGameObject(this.gameObject);
    }
}
