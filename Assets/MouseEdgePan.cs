using UnityEngine;
using System.Collections;

public class MouseEdgePan : MonoBehaviour {

	public Vector2 minEdge = new Vector2(100.0f,100.0f);
	public Vector2 maxEdge = new Vector2(Screen.width - 100.0f, Screen.height - 100.0f);
	public Vector2 sensitivity = new Vector2(.05f, .05f);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(Input.mousePosition);
		if(Input.mousePosition.x <= minEdge.x)
		{
			GetComponent<OTView>().position += new Vector2(sensitivity.x * (Input.mousePosition.x - minEdge.x), 0.0f);
		}
		if(Input.mousePosition.x >= maxEdge.x)
		{
			GetComponent<OTView>().position += new Vector2(sensitivity.x * (Input.mousePosition.x - maxEdge.x), 0.0f);
		}
		if(Input.mousePosition.y <= minEdge.y)
		{

		}
		if(Input.mousePosition.y >= maxEdge.y)
		{

		}
	}
}
