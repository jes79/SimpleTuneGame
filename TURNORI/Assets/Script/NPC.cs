using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject can;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        can.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        can.SetActive(false);
    }




}
