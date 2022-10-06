using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Vida player")]
    [SerializeField] private int life = 10; //Vida Player
    [SerializeField] private int maxlife; //Vida Player
    [Header("Dano que o Player leva")]
    [SerializeField] private int damage;
    private Rigidbody2D rb;
    private Vector2 mov;

    [Header("Sprites barco se destruindo e componento SpriteRenderer")]
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private Sprite[] sprite = new Sprite[4];

    [Header("Tipo de ataque")]
    [SerializeField] private string attack = "Triple";
    [SerializeField] private GameObject singleAttack;
    [SerializeField] private GameObject tripleAttack;
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip attackCannon;

    [Header("Velocidade e Rotação")]
    [SerializeField] private float spd; // Variaveis de velocidade
    [SerializeField] private float maxSpeed; // Variaveis de velocidade
    private float rotateAngle; // Variaveis de velocidade

    [SerializeField] private float rotateSpeed; // Variaveis de rotação

    [Header("Fator do Dift")]
    [Range(0, 1)]
    [SerializeField] private float diftFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
        //Valor da velocidade do player e sua velocidade maxima
        spd = 0f;
        maxSpeed = 60f;
        //Valor da rotação do player e velocidade de rotação
        rotateAngle = 0;
        rotateSpeed = 150f;

        rb = GetComponent<Rigidbody2D>(); //Componente RigidBody do player
        //Valor da vida do player
        life = 10;
        maxlife = 10;
        //Valor Dano
        damage = 1;

        spr = GetComponent<SpriteRenderer>(); //Componente SpriteRenderer do player
        //Player com vida cheia
        spr.sprite = sprite[0];

        //Muda attack
        ChangeAttack();

    }
    public void FixedUpdate () {
       //Debug.Log(spd);
        //Movimentação do Player
        MovimentedPlayer();
        Rotation();
        Dift();
    }
    // Update is called once per frame
    void Update()
    {
        mov.x = Input.GetAxis("Vertical");
        mov.y = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Tab)) {
            ChangeAttack();
        }
        if(Input.GetMouseButtonDown(0)) {
            audioS.PlayOneShot(attackCannon);
        }
        lifePlayer();
    }
    public int GetLifePlayer () {
        var life = this.life;
        return life;
    }
    public int GetMaxLifePlayer () {
        var maxlife = this.maxlife;
        return maxlife;
    }
    void lifePlayer () {
        //Vida do player durando o jogo
        if(life >= maxlife) {
            //Player com vida cheia
            spr.sprite = sprite[0];
            life = maxlife;
        } else if(life < 8 && life >= 4) {
            //Player depois de um pouco de dano
            spr.sprite = sprite[1];
        } else if(life < 4 && life > 0) {
            //Player depois de muito dano
            spr.sprite = sprite[2];
        } else if(life <= 0) {
            spr.sprite = sprite[3];
            life = 0;
            GameManager.gameOver = true;
        }
    }
    void MovimentedPlayer () {
        //Acerelar barco
        if(Input.GetKey(KeyCode.W)) {
            if(spd <= maxSpeed) {
                spd += 5.5f;
            }
            rb.drag = 0;
            rb.AddForce((transform.up * mov.x * spd) * Time.deltaTime, ForceMode2D.Force);
        } else if(!Input.GetKey(KeyCode.W)) {
            if(spd > 0) {
                spd -= 5.5f;
            }
            rb.drag = Mathf.Lerp(rb.drag, diftFactor, Time.fixedDeltaTime);
        }
        

    }
    void Rotation () {
        rotateAngle = (rotateAngle - (mov.y * rotateSpeed));
        rb.MoveRotation(rotateAngle * Time.deltaTime);
    }
    void Dift () {
        Vector2 velocityUp = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 velocityRight = transform.right * Vector2.Dot(rb.velocity, transform.right);
        rb.velocity = velocityUp + velocityRight * diftFactor;
    }
    void ChangeAttack () {
        if(attack == "Single") {
            singleAttack.SetActive(false);
            tripleAttack.SetActive(true);
            attack = "Triple";
        }else if(attack == "Triple") {
            singleAttack.SetActive(true);
            tripleAttack.SetActive(false);
            attack = "Single";
        }
    }
    public void Damage () {
        life -= damage;
    }
}
