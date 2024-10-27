using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    private Condition health { get { return uiCondition.health; } }

    void Update()
    {
        health.Subtract(health.passiveValue * Time.deltaTime);
    }
}
