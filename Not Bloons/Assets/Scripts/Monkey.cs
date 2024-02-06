using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [Header("Controller")]
    public Vector2 inputDirection;
    public int health;
    public float velocity;
    public float minimumMovementRange = 1F;

    [Header("Dart Shooting")]
    public int dartsPerShot = 1;
    public float dartSpreadAngle = 10f;
    public float dartSpawnSpacing = 0f;
    public GameObject dartPrefab;
    public float dartRateOfFire = 0.1F;
    public SFX dartShootSFX;

    [Header("Extras")]
    public Transform crosshair;
    public Transform gunStartPosition;
    public LineRenderer laserLineRenderer;

    float lastTimeShot;
    Rigidbody2D rb;
    public event Action buttonHoldAction;
    public event Action buttonPressAction;
    public event Action buttonReleaseAction;
    public event Action rightButtonPress;

    public event Action takeDamageEvent;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        buttonHoldAction = SpawnDart;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = GetMouseWorldPosition() - transform.position;

        if (direction.magnitude > minimumMovementRange)
            inputDirection = (GetMouseWorldPosition() - transform.position).normalized;
        else inputDirection = Vector2.zero;


        UpdateCrosshair(GetMouseWorldPosition());

        transform.up = (crosshair.position - transform.position).normalized;

        if (Input.GetMouseButton(0))
        {
            buttonHoldAction?.Invoke();
        }
        else
        {
            laserLineRenderer.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            buttonPressAction?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            buttonReleaseAction?.Invoke();
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    buttonHoldAction += ShootLaser;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    buttonHoldAction += SpawnDart;
        //}

        if (Input.GetMouseButton(1))
        {
            rightButtonPress?.Invoke();
        }
    }
    public void TakeHit(int dmg = 1)
    {
        takeDamageEvent?.Invoke();
        health -= dmg;
        if (health <= 0)
            Debug.Log("Death");
    }
    private void FixedUpdate()
    {
        rb.velocity = inputDirection.normalized * velocity;
    }

    void ShootLaser()
    {
        laserLineRenderer.gameObject.SetActive(true);
        SetLaserPosition(crosshair.position);

        Vector3 laserDirection = (crosshair.position - gunStartPosition.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(gunStartPosition.position, laserDirection);

        //If we're hitting something, do stuff!
        if (hit.collider != null)
        {
            Balloon balloon = hit.collider.GetComponent<Balloon>();

            if (balloon != null)
            {
                balloon.TakeHit();
            }
        }
    }

    void SpawnDart()
    {
        if (Time.time > lastTimeShot + (1 / dartRateOfFire))
        {
            lastTimeShot = Time.time;

            AudioManager.Instance.PlaySFX(dartShootSFX, transform.position);

            for (int i = 0; i < dartsPerShot; i++)
            {
                Vector3 dartRotation = transform.rotation.eulerAngles;
                float spawnOffset = 0;

                if (dartsPerShot % 2 == 1)
                {
                    float offset = i * dartSpreadAngle - (int)(dartsPerShot / 2f) * dartSpreadAngle;
                    spawnOffset = i * dartSpawnSpacing - (int)(dartsPerShot / 2f) * dartSpawnSpacing;
                    dartRotation.z += offset;
                }
                else
                {
                    float offset = i * dartSpreadAngle - (int)(dartsPerShot / 2f) * dartSpreadAngle + 0.5f * dartSpreadAngle;
                    spawnOffset = i * dartSpawnSpacing - (int)(dartsPerShot / 2f) * dartSpawnSpacing + 0.5f * dartSpawnSpacing;
                    dartRotation.z += offset;
                }
                Instantiate(dartPrefab, gunStartPosition.transform.position + transform.right * spawnOffset, Quaternion.Euler(dartRotation));
            }
        }
    }

    void SetLaserPosition(Vector3 laserEndPoint)
    {
        laserLineRenderer.SetPosition(0, gunStartPosition.transform.position);
        laserLineRenderer.SetPosition(1, laserEndPoint);
    }

    Vector3 GetMouseWorldPosition()
    {
        // this gets the current mouse position (in screen coordinates) and transforms it into world coordinates
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // the camera is on z = -10, so all screen coordinates are on z = -10. To be on the same plane as the game, we need to set z to 0
        mouseWorldPos.z = 0;

        return mouseWorldPos;
    }

    void UpdateCrosshair(Vector3 newCrosshairPosition)
    {
        crosshair.position = newCrosshairPosition;
    }


}
