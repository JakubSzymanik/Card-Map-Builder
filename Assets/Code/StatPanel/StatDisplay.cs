using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] private StatType type;
    [SerializeField] private TextMeshProUGUI statValueText;
    
    public StatType Type { get { return type; } }

    int value;
    Tween scaleTween;

    public void ChangeValue(int target)
    {
        StopAllCoroutines();
        StartCoroutine(ValueChangeCR(target));
    }

    IEnumerator ValueChangeCR(int target)
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
