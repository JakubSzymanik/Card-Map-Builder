using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;

public class HeroMover : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] float pauseTime;

    public IObservable<bool> OnActionFinished { get { return OnActionFinishedSubject; } }
    Subject<bool> OnActionFinishedSubject = new Subject<bool>();

    Tween positionTween;

    public void Move(Vector2Int pos)
    {
        positionTween.Kill();
        positionTween = transform.DOMove(GridUtility.GridToWorldPos(pos), moveTime)
            .OnComplete(()=> OnActionFinishedSubject.OnNext(true));
    }

    public void Fight(Enemy enemy)
    {

    }
}
