using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtPosition : MonoBehaviour {
	
	public GameObject player;
	private float xPosition;
	private float yPosition;

	void Update(){
		this.transform.position = new Vector3(-player.transform.localPosition.x + (yPosition + 350), player.transform.localPosition.y + (yPosition + 350), -player.transform.localPosition.z);
	}
}
