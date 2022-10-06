using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemy : MonoBehaviour
{
    [Header("Pega Game Objects")]
    [SerializeField] private GameObject p;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private AudioClip attackEnemy;
    [SerializeField] private AudioSource AudioS;

    [Header("Setar Nav Mesh Agent e Distancia para Atacar")]
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private bool attack;
    [SerializeField] private float spdCannonBall, cooldown, maxCooldown;
    [SerializeField] private float distPlayer, minDist;

    [Header("Vida Enemy")]
    [SerializeField] private int life;
    [SerializeField] private Transform healthBar; // Barra verde
    [SerializeField] private GameObject healthBarObject; //Objeto pai da barra
    private Vector3 healthBarScale; //Tamanho da barra
    private float healthBarPercent; //Porcentual da vida

    // Start is called before the first frame update
    void Start () {
        //Variaveis da vida
        life = 4;
        healthBarScale = healthBar.localScale;
        healthBarPercent = healthBarScale.x / life;

        //Nav Agent component
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
        nav.updateUpAxis = false;
        minDist = Random.Range(3, 6); //Distancia minima variando entre 3 a 6
        //Pegando GameObject do Player
        p = GameObject.FindGameObjectWithTag("Player");
        //Variavel de ataque, valor inicial
        attack = false;
        //Velocidade do projetil
        spdCannonBall = 8.5f;
        //Cooldown do attaque;
        cooldown = 0f;
        maxCooldown = 1.5f;
    }

    // Update is called once per frame
    void Update () {
        nav.SetDestination(p.transform.position);
        if(attack == false) {
            nav.updatePosition = true;
        } else {
            nav.updatePosition = false;
            cooldown += 0.5f * Time.deltaTime;
            if(cooldown >= maxCooldown) {
                Attack();
                cooldown = 0;
            }
            CannonFollowPlayer();
        }
        
    }
    public void LateUpdate () {
        Vector3 point = p.transform.position - transform.position;
        distPlayer = Vector2.Distance(transform.position, p.transform.position);
        float rotation = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotation - 90), 1 * Time.fixedDeltaTime);
        healthBarObject.transform.rotation = Quaternion.Slerp(healthBarObject.transform.rotation, Quaternion.Euler(0, 0, rotation + 90), 1);
        UpdateHealthBar();
        if(distPlayer <= minDist) {
            //Debug.Log("Atacando player");
            attack = true;
        } else {
            //Debug.Log("Seguindo player");
            attack = false;
        }
    }
    void UpdateHealthBar () {
        healthBarScale.x = healthBarPercent * life;
        healthBar.localScale = healthBarScale;
    }
    void Attack () {
        //Debug.Log("Atacando");
        Vector2 direction = (Vector2)((p.transform.position - transform.position));
        direction.Normalize();
        GameObject CannonBall = (GameObject)Instantiate(cannonBall, transform.position + (Vector3)(direction * 0.5f), transform.rotation);
        AudioS.PlayOneShot(attackEnemy);
        CannonBall.GetComponent<Rigidbody2D>().velocity = direction * spdCannonBall;
    }
    void CannonFollowPlayer () {
        //Debug.Log("Virando cannon");
        Vector3 point = p.transform.position - cannon.transform.position;
        float rotation = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        cannon.transform.rotation = Quaternion.Slerp(cannon.transform.rotation, Quaternion.Euler(0, 0, rotation), 1 * Time.fixedDeltaTime);
    }
    public int GetLife () {
        var life = this.life;
        return life;
    }
    public void Damage () {
        life -= 1;
    }
}
