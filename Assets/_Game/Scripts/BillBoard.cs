using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform camMain;
    private void LateUpdate() {
        transform.LookAt(transform.position + camMain.forward);
    }
}