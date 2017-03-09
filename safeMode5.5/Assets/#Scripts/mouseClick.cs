using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mouseClick : MonoBehaviour {

	public canvasType canvas;
	public GameObject canvasManager;
	CanvasManager manager;
	public bool enable = true;

	void Awake () {
		manager = canvasManager.GetComponent<CanvasManager> ();
	}

	public void click () {
		if (enable) {
			manager.change (canvas);
		}
	}
}
