using UnityEngine;
using System.Collections;

public class ClampToMouse : MonoBehaviour {

	public bool clampToView = false;
	public float sensitivityX = 20F;
	public float sensitivityY = 20F;

	private OTSprite sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(transform.position.x);
		if(!Screen.lockCursor && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
		{
			Screen.lockCursor = true;
			transform.position = Input.mousePosition;
		}
		if(Screen.lockCursor)
			transform.Translate(Input.GetAxis("Mouse X") * sensitivityX, Input.GetAxis("Mouse Y") * sensitivityY, 0);

		if(clampToView)
		{
			float viewX = OT.view.position.x - OT.view.pixelPerfectResolution.x / 2;
			float viewY = OT.view.position.y - OT.view.pixelPerfectResolution.y / 2;
			float clampedX = sprite.position.x;
			float clampedY = sprite.position.y;
			if(sprite.position.x < viewX)
				clampedX = viewX;
			else if(sprite.position.x > viewX + OT.view.pixelPerfectResolution.x)
				clampedX = viewX + OT.view.pixelPerfectResolution.x;
			
			if(sprite.position.y < viewY)
				clampedY = viewY;
			else if(sprite.position.y > viewY + OT.view.pixelPerfectResolution.y)
				clampedY = viewY + OT.view.pixelPerfectResolution.y;

			sprite.position = new Vector2(clampedX, clampedY);
		}
		
	}
}
