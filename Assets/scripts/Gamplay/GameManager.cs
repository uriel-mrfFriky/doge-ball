using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private List<UnityEvent> eventQueue;
    private GameObject _playerInstance;
    private GameObject misil;
    private GameObject misilSpawner;
    private GameObject lvlManager;
    private bool _win;
    private bool _gameOver;

    public static GameManager Instance => instance;
    public GameObject PlayerInstance => _playerInstance;
    public List<UnityEvent> EventQueue => eventQueue;
    public bool GameOver1  => _gameOver;

    public GameObject Misil { get => misil; set => misil = value; }
    public GameObject MisilSpawner { get => misilSpawner; set => misilSpawner = value; }
    public GameObject LvlManager { get => lvlManager; set => lvlManager = value; }
    public bool Win { get => _win; set => _win = value; }

    private void Awake()
    {
        //singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start() 
    {
        // Misil = GameObject.FindGameObjectWithTag("Misil");
        _playerInstance = GameObject.FindGameObjectWithTag("Player");
        eventQueue = new List<UnityEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventQueue.Count > 0)
        {
            //Debug.Log("evenQueue.Count mayor a 0");
            for (int i = EventQueue.Count-1; i >= 0; i--)
            {
                //Debug.Log("evenQueue ejecutando for");
                EventQueue[i].Invoke();
                EventQueue.Remove(EventQueue[i]);
            }   
        }
        else
        { return; }
    }
    public void GameWin()
    {
        _win = true;
        Debug.Log("winer winer chiken to diner");
    }
    public void GameOver() 
    {
        _gameOver = true;
        Destroy(PlayerInstance);
        Debug.Log("game over");
    }
}
