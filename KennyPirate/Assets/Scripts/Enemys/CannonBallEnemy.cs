using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallEnemy : MonoBehaviour
{
    [SerializeField] private Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5);
    }
    public void OnTriggerEnter2D (Collider2D collision) {
        if(collision.gameObject == GameObject.FindGameObjectWithTag("Player")) {
            p.Damage();
            Destroy(gameObject);
        }
    }
}
