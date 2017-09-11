using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;

	public LayerMask movementMask;
	PlayerMotor motor;
	Camera cam;

	// Use this for initialization
	void Start () 
	{
		cam = Camera.main;
		motor = GetComponent<PlayerMotor> ();
	}
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

		Ray ray = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		// Left mouse button
		if (Input.GetMouseButtonDown(0)) 
		{
			if (Physics.Raycast(ray, out hit, 100, movementMask)) 
			{
				Debug.Log ("You hit " + hit.collider.name + " on " + hit.point);
				// Move player to what we hit
				motor.MoveToPoint(hit.point);
                // Stop focusing any objects
                RemoveFocus();
			}
		}
		// Right mouse button
		if (Input.GetMouseButtonDown(1)) 
		{
			if (Physics.Raycast(ray, out hit, 100)) 
			{
                // Check if we hit an interactable
                // If we did set it as focus
                Interactable interactable  = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
			}
		}
	}

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus!= null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowTarget();
    }

}
