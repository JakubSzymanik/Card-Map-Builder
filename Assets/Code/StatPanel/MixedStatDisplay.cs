using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedStatDisplay : StatDisplay
{
    [SerializeField] private StatType maxType;

    int maxValue;
    Coroutine valueChange;
    Coroutine maxValueChange;
    
    public override bool HasType(StatType type)
    {
        return this.type == type || this.maxType == type;
    }
    public override void ChangeValue(StatValueDTO stat)
    {
        if (stat.Type == this.type)
        {
            if(valueChange != null) StopCoroutine(valueChange);
            valueChange = StartCoroutine(ValueChangeCR(stat.Value));
        }
        else
        {
            if(maxValueChange != null) StopCoroutine(maxValueChange);
            maxValueChange= StartCoroutine(MaxValueChangeCR(stat.Value));
        }
    }

    //not the best solution, would be better if there was ref int possibility in coroutine, maybe ill write something smoother later
    private IEnumerator ValueChangeCR(int target)
    {
        scaleTween = statValueText.transform.DOScale(Vector3.one * 1.2f, 0.2f);
        float waitTime = 0.5f / (float)(target - value);
        while (value != target)
        {
            value++;
            statValueText.text = value.ToString() + "/" + maxValue.ToString();
            yield return new WaitForSeconds(waitTime);
        }
        scaleTween = statValueText.transform.DOScale(Vector3.one, 0.2f);
    }
    private IEnumerator MaxValueChangeCR(int target)
    {
        scaleTween = statValueText.transform.DOScale(Vector3.one * 1.2f, 0.2f);
        float waitTime = 0.5f / (float)(target - maxValue);
        while (maxValue != target)
        {
            maxValue++;
            statValueText.text = value.ToString() + "/" + maxValue.ToString(); //repeat, change to method later
            yield return new WaitForSeconds(waitTime);
        }
        scaleTween = statValueText.transform.DOScale(Vector3.one, 0.2f);
    }
}
