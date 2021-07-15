using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCubeCollider : MonoBehaviour
{
    private bool _destroyStarted = false;

    private float _fallSpeed = 1;
    private void Update()
    {
        if(!_destroyStarted)
            return;
        if (_fallSpeed<10)
            _fallSpeed += Time.deltaTime*10;
        
        transform.Translate(Vector3.down * (Time.deltaTime * _fallSpeed));
    }

    private void OnCollisionEnter(Collision other)
    {
        PathManager.Instance.InstantiateCube();
        GameManager.Instance.ONRoadCubeCollisionEnter?.Invoke();
    }
    private void OnCollisionExit(Collision other)
    {
        StartCoroutine(StartDestroy());
        Destroy(this.gameObject,3f);
    }

    private IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(1f);
        _destroyStarted = true;
    }

}
