using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fight : MonoBehaviour
{
    public Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("fade").GetComponent<Fade>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 0f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            //Debug.Log("충돌");
            StartCoroutine("FadeAction");
            
            
        }        
    }
    IEnumerator FadeAction()
    {
        int r = Random.Range(1, 3);
        fade.FadeIn2(0.5f);
        yield return new WaitForSeconds(1);
        fade.FadeOut2(0.5f);
        float px =  transform.position.x;
        float py = transform.position.y;
        PlayerPrefs.SetFloat("PlayerX", px);
        PlayerPrefs.SetFloat("PlayerY", py);
        SceneManager.LoadScene("Stage"+ r);
    }




}
