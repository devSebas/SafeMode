using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class loginManager : MonoBehaviour {

	mouseClick script;
	public InputField user;
	public InputField pass;

	void Awake() {
		script = GetComponent<mouseClick> ();
	}

	void Update () {
		if (user.text == "seb" && pass.text == "pass") {
			script.enable = true;
		}
	}
}
