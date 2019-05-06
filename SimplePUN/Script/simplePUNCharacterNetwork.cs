using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class simplePUNCharacterNetwork : MonoBehaviour
{
    // Start is called before the first frame update
    
    PhotonView pv;
    public string charName;
    public GameObject CameraObj;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if(!pv.IsMine)
        {
            CameraObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
