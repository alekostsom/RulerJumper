using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Playermovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public bool isGrounded = false;

	public InputField input;
	public float distance;

	public Transform dotPrefab;
	public Transform canvas;
	Vector3 playerInitPosition, tmpPos;
	public Transform dot;

	public Ruler rulerObject;

	void Start()
	{
		dot = (Transform)Instantiate(dotPrefab, Vector3.zero, Quaternion.identity);
		dot.SetParent(canvas);
		dot.localScale = Vector3.one;
		tmpPos = Camera.main.WorldToScreenPoint(transform.position);
		tmpPos.x -= Screen.width/2;
		tmpPos.y -= Screen.height/2;
		tmpPos.z = 0;
		dot.localPosition = tmpPos;// + 25*Vector3.up;

		rulerObject.InitDot = dot;

		playerInitPosition = transform.position;
	}	

    void Update() {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0); //Set X and Z velocity to 0
 
        //transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, 0, 0);

        /*if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(); //Manual jumping
        }*/
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1.0f);
        if (isGrounded)
        {
            //if (Input.GetKeyUp(KeyCode.Space))
                //Jump(); //Automatic jumping
        }

		tmpPos = Camera.main.WorldToScreenPoint(playerInitPosition);
		tmpPos.x -= Screen.width/2;
		tmpPos.y -= Screen.height/2;
		tmpPos.z = 0;
		dot.localPosition = tmpPos;
	}

    void Jump()
    {
        if (!isGrounded) { return; }
        isGrounded = false;
		distance = float.Parse (input.text);
		Debug.Log (distance);
        rigidbody.velocity = new Vector3(0, 0, 0);
        rigidbody.AddForce(new Vector3(0, distance * 100, 0), ForceMode.Force);        
    }

    void FixedUpdate()
    {
        
    }
       

}
