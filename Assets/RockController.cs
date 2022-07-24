using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [Range(0, 10)] public float range;
    [HideInInspector] public bool isMoving;

    public string letter = "";
    public int damagePoints = 3;
    private bool picked = false;
    public GameObject rockObject, noteObject, eKey;
    private AudioSource audioSource;
    private GameObject player;
    private EnemyController enemy;

    // Start is called before the first frame update
    void Start()
    {
        int spriteN = Random.Range(0, sprites.Length - 1);
        SpriteRenderer sr = GetComponentInChildren<PiedraMovement>().GetComponent<SpriteRenderer>();
        sr.sprite = sprites[spriteN];
        enemy = FindObjectOfType<EnemyController>();

        player = FindObjectOfType<PlayerMovement>().gameObject;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(makeDetection());
    }

    private void Update()
    {
        eKey.gameObject.SetActive(false);
        if (!picked)
        {
            if (Vector2.Distance(rockObject.transform.position, this.transform.position) > range)
            {
                if (Vector2.Distance(player.transform.position, this.transform.position) <= range)
                {
                    eKey.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        picked = true;
                        if (PlayerPrefs.GetInt("wordle") == 0)
                            PlayerPrefs.SetInt("photos", PlayerPrefs.GetInt("photos") + 1);
                        Destroy(noteObject);
                        PlayerPrefs.SetString("discovered-letters", PlayerPrefs.GetString("discovered-letters") + letter + ",");
                    }
                }
            }
        }
    }

    private IEnumerator makeDetection() {

        while (true) {

            if (isMoving)
            {
                enemy.addPoints(damagePoints);
                if(!audioSource.isPlaying)
                    audioSource.Play();
                yield return new WaitForSeconds(0.5f);
            }
            else {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
}
