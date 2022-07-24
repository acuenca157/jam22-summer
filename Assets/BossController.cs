using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public GameObject topLeft, topRight;
    public GameObject pata;
    public int vida = 1000;
    public int damage = 5;
    private SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(lanzarPatas());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "laserPlayer") {
            vida -= damage;
            sr.color = Color.red;
            //Destroy(collision.gameObject);
            if (vida <= 0) {
                SceneManager.LoadScene("Win");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "laserPlayer") { 
            sr.color = Color.white;
        }
    }

    IEnumerator lanzarPatas() {
        while (vida > 0) {
            float pointX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x); 
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            Instantiate(pata, new Vector3(pointX, 9.35f, 0), Quaternion.identity);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();

    }
}
