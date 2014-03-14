using UnityEngine;
using System.Collections;

public class ComputerCursor : MonoBehaviour {

	public OTSprite computerMouse;
	public OTSprite computerScreen;

	private OTSprite sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		//sprite.worldBounds = new Rect(computerScreen.size.x + sprite.size.x/2, computerScreen.size.y + sprite.size.y/2,
		//							computerScreen.size.x, computerScreen.size.y);
	}
	
	// Update is called once per frame
	void Update () {
		sprite.position = computerScreen.position - computerScreen.size/2 + sprite.size/2 + 
							computerMouse.position - new Vector2(computerMouse.worldBounds.x, computerMouse.worldBounds.y);
	}
}
