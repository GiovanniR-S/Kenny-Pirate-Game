using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [Header("Efeito de Explosão")]
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip explosionSound;

    [Header("Game Manager")]
    [SerializeField] private GameManager gm;

    private GameObject destroyExplosion;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 2);
    }
    public void OnTriggerEnter2D (Collider2D collision) {
        if(collision.gameObject.tag == "EnemyShooter"
            || collision.gameObject.tag == "EnemyChaser") {
            //Debug.Log("Inimigo");
            if(collision.gameObject.tag == "EnemyShooter") {
                var shooter = collision.gameObject.GetComponent<ShooterEnemy>();
                shooter.Damage();
                if(shooter.GetLife() <= 0) {
                    audioS.PlayOneShot(explosionSound);
                    destroyExplosion = (GameObject)Instantiate(explosion, collision.transform.position, collision.transform.rotation);
                    gm.AddScore();
                    gm.RemoveNumerosEnemys();
                    Destroy(collision.gameObject); 
                }
            } else if(collision.gameObject.tag == "EnemyChaser") {
                var chaser = collision.gameObject.GetComponent<ChaserEnemy>();
                chaser.Damage();
                if(chaser.GetLife() <= 0) {
                    audioS.PlayOneShot(explosionSound);
                    destroyExplosion = (GameObject)Instantiate(explosion, collision.transform.position, collision.transform.rotation);
                    gm.AddScore();
                    gm.RemoveNumerosEnemys();
                    Destroy(collision.gameObject); 
                }
            }
            Destroy(gameObject);
        }
    }
}
