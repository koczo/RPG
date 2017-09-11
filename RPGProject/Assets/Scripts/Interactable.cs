using System;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public float radius = 3f;
    bool isFocus = false;
    bool hasInteracted = false;
    Transform player;
    public Transform interactcionTransform;

    public virtual void Interact()
    {
        // this method is ment to be overeritten
        Debug.Log("Interacting with " + interactcionTransform.name);
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactcionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        hasInteracted = false;
        isFocus = true;
        player = playerTransform;
    }

    public void OnDefocused()
    {
        hasInteracted = false;
        isFocus = false;
        player = null;
    }

    void OnDrawGizmosSelected()
	{
        if (interactcionTransform == null)
        {
            interactcionTransform = transform;
        }

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (interactcionTransform.position, radius);
	}

    
}
