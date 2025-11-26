using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawn : MonoBehaviour
{
    public GameObject Fight;
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i<3; i++)
        {
            Vector3 ran = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
            GameObject go =  Instantiate(Fight, transform.position + ran, Quaternion.identity);
            go.transform.SetParent(transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
