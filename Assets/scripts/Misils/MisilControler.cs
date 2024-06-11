using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NavMeshAgent))]
public class MisilControler : MonoBehaviour,IFactorizable,IComand
{
    [SerializeField] 
    protected Misil_Stats _misil_stats;
    [SerializeField]
    protected float avoidRadius;
    [SerializeField]
    protected LayerMask Mask;

    protected Rigidbody2D _rb;
    protected GameObject Target;
    protected ObstacleAvoidance _avoidance;
    protected float _currentCooldDownTime;
    protected float _currentLifeTime;
    protected float _currentSpeed;
    public UnityEvent Deflection;
    public bool deflected;
    protected NavMeshAgent agent;
    public Misil_Stats Misil_stats  => _misil_stats;

    public float CurrentLifeTime  => _currentLifeTime;

    protected virtual void Awake()
    { 
        _currentCooldDownTime = 0;
        _currentLifeTime = _misil_stats._lifeTime;
        _currentSpeed = Misil_stats.speed;
        _rb = GetComponent<Rigidbody2D>();
        this.SetTarget();
        _avoidance = new ObstacleAvoidance(transform,Target.transform,avoidRadius,_misil_stats.rotationSpeed,Mask);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameManager.Instance.Misil = this.gameObject;
        Deflection = new UnityEvent();
        Deflection.AddListener(OnDeflection);
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
        if (_currentSpeed >= _misil_stats.speed * 3)
            _currentSpeed = _misil_stats.speed * 3;
    }
    protected virtual void FixedUpdate()
    {
        if (_currentCooldDownTime<0)
        {
            if (!deflected)
            {
                Execute();
            }
            else
            {
                agent.SetDestination((-transform.up*2) * _currentSpeed * Time.deltaTime);
                _currentCooldDownTime = _misil_stats._cooldDownTime;   
                deflected = false;
            }
        }
        else
        { 
            _currentCooldDownTime -= Time.deltaTime;
            AgentRotation();
        }
        //Debug.Log(_currentCooldDownTime);
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
    {   deflected = true;  _currentSpeed += _currentSpeed * 0.5f;
        /*-transform.Rotate(new Vector3(0, 0, 180));*/
        //agent.SetDestination(-Target.transform.position);
    }
    public virtual void Execute()
    {
        // execute misil chase strategi
        AgentRotation();
        agent.speed = _currentSpeed * Time.deltaTime;
        agent.SetDestination(Target.transform.position);
    }
    public virtual void AgentRotation()// rotates the game object of a NavMeshAgent to look at the destination 
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            // Obtén la dirección de movimiento
            Vector3 direction = agent.velocity.normalized;

            // Calcula el ángulo en grados entre la dirección de movimiento y el eje X
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            // Crea la rotación objetivo
            Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);

            // Suaviza la rotación del Rigidbody hacia la rotación objetivo
            _rb.transform.rotation = Quaternion.Lerp(_rb.transform.rotation, targetRotation, Time.deltaTime * _misil_stats.rotationSpeed * _currentSpeed);
        }
    }
    public virtual void Destroy()
    {        
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,avoidRadius);

        Gizmos.color = Color.green;
       // Gizmos.DrawLine(transform.position, agent.nextPosition);
        
        /*
        foreach (var item in _avoidance.obstacles)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position,item.transform.position);
        }*/ 
    }
}
