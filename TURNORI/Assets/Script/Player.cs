using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData Pdata;
    GameObject[] Monster;
    Rigidbody2D rig;

    public bool Back = false;
    public Vector3 OriPos;

    Animator ani;


    //마법진
    public GameObject MagicAura;
    public Transform T_MagicAura;
    //마법공격
    public GameObject Explosion;


    //데미지 캔버스
    public GameObject DamegeCanvas;
    TextMeshProUGUI TMPdamege;

    // Start is called before the first frame update
    void Start()
    {
        Monster = GameObject.FindGameObjectsWithTag("Monster");
        rig = GetComponent<Rigidbody2D>();
        OriPos = transform.position;
        ani = GetComponent<Animator>();
        
    }

    public void NomalAttack()
    {
        if(GameManager.ins.CurrTurn==false)
            StartCoroutine("NomalAttackCT");
    }
    IEnumerator NomalAttackCT()
    {
        Back = false;
        int r = Random.Range(0, Monster.Length);
        while (true)
        {
            
            //rig.MovePosition(Vector3.MoveTowards(transform.position, Monster[0].transform.position, 100 * Time.deltaTime));
            if(Monster[r] != null)
            {
                rig.MovePosition(Vector3.Lerp(transform.position, Monster[r].transform.position, 20 * Time.deltaTime));
                if (Vector3.Distance(transform.position, Monster[r].transform.position) <= 0.5f)
                {
                    ani.SetTrigger("attack");
                    Sound();
                    Monster[r].GetComponent<Monster>().Damege(Pdata.Attack);
                    yield return new WaitForSeconds(0.3f);
                    Back = true;

                    break;
                }               
            }
            yield return null;
        }
    }

    void Sound()
    {
        if(Pdata.Job== "검사")
        {
            SoundManager.instance.PlayAttackSound(8);
        }

        if (Pdata.Job == "신관")
        {
            SoundManager.instance.PlayAttackSound(4);
        }

        if (Pdata.Job == "마법사")
        {
            SoundManager.instance.PlayAttackSound(2);
        }
    }


    public void SpecialAttack()
    {
        if (GameManager.ins.CurrTurn == false)
            StartCoroutine("SpecialAttackCT");
    }

    IEnumerator SpecialAttackCT()
    {
        int r = Random.Range(0, Monster.Length);  

        Instantiate(MagicAura, T_MagicAura.position, T_MagicAura.rotation);
        yield return new WaitForSeconds(2.5f);
        if (Monster[r] != null)
        {
            if (!Pdata.Job.Equals("신관"))
            {
                Instantiate(Explosion, Monster[r].transform.position + Vector3.up * 0.8f, Quaternion.identity);
            }
            else
            {
                GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");
                int i = Random.Range(0, Player.Length);
                Instantiate(Explosion, Player[i].transform.position + Vector3.up * 0.8f, Quaternion.identity);
            }
        }

    }





    // Update is called once per frame
    void Update()
    {
        if(Back == true)
        {
            //rig.MovePosition(Vector3.MoveTowards(transform.position, OriPos, 100 * Time.deltaTime));
            rig.MovePosition(Vector3.Lerp(transform.position, OriPos, 20 * Time.deltaTime));
            
        }


    }


    public void Damege(int Attack)
    {
        Pdata.Hp -= Attack;
        ani.SetTrigger("damege");
        GameObject go = Instantiate(DamegeCanvas, transform.position, Quaternion.identity);
        //캔버스에 몬스터를 부모로하겠다.
        go.transform.SetParent(transform);
   
        TMPdamege = go.GetComponentInChildren<TextMeshProUGUI>();
        TMPdamege.text = "" + Attack;
        if (Pdata.Hp <=0)
        {            
            GameManager.ins.D_Player.Remove(Pdata.Job);
            Destroy(gameObject);
        }
    }


}
