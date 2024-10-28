using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    private Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    private void Die()
    {
        Debug.Log("플레이어 사망");
    }

    void Update()
    {
        if (health.currentValue <= 0)
        {
            Die();
        }
        else
        {
            health.Subtract(health.passiveValue * Time.deltaTime);
        }
    }

}
