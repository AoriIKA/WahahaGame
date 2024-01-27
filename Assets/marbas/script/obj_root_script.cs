using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using static Unity.Burst.Intrinsics.X86.Avx;

public class obj_root_script : MonoBehaviour
{
    [SerializeField] private float obj_miss_speed;
    [SerializeField] private CinemachineDollyCart[] obj_script;
    int tmp_count;
    [SerializeField] float miss_time;
    // Start is called before the first frame update
    void Start()
    {
        tmp_count = obj_script.Length;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("miss");

        StartCoroutine("miss_count");
    }
    IEnumerator miss_count()
    {
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
    }
}
