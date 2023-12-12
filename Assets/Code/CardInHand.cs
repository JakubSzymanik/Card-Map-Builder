using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;
using UniRx.Triggers;

public class CardInHand : MonoBehaviour
{
    [SerializeField] SpriteRenderer image;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] SpriteRenderer highlight;
    public Card Card { get; private set; }
    Tween scaleTween;
    Tween moveTween;
    Tween highlightColorTween;
    bool selectable = true;

    public IObservable<CardInHand> OnMouseDown { get { return cardSubject; } }
    ISubject<CardInHand> cardSubject = new Subject<CardInHand>();

    void Start()
    {
        this.OnMouseDownAsObservable().Where(_ => selectable).Subscribe(_ => cardSubject.OnNext(this));
    }
    public void Destroy()
    {
        selectable = false;
        highlightColorTween.Kill();
        highlightColorTween = highlight.DOFade(0.1f, 0);
        scaleTween.Kill();
        scaleTween = transform.DOScale(0, 0.2f).OnComplete(() => 
        {
            cardSubject.OnCompleted();
            Destroy(this.gameObject);
        });
    }
    public void Create(Card card, Vector3 position)
    {
        transform.position = position;
        transform.localScale = Vector3.zero;
        
        this.Card = card;
        image.sprite = card.Image;
        description.text = card.Description;

        scaleTween = transform.DOScale(1, 0.3f);
    }
    public void MoveTo(Vector3 position)
    {
        moveTween.Kill();
        moveTween = transform.DOMove(position, 0.3f);
    }
    public void Highlight(bool isHighlighted)
    {
        if(isHighlighted)
        {
            highlightColorTween.Kill();
            highlightColorTween = highlight.DOFade(0.5f, 0.3f);
        }
        else
        {
            highlightColorTween.Kill();
            highlightColorTween = highlight.DOFade(0f, 0.3f);
        }
    }

    private void OnMouseEnter()
    {
        if (!selectable) return;
        scaleTween.Kill();
        scaleTween = transform.DOScale(1.25f, 0.3f);
    }
    private void OnMouseExit()
    {
        if(!selectable) return;
        scaleTween.Kill();
        scaleTween = transform.DOScale(1f, 0.3f);
    }
}
