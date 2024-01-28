using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using static Unity.Burst.Intrinsics.X86.Avx;
using DG.Tweening;

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

        //�ŏ��͑��x��0�ɂ��Ď~�߂Ă���
        StopDollyCart();
    }

    //�O������N������������悤��
    public void StartDollyCaet()
    {
        for (int i = 0; i < tmp_count; i++)
        {
            obj_script[i].m_Speed = dollyCartSpeed;
        }
    }

    //�J�[�g�̑��x��0��
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
        float lMissspeed = obj_miss_speed;

        DOTween.To(
            () => lMissspeed,          // ����Ώۂɂ���̂�
            num => lMissspeed = num,   // �l�̍X�V
            0,                  // �ŏI�I�Ȓl
            0.5f                  // �A�j���[�V��������
        ).OnUpdate(() => {
            // �Ώۂ̒l���ύX�����x�ɂ�΂��
            for (int i = 0; i < tmp_count; i++)
            {
                obj_script[i].m_Speed = lMissspeed;
            }
        }); 

       
        yield return new WaitForSeconds(miss_time-0.4f);

        for (int i = 0; i < tmp_count; i++)
        {
            obj_script[i].m_Speed = tmp;
        }
        miss_bool = false;
    }
}
