using UnityEngine;
using System.Collections;

public class RulerIcon : MonoBehaviour {

	public GameObject ruler;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableRuler()
	{
		ruler.SetActive (!ruler.activeSelf);
	}
}
