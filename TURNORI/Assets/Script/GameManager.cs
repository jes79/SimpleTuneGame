using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



//더조은게임아카데미 유니티반
//2020.10.29 개발일시
//by 유건곤 강사


public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public Dictionary<string,GameObject> D_Player = new Dictionary<string, GameObject>();
    public List <GameObject> L_Monster = new List<GameObject>();

    public bool PlayTurn = true; //true 플레이어턴 false 몬스터턴
    public bool MonsterTurn = true; //코루틴용
    public bool CurrTurn = false; //false 플레이어턴 true 몬스터턴

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;

    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject Monster3;

    //상태창
    public GameObject[] Status;
    Text[] swordmanTxt;
    Text[] priestTxt;
    Text[] witchTxt;

    //전체턴
    public Slider Turn;
    public Text TurnTxt;
    public float TurnTime = 10;


    CoolTime ct;

    //캔버스GAMEOVER
    public GameObject GameOver;
    public GameObject GameWin;


    private void Awake()
    {
        ins = this;

        ct = new CoolTime();
    }

    // Start is called before the first frame update
    void Start()
    {        
        //리소스로드
        //GameObject temp1 = Resources.Load("Player/swordman") as GameObject;
        //GameObject temp2 = Resources.Load("Player/priest") as GameObject;
        //GameObject temp3 = Resources.Load("Player/witch") as GameObject;       

        D_Player.Add("검사", Player1);
        D_Player.Add("신관", Player2);
        D_Player.Add("마법사", Player3);


        L_Monster.Add(Monster1);
        L_Monster.Add(Monster2);
        L_Monster.Add(Monster3);
        //foreach (KeyValuePair<string, GameObject> p in D_Player)
        //{
        //    Instantiate(D_Player[p.Key]);
        //}


        //상태창
        Status = GameObject.FindGameObjectsWithTag("Status");
        swordmanTxt = Status[0].GetComponentsInChildren<Text>();
        priestTxt = Status[1].GetComponentsInChildren<Text>();
        witchTxt = Status[2].GetComponentsInChildren<Text>();


        //캔버스초기화
        GameOver.SetActive(false);
        GameWin.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //전체턴 
        Turn.value = ct.Timer(TurnTime);
        if(Turn.value >= TurnTime)
        {           
            if(PlayTurn)
            {
                TurnTxt.text = "Player Turn";
                MonsterTurn = false;
            }
            else
            {
                MonsterTurn = true;
                TurnTxt.text = "Monster Turn";                
                StartCoroutine("MonsterAttack"); 

            }
            PlayTurn = !PlayTurn;
            CurrTurn = PlayTurn;
        }
        
        print("CurrTurn" + CurrTurn);

        if(D_Player.Count>0)
         StatusShow();

        if(D_Player.Count == 0)
            GameOver.SetActive(true);

        if(L_Monster.Count == 0)
            GameWin.SetActive(true);


    }

    public void Win()
    {
        SceneManager.LoadScene("Field");
    }

    public void Over()
    {
        SceneManager.LoadScene("Field");
    }


    IEnumerator MonsterAttack()    
    {
        int i = 0;
        while(MonsterTurn)
        {
            
            if (L_Monster.Count!=0)
            {
                L_Monster[(i++) % L_Monster.Count].GetComponent<Monster>().NomalAttack();
            }            
            yield return new WaitForSeconds(2f);
            
        }       
    }




    //상태표시 
    void StatusShow()
    {
        Debug.Log("플레이어"+D_Player.Count);
        Debug.Log("몬스터"+L_Monster.Count);
        
        if(D_Player.ContainsKey("검사"))
        {
            Player P = D_Player["검사"].GetComponent<Player>();
            if (P != null)
            {
                swordmanTxt[0].text = P.Pdata.Job;
                swordmanTxt[1].text = "레벨              " + P.Pdata.Level;
                swordmanTxt[2].text = "경험치            " + P.Pdata.Exp;
                swordmanTxt[3].text = "HP        " + P.Pdata.Hp + "/" + P.Pdata.MaxHp;
                swordmanTxt[4].text = "MP        " + P.Pdata.Mp + "/" + P.Pdata.MaxMp;
            }
        }


        if (D_Player.ContainsKey("신관"))
        {
            Player P2 = D_Player["신관"].GetComponent<Player>();
            if (P2 != null)
            {
                priestTxt[0].text = P2.Pdata.Job;
                priestTxt[1].text = "레벨              " + P2.Pdata.Level;
                priestTxt[2].text = "경험치            " + P2.Pdata.Exp;
                priestTxt[3].text = "HP        " + P2.Pdata.Hp + "/" + P2.Pdata.MaxHp;
                priestTxt[4].text = "MP        " + P2.Pdata.Mp + "/" + P2.Pdata.MaxMp;
            }
        }
        if (D_Player.ContainsKey("마법사"))
        {
            Player P3 = D_Player["마법사"].GetComponent<Player>();
            if (P3 != null)
            {
                witchTxt[0].text = P3.Pdata.Job;
                witchTxt[1].text = "레벨              " + P3.Pdata.Level;
                witchTxt[2].text = "경험치            " + P3.Pdata.Exp;
                witchTxt[3].text = "HP        " + P3.Pdata.Hp + "/" + P3.Pdata.MaxHp;
                witchTxt[4].text = "MP        " + P3.Pdata.Mp + "/" + P3.Pdata.MaxMp;
            }
        }
    }




}
