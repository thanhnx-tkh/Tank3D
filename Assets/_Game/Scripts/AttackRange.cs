using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public Enemy enemyParent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            enemyParent.ChangeState(new AttackState());
        }
    }
}