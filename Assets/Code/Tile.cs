using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    public Vector2Int GridPosition { get { return GridPos.WorldToGridPos(transform.position); } }

    public IObservable<Tile> OnMouseDown { get { return tileSubject; } }
    ISubject<Tile> tileSubject = new Subject<Tile>();

    void Start()
    {
        this.OnMouseDownAsObservable().Subscribe(_ => tileSubject.OnNext(this));

        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f);
    }
}
