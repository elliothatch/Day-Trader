using UnityEngine;
using System.Collections;

public class ButtonObject : MonoBehaviour {

	public Texture2D unpressedTexture;
	public Texture2D pressedTexture;

	public delegate void ButtonObjectDelegate(OTObject owner);
	public ButtonObjectDelegate onPressed = null;
	public ButtonObjectDelegate onReleased = null;
	public ButtonObjectDelegate onActivate = null;

	private OTSprite sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pressButton()
	{
		if(onPressed != null)
			onPressed(GetComponent<OTSprite>());

		//sprite.image = pressedTexture;
		sprite.tintColor = new Color(1,0,1,1);

	}

	public void releaseButton()
	{
		if(onReleased != null)
			onReleased(GetComponent<OTSprite>());

		//sprite.image = unpressedTexture;
		sprite.tintColor = new Color(.1f,.1f,.4f,1f);
	}

	public void activateButton()
	{
		releaseButton();
		if(onActivate != null)
			onActivate(GetComponent<OTSprite>());
	}
}
