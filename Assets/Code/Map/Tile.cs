using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    public Card card { get; private set; }
    public Vector2Int GridPosition { get { return GridUtility.WorldToGridPos(transform.position); } }

    public IObservable<Tile> OnMouseDown { get { return this.OnMouseDownAsObservable().Select(_=>this); } }

    void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f);

        //this.OnMouseEnterAsObservable().Subscribe(v =>
        //{ 
        //    hoverStartTime = Time.time; 
        //}).AddTo(this);
        //this.OnMouseOverAsObservable().Subscribe(v =>
        //{
        //    if (hoverStartTime > 0 && Time.time - hoverStartTime >= 0.5f) tileDetails.gameObject.SetActive(true);
        //}).AddTo(this);
        //this.OnMouseExitAsObservable().Subscribe(v =>
        //{
        //    hoverStartTime = -1;
        //    tileDetails.gameObject.SetActive(false);
        //}).AddTo(this);
    }

    public void Create(Card card)
    {
        this.card = card;
    }
}
