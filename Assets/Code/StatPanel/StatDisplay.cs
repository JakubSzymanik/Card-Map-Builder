using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] protected StatType type;
    [SerializeField] protected TextMeshProUGUI statValueText;

    protected int value;
    protected Tween scaleTween;

    public virtual bool HasType(StatType type)
    {
        return type == this.type;
    }
    public virtual void ChangeValue(StatValueDTO stat)
    {
        StopAllCoroutines();
        StartCoroutine(ValueChangeCR(stat.Value));
    }

    private IEnumerator ValueChangeCR(int target)
    {
        scaleTween = statValueText.transform.DOScale(Vector3.one * 1.2f, 0.2f);
        float waitTime = 0.5f / (float)(target - value);
        while (value != target)
        {
            value++;
            statValueText.text = value.ToString();       
            yield return new WaitForSeconds(waitTime);
        }
        scaleTween = statValueText.transform.DOScale(Vector3.one, 0.2f);
    }
}
