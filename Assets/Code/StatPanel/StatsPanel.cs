using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] List<StatDisplay> statsPanels = new List<StatDisplay>();
    [SerializeField] StatStreamHandler streamHandler;

    private void Awake()
    {
        streamHandler.StatStream.Subscribe(v => ChangeValue(v));
    }

    void ChangeValue(StatValueDTO statDTO)
    {
        StatDisplay display = statsPanels.Find(v => v.HasType(statDTO.Type));
        if (display == null) 
            return; //print error
        display.ChangeValue(statDTO);
    }
}
