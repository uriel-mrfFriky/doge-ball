using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisilMitosisControler : MisilControler
{
    [SerializeField]
    protected Mitosis_Stats Mitosis_Stats;

    protected override void Start()
    {
        base.Start();
        Mitosis_Stats.childrensCreated = false;
        Mitosis_Stats.Childrens.Clear();
    }
    protected override void Update()
    {
        if (_currentLifeTime <= 0)
        {
            _currentLifeTime = 0;
            if (Mitosis_Stats.Childrens.Count < Mitosis_Stats.childrensAmound)
            {
                Mitosis_Stats.Mitosis_Strategi.Mitosis(Mitosis_Stats.childrensAmound,Mitosis_Stats.ChildrenPrefav,transform.position, Mitosis_Stats.rotateAmound, Mitosis_Stats.Childrens);
            }
            else
            {
                Mitosis_Stats.childrensCreated = true;
                gameObject.SetActive(false);
            }
        }
        else
            _currentLifeTime -= Time.deltaTime;

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
    }
}
