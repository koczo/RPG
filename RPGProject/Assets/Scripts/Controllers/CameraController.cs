﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float pitch = 2f;

	public float zoomSpeed = 4f;
	public float minZoom = 5f;
	public float maxZoom = 15f;
	private float currentZoom = 10f;

	public float yawSpeed = 100f;
	private float currentYaw = 0f;
	

	void Update()
	{
		currentZoom -= Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp (currentZoom, minZoom, maxZoom);
        
        // Right mouse button
        if (Input.GetMouseButton(1))
        {
            currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
        }
	}

	void LateUpdate () 
	{
		transform.position = target.position - offset * currentZoom;
		transform.LookAt (target.position + Vector3.up * pitch);

		transform.RotateAround (target.position, Vector3.up, currentYaw);
	}
}
