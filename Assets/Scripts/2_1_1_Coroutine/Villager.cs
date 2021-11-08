using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一个角色有“饱食度”和“困倦度”两个属性，这两个属性会不断衰减
/// 当角色饿了会自己寻找食物，困了会自己去睡觉
/// 睡觉优先级较高
/// </summary>
public class Villager : MonoBehaviour
{
    const float FATIGUE_DEFAULT_VALUE = 5f;  // 疲倦默认值
    const float SATIATION_DEFAULT_VALUE = 5f;  // 饱食度默认值
    const float FATIGUE_MIN_VALUE = 0.2f;  // 疲倦最小值
    const float SATIATION_MIN_VALUE = 0.2f;  // 饱食度最小值
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
            // 饱食度下降
            mSatiation = Mathf.Max(0, mSatiation - Time.deltaTime);
            Debug.LogFormat("Satiation: {0}", mSatiation);

            // 疲倦度下降
            mFatigue = Mathf.Max(0, mFatigue - Time.deltaTime);
            Debug.LogFormat("Fatigue: {0}", mFatigue);

            if (mSatiation <= SATIATION_MIN_VALUE && mActionCoroutine == null)
                mActionCoroutine = StartCoroutine(EatFood());  // 切换吃食物行为

            if (mFatigue <= FATIGUE_MIN_VALUE)
                mActionCoroutine = StartCoroutine(Sleep());  // 切换睡觉行为
            yield return null;
        }
    }

    private IEnumerator EatFood()
    {
        // 自定义逻辑处理，此处省略
        yield return null;
        Debug.Log("Eating");
        mSatiation = SATIATION_DEFAULT_VALUE;
        mActionCoroutine = null;
    }

    private IEnumerator Sleep()
    {
        StopCoroutine(mActionCoroutine);  // 睡觉优先级最高
        yield return null;
        // 自定义逻辑处理，此处省略
        Debug.Log("Sleeping");
        mFatigue = FATIGUE_DEFAULT_VALUE;
        mActionCoroutine = null;
    }
}
