using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRootManager : MonoBehaviour
{


    public void Entrance()
    {
        BroadcastMessage("ReceiverInfoStartRun", SendMessageOptions.DontRequireReceiver);
    }
}
