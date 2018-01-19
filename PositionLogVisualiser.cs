using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PositionLogVisualiser : EditorWindow {

    private List<Vector3> _posList = new List<Vector3>(1);
    private bool _showVisualisation = false;
    private float _discSize = 2;

    [MenuItem("Position logger/Position log visualiser")]
    private static void Init()
    {
        PositionLogVisualiser window = (PositionLogVisualiser)EditorWindow.GetWindow(typeof(PositionLogVisualiser));
        window.Show();
        

    }


    private void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (_showVisualisation)
        {
            Handles.color = new Color(1, 1, 1, 0.05f);
            for (int i = 0; i < _posList.Count; i++)
            {
                Handles.DrawSolidDisc(_posList[i], Vector3.up, _discSize);
            }
        }
        
    }

    private void OnGUI()
    {
        if (GUILayout.Button("VISUALISE POSITION LOG AS HEATMAP"))
        {
            VisualisePositionLog("positionlog.txt");
            _showVisualisation = true;
            SceneView.onSceneGUIDelegate = this.OnSceneGUI;
        }


        GUILayout.Label("Circle size");
        _discSize = EditorGUILayout.FloatField(_discSize);
    }	

    

    private void VisualisePositionLog(string _poslogfilename)
    {
        using (StreamReader sr = File.OpenText(_poslogfilename))
        {
            string _stringVec3 = " ";
            while(sr.Peek() >= 0) /////while there is still a new line to be read, add a parsed Vector3 to the end of the _posList
            {
                _stringVec3 = sr.ReadLine();
                _posList.Add(ParseStringToVector3(_stringVec3));
            }
        }
        _showVisualisation = true;
    }

    private Vector3 ParseStringToVector3(string _vector3AsString)
    {
        Vector3 _parsedVector3 = new Vector3(0, 0, 0);
        float x = 0;
        float y = 0;
        float z = 0;

        _vector3AsString = _vector3AsString.Remove(0, 2);

        string xSubstring = _vector3AsString.Substring(0, _vector3AsString.IndexOf(",")); ////makes a new string from everything from 0 to the first comma left over
        x = float.Parse(xSubstring);

        _vector3AsString = _vector3AsString.Remove(0, xSubstring.Length + 1);
        string ySubstring = _vector3AsString.Substring(0, _vector3AsString.IndexOf(",")); ////makes a new string from everything from 0 to the first comma left over
        y = float.Parse(ySubstring);

        _vector3AsString = _vector3AsString.Remove(0, ySubstring.Length + 1);
        string zSubstring = _vector3AsString.Substring(0, _vector3AsString.IndexOf(")")); ////makes a new string from everything from 0 to the first ) left over
        z = float.Parse(zSubstring);

        _parsedVector3.x = x;
        _parsedVector3.y = y;
        _parsedVector3.z = z;
        return _parsedVector3;
    }

}