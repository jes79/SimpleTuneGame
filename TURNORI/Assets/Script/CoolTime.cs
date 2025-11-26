using UnityEngine;

public class CoolTime 
{
    public float Cooltime;
    float CoolCnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        CoolCnt = 0;
    }
    // Update is called once per frame
    public float Timer(float t)
    {
        
        if (CoolCnt + t <= Time.time)
        {
            CoolCnt = Time.time;
            Cooltime = 0;            
        }
        Cooltime += Time.deltaTime;
        return Cooltime;
    }
}
