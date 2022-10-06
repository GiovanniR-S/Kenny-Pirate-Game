using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIGame : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManager gm;
    [Header("Texto Score")]
    [SerializeField] private Text text;
    [Header("Texto Tempo")]
    [SerializeField] private Text time;
    [SerializeField] private float ss, mm;
    
    [Header("Vida Player")]
    [SerializeField] private GameObject p;
    [SerializeField] private GameObject pLifeBar;
    private RectTransform lifeBar;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        text.text = "Score: " + gm.GetScore();
        p = GameObject.FindGameObjectWithTag("Player");
        pLifeBar = GameObject.FindGameObjectWithTag("LifeBarPlayer");
        lifeBar = pLifeBar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gm.GetScore());
        text.text = "Score: " + gm.GetScore();
        //Cronometro/Relogio
        TimeCount();
        if(ss < 10) {
            if(mm < 10) {
                time.text = "Time: 0" + mm + ":0" + (int)ss;
            } else {
                time.text = "Time: " + mm + ":0" + (int)ss;
            }
        } else {
            if(mm < 10) {
                time.text = "Time: 0" + mm + ":" + (int)ss;
            } else {
                time.text = "Time: " + mm + ":" + (int)ss;
            }
        }
        //Life Bar
        lifeBar.sizeDelta = new Vector2(p.GetComponent<Player>().GetLifePlayer() * p.GetComponent<Player>().GetMaxLifePlayer(), 12);
    }
    void TimeCount () {
        ss += Time.deltaTime;
        if(ss >= 60) {
            mm += 1;
            ss = 0;
        }
        if(mm >= GameManager.maxTime) {
            GameManager.congratulations = true;
        }
    }
}
