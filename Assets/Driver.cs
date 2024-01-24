using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEditor.SearchService;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 300f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;
    bool hasSpeedUp = false;
    Collider2D currentObjectCollidedWith;

    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        currentObjectCollidedWith = other.collider;

        bool hasCollidedWithSpeedUp = (currentObjectCollidedWith.tag != "package" || currentObjectCollidedWith.tag != "customer") && hasSpeedUp;

        if (hasCollidedWithSpeedUp)
        {
            moveSpeed = 20f;
            hasSpeedUp = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currentObjectCollidedWith = other;
        string tag = currentObjectCollidedWith.tag;

        switch(tag.ToLower()) {
            case "boost":
                HandleObjectModifier(boostSpeed, true, 1f);
                break;
            case "bump":
                HandleObjectModifier(slowSpeed, false, 1f);
                break;
            default:
                Debug.Log("Unknown error occured with tag: " + tag);
                break;
        }
    }

    void HandleObjectModifier(float speed, bool hasPickup, float delay)
    {
        moveSpeed = speed;
        hasSpeedUp = hasPickup;
        Destroy(currentObjectCollidedWith.gameObject, delay);
    }

}
