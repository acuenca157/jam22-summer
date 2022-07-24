using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataController : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    private Vector3 startMarker, endPos;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public GameObject polvo;
    public AudioClip pum;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bool rotate = 1f > Random.Range(0f, 2f);
        if (rotate) {
            this.transform.Rotate(new Vector3(0, 180, 0));
        }
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        startMarker = this.transform.position;
        endPos = new Vector3 (startMarker.x, 0f, startMarker.z);
        journeyLength = Vector3.Distance(startMarker, endPos);
        StartCoroutine(ataque());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            PlayerBattle player = collision.gameObject.GetComponent<PlayerBattle>();
            player.reciveDamage();
        }
    }

    IEnumerator ataque() {

        while (this.transform.position != endPos) {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker, endPos, fractionOfJourney);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Abajo done");
        Instantiate(polvo, new Vector3(this.transform.position.x, 0f, this.transform.position.z), Quaternion.identity);
        audioSource.PlayOneShot(pum);
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        Vector3 newObjective = new Vector3(this.transform.position.x, 9.3f, this.transform.position.z);
        Vector3 actualPos = transform.position;
        float startTime2 = Time.time;
        while (this.transform.position != newObjective)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime2) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(actualPos, newObjective,  fractionOfJourney);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Arria Done");
        Destroy(this.gameObject);
        yield return new WaitForEndOfFrame();
    }
}
