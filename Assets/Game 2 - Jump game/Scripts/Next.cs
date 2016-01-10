using UnityEngine;
using System.Collections;

public class Next : MonoBehaviour {

	private Transform curPlat;
	public Transform CurPlat {
		set { curPlat = value; }
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (curPlat != null) {
			Vector3 tmpPos = Camera.main.WorldToScreenPoint (curPlat.position);

			tmpPos.x -= 18;
			tmpPos.y -= Screen.width / 20;
			tmpPos.z = 0;
			transform.position = tmpPos;
		}
		else 
		{
			Vector3 tmpPos = Camera.main.WorldToScreenPoint (curPlat.position);
			
			tmpPos.x -= 18;
			tmpPos.y -= Screen.width / 20;
			tmpPos.z = 0;
			transform.position = tmpPos;
		}
	}
}
