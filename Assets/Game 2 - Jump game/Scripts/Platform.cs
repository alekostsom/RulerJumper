using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public GameObject dot;
	Camera cam;

	public Transform dotTransform;
	Vector3 tmpPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		cam = Camera.main;

		dotTransform = dot.transform;
	}
	
	// Update is called once per frame
	void Update () {
		tmpPos = cam.WorldToScreenPoint(transform.position);
		tmpPos.x -= Screen.width / 2;
		tmpPos.y -= Screen.height /2;
		tmpPos.z = 0;

		dotTransform.localPosition = tmpPos + 75*Vector3.up;
	}
}
