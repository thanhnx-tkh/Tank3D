using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Enemy enemyParent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            enemyParent.ChangeState(new FollowState());
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            enemyParent.ChangeState(new IdleState());
        }
    }
}
