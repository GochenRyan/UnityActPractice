using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ����ɫ�С���ʳ�ȡ��͡�����ȡ��������ԣ����������Ի᲻��˥��
/// ����ɫ���˻��Լ�Ѱ��ʳ����˻��Լ�ȥ˯��
/// ˯�����ȼ��ϸ�
/// </summary>
public class Villager : MonoBehaviour
{
    const float FATIGUE_DEFAULT_VALUE = 5f;  // ƣ��Ĭ��ֵ
    const float SATIATION_DEFAULT_VALUE = 5f;  // ��ʳ��Ĭ��ֵ
    const float FATIGUE_MIN_VALUE = 0.2f;  // ƣ����Сֵ
    const float SATIATION_MIN_VALUE = 0.2f;  // ��ʳ����Сֵ
    float mSatiation;
    float mFatigue;
    Coroutine mActionCoroutine;
    private void OnEnable()
    {
        mSatiation = SATIATION_DEFAULT_VALUE;
        mFatigue = FATIGUE_DEFAULT_VALUE;
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        while(true)
        {
            // ��ʳ���½�
            mSatiation = Mathf.Max(0, mSatiation - Time.deltaTime);
            Debug.LogFormat("Satiation: {0}", mSatiation);

            // ƣ����½�
            mFatigue = Mathf.Max(0, mFatigue - Time.deltaTime);
            Debug.LogFormat("Fatigue: {0}", mFatigue);

            if (mSatiation <= SATIATION_MIN_VALUE && mActionCoroutine == null)
                mActionCoroutine = StartCoroutine(EatFood());  // �л���ʳ����Ϊ

            if (mFatigue <= FATIGUE_MIN_VALUE)
                mActionCoroutine = StartCoroutine(Sleep());  // �л�˯����Ϊ
            yield return null;
        }
    }

    private IEnumerator EatFood()
    {
        // �Զ����߼������˴�ʡ��
        yield return null;
        Debug.Log("Eating");
        mSatiation = SATIATION_DEFAULT_VALUE;
        mActionCoroutine = null;
    }

    private IEnumerator Sleep()
    {
        StopCoroutine(mActionCoroutine);  // ˯�����ȼ����
        yield return null;
        // �Զ����߼������˴�ʡ��
        Debug.Log("Sleeping");
        mFatigue = FATIGUE_DEFAULT_VALUE;
        mActionCoroutine = null;
    }
}
