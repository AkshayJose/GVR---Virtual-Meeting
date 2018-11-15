using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyScript : Photon.MonoBehaviour
{

    public GameObject camera;
    public PhotonView name;

    void Awake()
    {
        name.RPC("updateName", PhotonTargets.AllBuffered, PhotonNetwork.playerName);
        gameObject.name = PhotonNetwork.playerName;
    }

    private void Start()
    {

        if (photonView.isMine == true && PhotonNetwork.connected == true)
        {
            GetComponent<PhotonVoiceRecorder>().enabled = true;
        }/*
        else
        {
            GetComponent<CopyScript>().enabled = false;
        }*/

    }
	
	// Update is called once per frame
	void Update () {
        
        if (photonView.isMine)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
		
	}
}
