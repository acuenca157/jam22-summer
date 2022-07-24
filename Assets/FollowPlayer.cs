using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    bool followPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer) {
            Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
            this.transform.position = newPos;
        }
    }
}
