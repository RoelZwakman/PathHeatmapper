using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PositionLogger : MonoBehaviour {

    public float timer;
    public float maxTimer;
    public bool logThisGO;

	private void FixedUpdate ()
    {
        timer += Time.deltaTime;
        if (logThisGO && timer >= maxTimer)
        {
            timer = 0;
            LogPosition();
        }
	}

    private void LogPosition()
    {
        string _positionAsString = transform.position.ToString();
        WriteTextLine("positionlog.txt", _positionAsString);
    }

    private void WriteTextLine(string _filename, string _data)
    {
        using (StreamWriter sw = File.AppendText(_filename))
        {
            sw.WriteLine(_data);
        }
          
    }

}
