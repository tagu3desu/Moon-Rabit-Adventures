using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFollow : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            player.transform.position.x,
            this.transform.position.y,
            this.transform.position.z);
    }
}
