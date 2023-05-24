using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MisilChildrenControler : MisilMitosisControler
{    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        _currentCooldDownTime = 0;
        _currentLifeTime = _misil_stats._lifeTime;
        _currentSpeed = Misil_stats.speed;
        _rb = GetComponent<Rigidbody2D>();
        Deflection = new UnityEvent();
        Deflection.AddListener(OnDeflection);
        this.SetTarget();
        Mitosis_Stats.Childrens.Add(this.gameObject);
    }
    protected override void Update()
    {
        if(Mitosis_Stats.Childrens.Count ==1)
        { 
            GameManager.Instance.LvlManager.GetComponent<LevelManager>().SurvivedMisils += 1;
            base.Destroy();
        }
        if (_currentLifeTime <= 0)
        {
            _currentLifeTime = 0;
            Mitosis_Stats.Childrens.Remove(this.gameObject);
            Destroy();
        }
        else
            _currentLifeTime -= Time.deltaTime;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void SetTarget()
    {
        Target = GameManager.Instance.PlayerInstance;
    }
    public override void OnDeflection()
    {
        base.OnDeflection();
    }
    public override void Execute()
    {
        base.Execute();
    }
}
