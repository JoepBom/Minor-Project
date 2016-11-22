using UnityEngine;
using System.Collections;

public class Portal : PhilInteractable {

    public string nextLevelName;

	public override void Interact(GameObject interacted)
    {
        Debug.Log("Interacting with base class.");
        Application.LoadLevel(nextLevelName);
    }
}

