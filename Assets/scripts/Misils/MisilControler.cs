using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class MisilControler : MonoBehaviour,IFactorizable,IComand
{
    [SerializeField] 
    protected Misil_Stats _misil_stats;

    protected Rigidbody2D _rb;
    protected GameObject Target;
    protected float _currentCooldDownTime;
    protected float _currentLifeTime;
    protected float _currentSpeed;
    public UnityEvent Deflection;
    private bool deflected;

    public Misil_Stats Misil_stats  => _misil_stats;

    public float CurrentLifeTime  => _currentLifeTime;

    protected virtual void Awake()
    { }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameManager.Instance.Misil = this.gameObject;
        _currentCooldDownTime = 0;
        _currentLifeTime = _misil_stats._lifeTime;
        _currentSpeed = Misil_stats.speed;
        _rb = GetComponent<Rigidbody2D>();
        Deflection = new UnityEvent();
        Deflection.AddListener(OnDeflection);
        this.SetTarget();
    }
    protected virtual void Update()
    {
        if (_currentLifeTime <= 0)
        {
            _currentLifeTime = 0;
            GameManager.Instance.LvlManager.GetComponent<LevelManager>().SurvivedMisils += 1;
            GameManager.Instance.MisilSpawner.GetComponent<MisilFactory>().setMisilType(_misil_stats.NextMisil);
            Destroy();
        }
        else
            _currentLifeTime -= Time.deltaTime;
        if (_currentSpeed >= _misil_stats.speed * 2)
            _currentSpeed = _misil_stats.speed * 2;
    }
    protected virtual void FixedUpdate()
    {
        if (_currentCooldDownTime<=0)
        {
            if (!deflected)
            {
                Execute();
            }
            else
            {
                _rb.velocity = _rb.velocity * -1;
                _rb.angularVelocity = 0;
                _currentCooldDownTime = _misil_stats._cooldDownTime;
                deflected = false;
            }
        }
        else
        { _currentCooldDownTime -= Time.deltaTime; }
    }
    public virtual void Create(Transform spawnPoint)
    {
        Instantiate(this.gameObject, spawnPoint.position,Quaternion.identity);
    }
    public virtual void SetTarget()
    {
        Target = GameManager.Instance.PlayerInstance;
    }
    public virtual void OnDeflection()
    { deflected = true; transform.Rotate(new Vector3(0, 0, 180)); _currentSpeed += _currentSpeed * 0.5f; }
    public virtual void Execute()
    {
        // execute misil chase strategi
        _misil_stats.chase_Strategi.MoveStrategi(Target.transform, transform, _rb, _currentSpeed,_misil_stats.rotationSpeed);
        _rb.angularVelocity = _misil_stats.chase_Strategi.NewAngle;
        _rb.velocity = _misil_stats.chase_Strategi.NewVelocity;
    }
    public virtual void Destroy()
    {        
        Destroy(this.gameObject);
    }
}
