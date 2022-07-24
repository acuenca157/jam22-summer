using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform originBullet;
    private bool canFire = true;
    private float timer;
    public float timeBetweenFiring = 1f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire) {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring) { 
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire) {
            canFire = false;
            Instantiate(bullet, originBullet.transform.position, Quaternion.identity);
        }
    }
}
