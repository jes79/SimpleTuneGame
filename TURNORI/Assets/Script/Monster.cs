using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData Pdata;
    GameObject[] Player;
    Rigidbody2D rig;

    public bool Back = false;
    public Vector3 OriPos;

    Animator ani;

    public int Hp;
    public int MaxHp;

    //데미지 캔버스
    public GameObject DamegeCanvas;
     TextMeshProUGUI TMPdamege;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        OriPos = transform.position;
        ani = GetComponent<Animator>();


        Hp = Pdata.Hp;
        MaxHp = Pdata.MaxHp;


    }
    public void NomalAttack()
    {
        StartCoroutine("NomalAttackCT");
    }
    IEnumerator NomalAttackCT()
    {
        Back = false;
        int r = Random.Range(0, Player.Length);
        while (true)
        {

            //rig.MovePosition(Vector3.MoveTowards(transform.position, Monster[0].transform.position, 100 * Time.deltaTime));
            if (Player[r] != null)
            {
                rig.MovePosition(Vector3.Lerp(transform.position, Player[r].transform.position, 20 * Time.deltaTime));
                if (Vector3.Distance(transform.position, Player[r].transform.position) <= 0.5f)
                {
                    ani.SetTrigger("attack");
                    Player[r].GetComponent<Player>().Damege(Pdata.Damege);

                    yield return new WaitForSeconds(0.03f);
                    Back = true;

                    break;
                }
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Back == true)
        {
            //rig.MovePosition(Vector3.MoveTowards(transform.position, OriPos, 100 * Time.deltaTime));
            rig.MovePosition(Vector3.Lerp(transform.position, OriPos, 20 * Time.deltaTime));

        }
    }

    public void Damege(int Attack)
    {
        Hp -= Attack;
        ani.SetTrigger("damege");
        GameObject go =Instantiate(DamegeCanvas, transform.position, Quaternion.identity);
        //캔버스에 몬스터를 부모로하겠다.
        go.transform.SetParent(transform);

        TMPdamege = go.GetComponentInChildren<TextMeshProUGUI>();
        TMPdamege.text = "" + Attack;
        
        if (Hp <= 0)
        {
            GameManager.ins.L_Monster.Remove(gameObject);
            Destroy(gameObject);
        }
    }

}
