using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private Player playerPos;
    [SerializeField]
    private GameObject wall;
    Vector3 offSet;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Player>();
        offSet = new Vector3(0, -4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerPos.transform.position, out hit, 3))
        {
            Destroy(hit.transform.gameObject);
        }
    }
}
