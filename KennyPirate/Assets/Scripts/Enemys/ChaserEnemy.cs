using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserEnemy : MonoBehaviour
{
    
    [Header("Pega Game Object do Player")]
    [SerializeField] private GameObject p;
    [Header("Setar Nav Mesh Agent e Distancia para Atacar/Explodir")]
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private bool explosion;
    [SerializeField] private GameObject destroyExplosion;

    [Header("Vida Enemy")]
    [SerializeField] private int life;
    [SerializeField] private Transform healthBar; // Barra verde
    [SerializeField] private GameObject healthBarObject; //Objeto pai da barra
    private Vector3 healthBarScale; //Tamanho da barra
    private float healthBarPercent; //Porcentual da vida

    // Start is called before the first frame update
    void Start()
    {
        //Variaveis da vida
        life = 2;
        healthBarScale = healthBar.localScale;
        healthBarPercent = healthBarScale.x / life;

        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
        nav.updateUpAxis = false;
        p = GameObject.FindGameObjectWithTag("Player");
        explosion = false;
    }

    // Update is called once per frame
    void Update () {
        nav.SetDestination(p.transform.position);
        
        if(explosion) {
            ExplosionAttack();
        }
    }
    public void LateUpdate () {
        Vector3 point = p.transform.position - transform.position;
        float rotation = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotation - 90), 1 * Time.fixedDeltaTime);
        healthBarObject.transform.rotation = Quaternion.Slerp(healthBarObject.transform.rotation, Quaternion.Euler(0, 0, rotation + 90), 1);
        UpdateHealthBar();
    }
    public void OnCollisionEnter2D (Collision2D collision) {
        if(collision.gameObject == GameObject.FindGameObjectWithTag("Player")) {
            explosion = true;
        }
    }
    void UpdateHealthBar () {
        healthBarScale.x = healthBarPercent * life;
        healthBar.localScale = healthBarScale;
    }
    void ExplosionAttack () {
        destroyExplosion = (GameObject)Instantiate(destroyExplosion, transform.position, transform.rotation);
        p.GetComponent<Player>().Damage();
        Destroy(gameObject);
    }
    public int GetLife () {
        var life = this.life;
        return life;
    }
    public void Damage () {
        life -= 1;
    }
}
