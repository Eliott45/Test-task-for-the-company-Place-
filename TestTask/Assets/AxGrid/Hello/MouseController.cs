using System;
using System.Collections;
using System.Collections.Generic;
using AxGrid.Hello;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) StartCheck();
    }
    
    private void StartCheck()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hits2D = Physics2D.GetRayIntersection(ray);

        if (!hits2D.collider) return;
        var click = hits2D.collider.GetComponent<IClicked>();
        click?.ONClickAction();
        /*
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit)) return;

        if (!hit.collider) return;
        var click = hit.collider.GetComponent<IClicked>();
        click?.ONClickAction();
        */
    }

}
