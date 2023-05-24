using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    private int survivedMisils;
    private UnityEvent Winlvl;
    [SerializeField]
    private int _amoundMisilsWin;

    public static LevelManager Instance => instance;
    public int AmoundMisilsWin => _amoundMisilsWin;
    public int SurvivedMisils { get => survivedMisils; set => survivedMisils = value; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }
    private void Start()
    {
        Winlvl = new UnityEvent();
        GameManager.Instance.LvlManager = this.gameObject;
        Winlvl.AddListener(GameManager.Instance.GameWin);
    }
    private void Update()
    {
        if(survivedMisils < AmoundMisilsWin)
        {
            GameManager.Instance.Win = false;
            return;
        }else
            GameManager.Instance.EventQueue.Add(Winlvl);
    }

}
