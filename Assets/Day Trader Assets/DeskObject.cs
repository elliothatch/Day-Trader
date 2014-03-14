using UnityEngine;
using System.Collections;

public class DeskObject : MonoBehaviour {


	public Texture2D cursorHeldTexture; //texture that replaces the cursor when this object is held
	public Texture2D cursorActivatedTexture; //replaces cursor when this object is held and activated (on left click)

	public delegate void DeskObjectDelegate(OTObject owner);
	public DeskObjectDelegate onActivate = null;

	public bool isHeld = false;

	private OTSprite sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isHeld && Input.GetMouseButtonDown(0))
		{
			if(onActivate != null)
				onActivate(GetComponent<OTSprite>());
		}
	}

	public void pickUp()
	{
		//sprite.alpha = 0.0f;
		isHeld = true;
	}

	public void drop()
	{
		//sprite.alpha = 1.0f;
		isHeld = false;
	}
}
