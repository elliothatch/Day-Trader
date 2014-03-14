using UnityEngine;
using System.Collections;

public class ViewEdgePan : MonoBehaviour {

	public GameObject targetObject = null;
	public bool panObject = false;
	public Vector2 minEdge = new Vector2(100.0f,100.0f);
	public Vector2 maxEdge = new Vector2(Screen.width - 100.0f, Screen.height - 100.0f);
	public Vector2 sensitivity = new Vector2(.05f, .05f);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 screenTarget;
		if(targetObject)
			screenTarget = (Vector2)targetObject.transform.position - GetComponent<OTView>().position;
		else
			screenTarget = Input.mousePosition;

		//Debug.Log(screenTarget);

		Vector2 deltaPosition = new Vector2(0.0f, 0.0f);

		if(screenTarget.x <= minEdge.x)
		{
			deltaPosition += new Vector2(sensitivity.x * (screenTarget.x - minEdge.x), 0.0f);
		}
		if(screenTarget.x >= maxEdge.x)
		{
			deltaPosition += new Vector2(sensitivity.x * (screenTarget.x - maxEdge.x), 0.0f);
		}
		if(screenTarget.y <= minEdge.y)
		{
			deltaPosition += new Vector2(0.0f, sensitivity.y * (screenTarget.y - minEdge.y));
		}
		if(screenTarget.y >= maxEdge.y)
		{
			deltaPosition += new Vector2(0.0f, sensitivity.y * (screenTarget.y - maxEdge.y));
		}

		GetComponent<OTView>().position += deltaPosition;

		if(panObject && targetObject)
		{
			float objectPanX = deltaPosition.x;
			float objectPanY = deltaPosition.y;
			float viewX = GetComponent<OTView>().position.x - GetComponent<OTView>().pixelPerfectResolution.x / 2;

			if(viewX < GetComponent<OTView>().worldBounds.x || 
				viewX + GetComponent<OTView>().pixelPerfectResolution.x > GetComponent<OTView>().worldBounds.x + GetComponent<OTView>().worldBounds.width)
			{
				objectPanX = 0;
			}
			
			float viewY = GetComponent<OTView>().position.y - GetComponent<OTView>().pixelPerfectResolution.y / 2;
			if(viewY < GetComponent<OTView>().worldBounds.y || 
				viewY + GetComponent<OTView>().pixelPerfectResolution.y > GetComponent<OTView>().worldBounds.y + GetComponent<OTView>().worldBounds.height)
			{
				objectPanY = 0;
			}

			targetObject.transform.position += new Vector3(objectPanX, objectPanY, 0.0f);
		}



	}
}
