using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UniRx;
using UniRx.Triggers;

public class FieldMarker : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    Tween spriteTween;
    bool active = false;

    public IObservable<Vector2Int> OnMouseDown { get { return posSubject; } }
    ISubject<Vector2Int> posSubject = new Subject<Vector2Int>();

    void Start()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0);

        this.OnMouseDownAsObservable()
            .Where(_=>active)
            .Subscribe(_ => posSubject.OnNext(GridPos.WorldToGridPos(transform.position)))
            .AddTo(this);
    }

    public void Show(bool visible)
    {
        if (visible)
        {
            spriteTween.Kill();
            spriteTween = spriteRenderer.DOFade(1, 0.3f);
            active = true;
        }
        else
        {
            spriteTween.Kill();
            spriteTween = spriteRenderer.DOFade(0, 0.3f);
            active = false;
        }
    }
}
