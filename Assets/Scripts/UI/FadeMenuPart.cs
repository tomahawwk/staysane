using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FadeMenuPart : MonoBehaviour {
	public GameObject linkedPart;
	public List<GameObject> partList;

	void Start(){
		GetComponent<Button>().onClick.AddListener(SetActive);
	}

	void SetActive(){
		foreach (GameObject part in partList)
		{
			part.SetActive(false);
			
		}
		linkedPart.SetActive(true);
		if(linkedPart.GetComponent<SaveAndLoad>() != null){
			linkedPart.GetComponent<SaveAndLoad>().init();
		}
	}
}
