using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    private GameObject p;
    // Start is called before the first frame update
    void Start () {
        p = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {

    }
    public void LateUpdate () {
        transform.position = new Vector3(p.transform.position.x, p.transform.position.y, transform.position.z);
    }
}
