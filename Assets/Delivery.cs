using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(0, 255, 0, 255);
    [SerializeField] Color32 noPackageColor = new Color32(0, 0, 255, 255);
    bool hasPackage = false;
    [SerializeField] float destroyDelay = 0.5f;
    
    SpriteRenderer spriteRenderer;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Oops we bumped into you");
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        HandleTrigger(other);
    }

    void HandleTrigger(Collider2D other) {
        switch(other.tag.ToLower()) {
            case "package":
                HandlePackagePickup(other);
                break;
            case "customer":
                HandlePackageDelivery();
                break;
            default:
                Debug.Log("Unknown error status of other tag: " + other.tag);
                break;
        }
    }

    void HandlePackagePickup(Collider2D other) {
        if (!hasPackage) {
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        }
    }
    void HandlePackageDelivery() {
        if (hasPackage) {
            Debug.Log("package have been delivered");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        } 
        else 
        {
            Debug.Log("Where is my package?!");
        }
    }
}
