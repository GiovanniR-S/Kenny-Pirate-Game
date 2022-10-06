using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float spdCannonBall;
    private GameObject CannonBall;
    // Start is called before the first frame update
    void Start()
    {
        spdCannonBall = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameIsPaused == false && !GameManager.gameOver && !GameManager.congratulations){
            MouseFollow();
            Fire();
        }
    }
    void Fire () { 
        if(Input.GetMouseButtonDown(0)) {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)((worldMousePosition - transform.position));
            direction.Normalize();
            CannonBall = (GameObject)Instantiate(cannonBall, transform.position + (Vector3)(direction * 0.5f), transform.rotation);
            CannonBall.GetComponent<Rigidbody2D>().velocity = direction * spdCannonBall;
        }
    }
    void MouseFollow () {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        point.Normalize();
        float rotation = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotation);
    }
}
