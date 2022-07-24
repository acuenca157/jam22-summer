using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraMovement : MonoBehaviour
{

    private RockController rc;
    // Start is called before the first frame update
    void Start()
    {
        rc = GetComponentInParent<RockController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rc.isMoving = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rc.isMoving = false;
    }
}
