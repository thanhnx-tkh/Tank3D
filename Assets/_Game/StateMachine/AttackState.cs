using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        Debug.Log("Attack");
    }

    public void OnExecute(Enemy t)
    {

    }

    public void OnExit(Enemy t)
    {

    }
}
