using System.Text;
using TMPro;
using UnityEngine;

public class LogViewer : MonoBehaviour
{
    [SerializeField] private int _logCount = 10;
    [SerializeField] private TMP_Text _view;

    private string[] _logs;
    private int _index = -1;
    private StringBuilder _logBuilder = new StringBuilder();

    private void Start()
    {
        _logs = new string[_logCount];

        Application.logMessageReceived += OnLogReceived;
    }

    private void UpdateView()
    {
        _logBuilder.Clear();

        int index = _index;
        for (int i = 0; i < _logCount; i++)
        {
            if (_logs[index] == null)
            {
                index = (index + 1) % _logCount;
                continue;
            }

            _logBuilder.AppendLine(_logs[index]);

            index = (index + 1) % _logCount;
        }

        _view.text = _logBuilder.ToString();
    }

    private void OnLogReceived(string condition, string stackTrace, LogType type)
    {
        if (type != LogType.Log) return;

        _index = (_index + 1) % _logCount;
        _logs[_index] = condition;

        UpdateView();
    }
}