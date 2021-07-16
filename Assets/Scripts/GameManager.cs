using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    //
    public bool isGameStarted=false;
    public bool isGameFailed = false;
    public float gameSpeed;
    [SerializeField] private List<Color> colors;
    [SerializeField] private Material groundMat;
    private int _currentColorIndex=0;
    //actions
    public UnityAction ONGameStarted;
    public UnityAction ONFailed;
    public UnityAction ONTouch;
    public UnityAction ONCollectableCollision;
    public UnityAction ONRoadCubeCollisionEnter;
    public UnityAction ONScore;
    //references
    public FollowLightController followLightController;
    private int _score = 0;
    public int Score => _score;

    private void Start()
    {
        ONGameStarted += OnGameStarted;
        ONRoadCubeCollisionEnter += DequeueDirection;
        ONTouch += OnTouch;
        ONCollectableCollision += OnCollectableCollision;
        ONFailed += OnFailed;
        ONScore += ChangeGroundMatColor;
        //
        groundMat.color= colors[0];
    }
    //Every movement from one cube to another, getting next point to moves the light
    private void DequeueDirection()
    {
        followLightController.DequeueDirection();
    }
    private void IncreaseScore(int s = 1)
    {
        _score += s;
        ONScore?.Invoke();
    }
    private void OnTouch()
    {
        IncreaseScore();
    }
    private void OnCollectableCollision()
    {
        IncreaseScore(2);
    }
    private void OnGameStarted()
    {
        isGameStarted = true;
    }
    private void OnFailed()
    {
        isGameFailed = true;
    }

    private void ChangeGroundMatColor()
    {
        //Every 50 score change color of ground from list
        if (_currentColorIndex!=(_score / 50) % colors.Count)
        {
            _currentColorIndex = (_score / 50) % colors.Count;
            var targetColor = colors[_currentColorIndex];
            var currentColor = groundMat.color;
            StartCoroutine(SmoothColor(targetColor,currentColor));
        }
    }
    //Changing color smoothly
    private IEnumerator SmoothColor(Color targetColor,Color currentColor)
    {
        float t = 0;
        while (t<1)
        {
            groundMat.color=Color.Lerp(currentColor, targetColor,t);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
