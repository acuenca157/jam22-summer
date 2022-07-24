using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBattle : MonoBehaviour
{
    private float lives = 6;
    private bool isInvencible = false;
    Animator animator;
    float horizontal;
    public float speed;
    private AudioSource audioSource;
    [SerializeField] private AudioClip pupa;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        this.transform.Translate(Vector3.right *  horizontal * speed * Time.deltaTime, Space.World);
        animator.SetBool("walking", horizontal != 0);
        this.GetComponent<SpriteRenderer>().flipX = horizontal < 0;
    }

    public void reciveDamage() {
        if (!isInvencible) {
            StartCoroutine(invencibleTime());
            lives--;
            audioSource.PlayOneShot(pupa);
        }

        if (lives <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator invencibleTime() {
        isInvencible = true;
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(3f);
        isInvencible = false;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForEndOfFrame();
    }
}
