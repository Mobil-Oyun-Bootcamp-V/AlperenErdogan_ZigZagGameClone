using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathManager : MonoBehaviour
{
    [SerializeField] private GameObject roadCubePrefab;
    [SerializeField] private Transform startCube;
    [SerializeField] private GameObject collectablePrefab;
    public Queue<Vector2> LightPathQueue = new Queue<Vector2>();
    [Range(0,100)]
    [SerializeField]
    private int collectableChance;
    private Vector3 _lastPos;
    #region Singleton
    private static PathManager _instance;
    public static PathManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    #endregion
    void Start()
    {
        _lastPos = startCube.position;
        for (int i = 0; i < 15; i++)
        { 
            InstantiateCube();
        }
        //dequeuing first 2 direction for light follow player 2 step ahead
        for (int i = 0; i < 2; i++)
        {
            LightPathQueue.Dequeue();
        }
    }

    
    public void InstantiateCube()
    {
        //Creating randomly directed road cube
        var direction = Random.Range(0, 2) == 0 ? Direction.Forward : Direction.Left;
        _lastPos = direction==Direction.Left ? _lastPos+=Vector3.left*3 : _lastPos+=Vector3.forward*3;
        var instantiated = ObjectPool.Instance.GetObject(roadCubePrefab);
        instantiated.transform.position = _lastPos;
        
        // instantiated.GetComponent<RoadCubeCollider>().OnObjectSpawn();
        //Adding randomly collectable
        var randomIntForCollectable = Random.Range(0f, 10.1f);
        if (randomIntForCollectable<collectableChance/10f)
        {
            var collectablePos = _lastPos;
            collectablePos.y += 3;
            Instantiate(collectablePrefab, collectablePos, collectablePrefab.transform.rotation, transform);
        }
        //Add path to queue
        LightPathQueue.Enqueue(new Vector2(_lastPos.x,_lastPos.z));
    }
}
