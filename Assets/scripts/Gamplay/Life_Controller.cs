#region usings
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion
public class Life_Controller 
{
    private float maxLife;
    private float currentLife;

    public UnityEvent Dead;
    public UnityEvent Damaged;
    public float CurrentLife => currentLife;

    public Life_Controller(float initialMaxLife) {
        maxLife = initialMaxLife;
        currentLife = maxLife;
        Dead = new UnityEvent();
        Damaged = new UnityEvent();
    }
    public void GetDamage(float damage) {
        currentLife -= damage;
        GameManager.Instance.EventQueue.Add(Damaged);
        if (currentLife <= 0) {
            Die();
        }
    }
    public void GetHeal(float heal) {
        currentLife += heal;

        if (currentLife > maxLife) {
            currentLife = maxLife;
        }
    }
    private void Die() {
        currentLife = 0;
        GameManager.Instance.EventQueue.Add(Dead);
    }
}
