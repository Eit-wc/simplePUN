using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class simplePUNSpawnPoint : MonoBehaviourPunCallbacks , IPunObservable
{
    public bool HaveOwner;
    // Start is called before the first frame update
    void Start()
    {
        HaveOwner = false;
    }
    
    public Transform getPoint()
    {
        HaveOwner = true;
        return transform;
    }

    // Update is called once per frame
    public void OnPhotonSerializeView(PhotonStream steam, PhotonMessageInfo message)
    {
        if(steam.IsWriting)
        {
            steam.SendNext(HaveOwner);
        }else if(steam.IsReading)
        {
            HaveOwner = (bool)steam.ReceiveNext();
        }
    }
}
