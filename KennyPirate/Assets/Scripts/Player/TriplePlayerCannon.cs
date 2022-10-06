using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplePlayerCannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float spdCannonBall;
    [SerializeField] Transform cannon1, cannon2, cannon3;
    private GameObject CannonBall, CannonBall2, CannonBall3;
    // Start is called before the first frame update
    void Start () {
        spdCannonBall = 20f;
    }

    // Update is called once per frame
    void Update () {
        Fire();
    }
    void Fire () {
        if(Input.GetMouseButtonDown(0)) {
            CannonBall = (GameObject)Instantiate(cannonBall, cannon1.position, transform.rotation);
            CannonBall2 = (GameObject)Instantiate(cannonBall, cannon2.position, transform.rotation);
            CannonBall3 = (GameObject)Instantiate(cannonBall, cannon3.position, transform.rotation);
        }
        CannonBall.GetComponent<Rigidbody2D>().velocity = cannon1.right * spdCannonBall;
        CannonBall2.GetComponent<Rigidbody2D>().velocity = cannon2.right * spdCannonBall;
        CannonBall3.GetComponent<Rigidbody2D>().velocity = cannon3.right * spdCannonBall;
    }
}
