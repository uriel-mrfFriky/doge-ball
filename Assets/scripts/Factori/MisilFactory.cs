using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MisilFactory : MonoBehaviour
{
    private static MisilFactory instance;

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private MisilControler misilType;
    private AbstractFactory<MisilControler> AbstractFactory = new AbstractFactory<MisilControler>();
    [SerializeField]
    private float _createCoolDown;
    private float _currentCreateCoolDown;
    public UnityEvent CreateMisil;

    public float CurrentCreateCoolDown => _currentCreateCoolDown;

    private void Awake()
    {
        //singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _currentCreateCoolDown = _createCoolDown;
        GameManager.Instance.MisilSpawner = this.gameObject;
        AbstractFactory.factoryObjet = misilType;
        CreateMisil = new UnityEvent();
        CreateMisil.AddListener(SpawnMisil);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_currentCreateCoolDown);
        if (GameManager.Instance.PlayerInstance != null && !GameManager.Instance.Win)
        {
            if (GameManager.Instance.Misil != null)
                return;
            else
            {
                if (_currentCreateCoolDown <= 0)
                {
                    _currentCreateCoolDown = 0;
                    GameManager.Instance.EventQueue.Add(CreateMisil);
                }
                else
                    _currentCreateCoolDown -= Time.deltaTime;
            }
        }
    }
    void SpawnMisil()
    {
        AbstractFactory.Create(spawnPoint);
        _currentCreateCoolDown = _createCoolDown;
    }
    public void setMisilType(GameObject newMisil)
    {
        AbstractFactory.factoryObjet = newMisil.GetComponent<MisilControler>();
    }
}
