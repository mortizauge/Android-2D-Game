using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float minX = -7.20f;
    [SerializeField] private float maxX = 35f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private Transform background;

    [SerializeField] private Camera cam;

    [SerializeField] private Text levelText;

    private float zoom;
    private float zoomMultiplier;
    private float zoomMin = 5f;
    private float zoomMax = 6f;
    private float velocity = 0f;
    private float smoothTime = .25f;
    private Vector3 originalBackgroundScale;

    private void Start()
    {
        Application.targetFrameRate = 60;
        levelText.text = "Level " + (SceneManager.GetActiveScene().buildIndex);
        zoom = cam.orthographicSize;
        originalBackgroundScale = background.transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        Zoom();

        transform.position = new Vector3(player.position.x, player.position.y + .75f, transform.position.z);
        background.position = new Vector3(player.position.x, player.position.y + .75f, background.position.z);

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            background.position = new Vector3(minX, background.position.y, background.position.z);
            OffsetUpwards();
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            background.position = new Vector3(maxX, background.position.y, background.position.z);
            OffsetUpwards();
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            background.position = new Vector3(background.position.x, minY, background.position.z);
        }
        else if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
            background.position = new Vector3(background.position.x, maxY, background.position.z);
        }

    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }

    private void OffsetUpwards()
    {
            transform.position = new Vector3(transform.position.x, player.position.y + .75f, transform.position.z);
            background.position = new Vector3(background.position.x, player.position.y + .75f, background.position.z);
    }
}
