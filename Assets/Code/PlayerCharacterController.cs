using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float movePauseTime;
    [SerializeField] float stepDuration;
    public Vector2Int GridPosition { get { return GridPos.WorldToGridPos(transform.position); } }

    Tween positionTween;

    //public IObservable<Vector2Int> OnActionFinished { get { return actionFinishedSubject; } }
    //ISubject<Vector2Int> actionFinishedSubject = new Subject<Vector2Int>();

    void Start()
    {
        
    }

    public void Action(Tile targetTile, Enemy targetEnemy, Action actionFinishedCallback)
    {
        //positionTween.kill() ?
        if(targetEnemy == null)
        {
            Vector3 target = GridPos.GridToWorldPos(targetTile.GridPosition);
            positionTween = transform.DOMove(target, stepDuration)
                .OnComplete(() => ActionFinished(actionFinishedCallback));
        }
        else
        {
            //fight the enemy
        }
    }
    void ActionFinished(Action actionFinishedCallback)
    {
        //add effect to stack
        //field.triggerEffect
        //fightEnemy
        print("Action completed");
        actionFinishedCallback();
    }

    //public void MoveToTarget(Vector2Int targetGridPos)
    //{
    //    Vector3 target = new Vector3(targetGridPos.x + 0.5f, targetGridPos.y + 0.5f, 0);
    //    Vector2 direction = target - transform.position;
    //    direction = direction.normalized;

    //    int distance = Mathf.RoundToInt(Vector3.Distance(transform.position, target));
    //    List<Vector2> targets = new List<Vector2>();
    //    for(int i = 1; i <= distance; i++)
    //    {
    //        targets.Add((Vector2)transform.position + i * direction);
    //    }
    //    StartCoroutine(MoveToTargetCR(targets));
    //}

    //IEnumerator MoveToTargetCR(List<Vector2> targets)
    //{
    //    for(int i = 0; i < targets.Count; i++)
    //    {
    //        var target = targets[i];
    //        yield return StartCoroutine(MoveCR(target));
    //        yield return new WaitForSeconds(movePauseTime);
    //    }
    //}
    //IEnumerator MoveCR(Vector2 target)
    //{
    //    while(Vector2.Distance(transform.position, target) > 0.01f)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    //        yield return null;
    //    }
    //}
}
