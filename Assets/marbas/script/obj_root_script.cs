using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using static Unity.Burst.Intrinsics.X86.Avx;

public class obj_root_script : MonoBehaviour
{
    [SerializeField] private float obj_miss_speed;
    [SerializeField]private float dollyCartSpeed=8;
    [SerializeField] private CinemachineDollyCart[] obj_script;
    int tmp_count;
    [SerializeField] float miss_time;
    bool miss_bool;
    // Start is called before the first frame update
    void Start()
    {
        tmp_count = obj_script.Length;

        //最初は速度を0にして止めておく
        StopDollyCart();
    }

    //外部から起動をかけられるように
    public void StartDollyCaet()
    {
        for (int i = 0; i < tmp_count; i++)
        {
            obj_script[i].m_Speed = dollyCartSpeed;
        }
    }

    //カートの速度を0に
    public void StopDollyCart()
    {
        for (int i = 0; i < tmp_count; i++)
        {
            obj_script[i].m_Speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnTriggerEnter(Collider other)
    {

        if (!miss_bool)
        {
            Debug.Log("miss");
            StartCoroutine("miss_count");
        }
    }
    IEnumerator miss_count()
    {
        miss_bool = true;
        float tmp = obj_script[0].m_Speed;
        for (int i=0; i < tmp_count; i++)
        {
            obj_script[i].m_Speed = obj_miss_speed;
        }
        yield return new WaitForSeconds(miss_time);

        for (int i = 0; i < tmp_count; i++)
        {
            obj_script[i].m_Speed = tmp;
        }
        miss_bool = false;
    }
}
