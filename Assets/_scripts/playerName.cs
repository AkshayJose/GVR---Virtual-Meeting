using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerName : MonoBehaviour {

	public Text nameTag;




	[PunRPC]
	public void updateName(string name){
		nameTag.text = name;
		Debug.Log (nameTag.text);
	}

}
