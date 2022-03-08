using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script reset the reward location to be active so the animal can get another reward on the next lap.
public class ResetLocation : MonoBehaviour
{
    private RewardLocation rewardLocation;
    private Task task;
    // Start is called before the first frame update
    void Start()
    {
        rewardLocation = FindObjectOfType<RewardLocation>();
        task = FindObjectOfType<Task>();
    }
    // Called when actor enters reset location.
    public void OnTriggerEnter(Collider collider)
    {
        // Set marker visible/invisible
        if (task.visibleMarker) { rewardLocation.GetComponent<MeshRenderer>().enabled = true; }
        else { rewardLocation.GetComponent<MeshRenderer>().enabled = false; }
        rewardLocation.isActive = true;
    }

}
