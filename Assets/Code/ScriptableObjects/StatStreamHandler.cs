using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

[CreateAssetMenu(menuName = "Items/Stream Handler", fileName = "New Stream Handler")]
public class StatStreamHandler : ScriptableObject
{
    Subject<StatValueDTO> statSubject = new Subject<StatValueDTO>();
    public IObservable<StatValueDTO> StatStream { get { return statSubject; } }

    public void PublishStatChange(StatValueDTO stat)
    {
        statSubject.OnNext(stat);
    }
}
