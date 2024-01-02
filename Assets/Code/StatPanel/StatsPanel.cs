using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] List<StatDisplay> statsPanels = new List<StatDisplay>();
    [SerializeField] StatStreamHandler streamHandler;

    private void Start()
    {
        streamHandler.StatStream.Subscribe(v => ChangeValue(v));
    }

    void ChangeValue(StatValueDTO statDTO)
    {
        StatDisplay display = statsPanels.Find(v => v.Type == statDTO.Type);
        if (display == null) return; //print error
        display.ChangeValue(statDTO.Value);
    }
}
