using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisilBHoleControler : MisilControler
{
    [SerializeField] private BlackHole_stats blackHole_Stats;
    protected override void Awake()
    {
        base.Awake();
        this.SetTarget();
    }
    protected override void Start()
    {
        base.Start();
        blackHole_Stats.suction_Strategi.steRange(blackHole_Stats.range);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Create(Transform spawnPoint)
    {
        base.Create(spawnPoint);
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
        blackHole_Stats.suction_Strategi.MoveStrategi(Target.transform, transform, Target.GetComponent<Rigidbody2D>(), blackHole_Stats.succionSpeed, _misil_stats.rotationSpeed,_avoidance);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * blackHole_Stats.range);
        Gizmos.DrawWireSphere(transform.position, blackHole_Stats.range);
    }
}
