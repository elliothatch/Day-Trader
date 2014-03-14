using UnityEngine;
using System.Collections;

public class DeskComputer : MonoBehaviour {

	public OTSprite cursor;
	public OTSprite screen;
	public OTSprite screenBlack;
	public ButtonObject powerButton;


	private bool isOn = false;
	private int turnOnState = 0;
	private float turnOnTimer = 0.0f;
	private float turnOnTimerDelay = 0.0f;
	// Use this for initialization
	void Start () {
	
		powerButton.onActivate = togglePower;
	}
	
	// Update is called once per frame
	void Update () {

		if(!isOn)
			return;
		if(turnOnState > 0)
			updateTurnOn();
	}

	public void togglePower(OTObject owner)
	{
		if(!isOn)
			turnOn();
		else
			turnOff();
	}

	public void turnOn()
	{
		isOn = true;
		turnOnState = 1;
		turnOnTimerDelay = 2.0f;
		turnOnTimer = 0.0f;
	}

	public void updateTurnOn()
	{
		turnOnTimer += Time.deltaTime;
		if(turnOnState == 1)
		{
			if(turnOnTimer < turnOnTimerDelay)
			{
				screenBlack.alpha = 1.0f - turnOnTimer;
			}
			else //finished
			{
				turnOnTimer = 0;
				turnOnTimerDelay = 1.0f;
				//turnOnState++;
				turnOnState = 0;
			}
		}

	}

	public void turnOff()
	{
		isOn = false;
		screenBlack.alpha = 1.0f;
		turnOnState = 0;
	}

	public void clickCursor()
	{

	}
}
