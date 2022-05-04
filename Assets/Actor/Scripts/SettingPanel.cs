using System.Collections;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    // Start is called before the first frame update

    [Button]
    private void CloseOpenRewardGizmos()
    {
        EventBus.Post(new CloseAllRewardGizmosDetected());
    }
}
