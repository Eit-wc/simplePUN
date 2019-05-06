using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplePUNGUIcontrol : MonoBehaviour
{   
    GameObject quickPlayPanel;
    GameObject playWithFriendPanel;
    TMPro.TMP_Dropdown dropDownMode;


    // Start is called before the first frame update
    void Start()
    {
        dropDownMode = transform.GetComponentInChildren<TMPro.TMP_Dropdown>();
        quickPlayPanel = transform.Find("Panel/QuickplayParnel").gameObject;
        playWithFriendPanel = transform.Find("Panel/FriendParnel").gameObject;
    }

    public void onModeChange()
    {
        int select = dropDownMode.value;
        Debug.Log(select);
        switch (select)
        {
            case 0:
                quickPlayPanel.SetActive(true);
                playWithFriendPanel.SetActive(false);
            break;
            
            case 1:
                quickPlayPanel.SetActive(false);
                playWithFriendPanel.SetActive(true);
            break;

            default:
                quickPlayPanel.SetActive(true);
                playWithFriendPanel.SetActive(false);
            break;
        }
    }
}
