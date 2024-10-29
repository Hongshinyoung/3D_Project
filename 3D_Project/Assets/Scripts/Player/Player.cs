using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerCondition condition;
    public ItemData itemData;
    public Transform dropPos;
    public Action addItem;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerController = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
