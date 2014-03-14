using UnityEngine;
using System.Collections;

public class PlayerCursor : MonoBehaviour {

	public Texture2D defaultTexture;
	public Texture2D deskObjectHoverTexture;
	public Texture2D buttonHoverTexture;
	public Texture2D buttonPressedTexture;

	private OTSprite sprite;
	private OTObject hoverObject = null;
	private OTObject heldObject = null;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		sprite.onCollision = OnCollision;
		sprite.onStay = OnStay;
		sprite.onExit = OnExit;
		sprite.tintColor = new Color(0,1,1,1);
	}
	
	// Update is called once per frame
	void Update () {

		//grab object
		if(hoverObject && Input.GetMouseButtonDown(1))
		{
			if(hoverObject.GetComponent<DeskObject>())
			{
				heldObject = hoverObject;
				heldObject.GetComponent<DeskObject>().pickUp();
				sprite.worldBounds = heldObject.worldBounds;
				//sprite.image = heldObject.GetComponent<DeskObject>().cursorHeldTexture;
				sprite.tintColor = new Color(0,1,0,1);
			}
		}

		//press button
		if(hoverObject && Input.GetMouseButtonDown(0))
		{
			if(hoverObject.GetComponent<ButtonObject>())
			{
				hoverObject.GetComponent<ButtonObject>().pressButton();
				heldObject = hoverObject;
				//sprite.image = buttonPressedTexture;
				sprite.tintColor = new Color(1,.5f,.5f,1);
			}
		}

		//drop object
		if(heldObject && Input.GetMouseButtonUp(1))
		{
			if(heldObject.GetComponent<DeskObject>())
			{
				heldObject.GetComponent<DeskObject>().drop();
				setHoverObject(heldObject);
				heldObject = null;
				sprite.worldBounds = OT.view.worldBounds;
				sprite.tintColor = new Color(1,0,0,1);
			}
		}

		//release button
		if(heldObject && Input.GetMouseButtonUp(0))
		{
			if(heldObject.GetComponent<ButtonObject>())
			{
				if(hoverObject == heldObject)
				{
					heldObject.GetComponent<ButtonObject>().activateButton();
					heldObject = null;
					setHoverObject(hoverObject);
				}
				else
				{
					heldObject.GetComponent<ButtonObject>().releaseButton();
					heldObject = null;
					setHoverObject(null);
				}
			}
		}

		if(heldObject && heldObject.GetComponent<DeskObject>())
		{
			heldObject.position = sprite.position;
		}

	}

	void OnCollision(OTObject owner)
	{
		/*
		if(owner.collisionObject.GetComponent<DeskObject>() != null)
			setHoverObject(owner.collisionObject);
		if(owner.collisionObject.GetComponent<ButtonObject>() != null)
			setHoverObject(owner.collisionObject);
		*/
	}

	void OnStay(OTObject owner)
	{
		if(!hoverObject && owner.collisionObject != heldObject)
		{
			if(owner.collisionObject.GetComponent<DeskObject>() != null || owner.collisionObject.GetComponent<ButtonObject>() != null)
			{
				setHoverObject(owner.collisionObject);
			}
		}
	}

	void OnExit(OTObject owner)
	{
		if(owner.collisionObject.GetComponent<DeskObject>() != null)
		{
			setHoverObject(null);
		}
		if(owner.collisionObject.GetComponent<ButtonObject>() != null)
		{
			setHoverObject(null);
		}
	}

	void setHoverObject(OTObject target)
	{
		hoverObject = target;
		if(heldObject)
			return;
		if(target == null)
		{
			//sprite.image = defaultTexture;
			sprite.tintColor = new Color(0,1,1,1);
			return;
		}
		if(target.GetComponent<DeskObject>() != null)
		{
			//sprite.image = deskObjectHoverTexture;
			sprite.tintColor = new Color(1,0,0,1);
		}
		if(target.GetComponent<ButtonObject>() != null)
		{
			//sprite.image = buttonHoverTexture;
			sprite.tintColor = new Color(1,1,0,1);
		}
	}
}
