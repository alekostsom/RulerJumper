using UnityEngine;
using System.Collections;

public class Ruler : MonoBehaviour {
	private Transform curPlat;
	public Transform CurPlat {
		set { curPlat = value; }
	}

	private Transform nextPlat;
	public Transform NextPlat {
		set { nextPlat = value; }
	}

	Transform initDot;
	public Transform InitDot
	{
		get { return initDot;}
		set { initDot = value;}
	}

	float correctDistance = 0.0f;
	public float CorrectDistance
	{
		get { return correctDistance;}
	}

	// Use this for initialization
	void Start () {
		if (initDot != null) {
			Vector3 tmpPos = (initDot.position);

			tmpPos.z = 0;
			transform.position = tmpPos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (curPlat != null) {
			Vector3 tmpPos = curPlat.GetComponent<Platform> ().dot.transform.position;

			tmpPos.z = 0;
			transform.position = tmpPos;
		}
		else if (initDot != null) {
			Vector3 tmpPos = (initDot.position);

			tmpPos.z = 0;
			transform.position = tmpPos;

			if (nextPlat != null) {
				correctDistance = 2*Vector3.Distance (nextPlat.GetComponent<Platform> ().dot.transform.position, initDot.position)/100;
			}
		}

		if (nextPlat != null) {
			Vector3 vectorToTarget = nextPlat.GetComponent<Platform> ().dot.transform.position - transform.position;
			float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * 10);

			if (curPlat != null)
			{
				correctDistance = 2*Vector3.Distance (nextPlat.GetComponent<Platform> ().dot.transform.position, curPlat.GetComponent<Platform> ().dot.transform.position)/100;
			}
			Debug.DrawRay (transform.position, /*Camera.main.WorldToScreenPoint*/(nextPlat.GetComponent<Platform> ().dot.transform.position) - /*Camera.main.WorldToScreenPoint */(transform.position), Color.green);
		}
	}	
}
