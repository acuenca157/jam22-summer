using UnityEngine;

public class Gun : MonoBehaviour
{
    private AudioSource m_AudioSource;
    [SerializeField] AudioClip pium;
    [SerializeField] private BalaController ammoPrefab;
    [SerializeField] private Transform startPoint;
    private Vector3 endPoint;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void FireGun()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        BalaController ammo = Instantiate(ammoPrefab, startPoint.transform.position, Quaternion.identity);
        m_AudioSource.PlayOneShot(pium);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireGun();
        }
    }
}
