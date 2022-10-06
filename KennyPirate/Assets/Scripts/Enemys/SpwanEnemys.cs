using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanEnemys : MonoBehaviour
{
    [Header("Enemys")]
    [Space]
    [SerializeField] private GameObject[] enemys;
    [Space]
    [Header("Tempo de Spwan dos inimigos")]
    [Space]
    [SerializeField] private float cooldown;
    [SerializeField] private float maxCooldown;

    [Header("Conta numero de inimigos")]
    [SerializeField] private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        LimitedCooldown();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Numeros Inimigos: " + countEnemys + " CanSpawn: " + canSpwan);
        if(gm.GetNumerosEnemys() < 30) {
            cooldown += 1 * Time.deltaTime;
            if(cooldown >= maxCooldown) {
                //Debug.Log("Spwanando 1 inimigo");  
                GameObject enemy = Instantiate(enemys[Random.Range(0, enemys.Length)], transform.position, transform.rotation);
                gm.AddNumerosEnemys();
                LimitedCooldown();
                cooldown = 0;
            }
        }
        
    }
    void LimitedCooldown () {
        if(GameManager.optionsTimeEnemy == 1) {
            maxCooldown = Random.Range(4, 8);
        } else if(GameManager.optionsTimeEnemy == 2) {
            maxCooldown = Random.Range(2, 6);
        } else if(GameManager.optionsTimeEnemy == 3) {
            maxCooldown = Random.Range(1, 3);
        }
    }
}
