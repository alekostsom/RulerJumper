using UnityEngine;
using System.Collections;

public class MyGUI : MonoBehaviour {

	
    private string stringToEdit = "Hello World";
    void OnGUI()
    {   
        //GUILayout.TextField("distance: ", 25);
        GUI.TextField(new Rect(150, 10, 200, 20), stringToEdit);

        GUI.Label(new Rect(150, 40, 200, 20), stringToEdit);
    }

}
