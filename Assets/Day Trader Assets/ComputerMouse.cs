using UnityEngine;
using System.Collections;

public class ComputerMouse : MonoBehaviour {

	public DeskComputer deskComputer;

	// Use this for initialization
	void Start () {
		GetComponent<DeskObject>().onActivate = OnActivate;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnActivate(OTObject owner)
	{
		deskComputer.clickCursor();
	}
}
