using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statValueText;
    int value;

    Tween scaleTween;

    int pressCounter = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeValue(pressCounter * 4);
            pressCounter++;
        }
    }

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
