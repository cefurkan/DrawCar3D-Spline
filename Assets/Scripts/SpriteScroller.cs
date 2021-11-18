using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Vector2 offset;
    Material material;

    Transform target;

    private void OnEnable()
    {
        DrawLine.newBodyCreated += DrawnBody_newBodyCreated;
    }

    private void OnDisable()
    {
        DrawLine.newBodyCreated -= DrawnBody_newBodyCreated;
    }

    private void DrawnBody_newBodyCreated(DrawnBody NewDrawnBody)
    {
        target = NewDrawnBody.transform;
    }

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;

    }

    private void Update()
    {
        // parallax effect
        if (target != null)
        {
            // based on player speed
            offset = target.GetComponent<DrawnBody>().GetDrawnBodyVelocity() * Time.deltaTime * moveSpeed;
            material.mainTextureOffset += offset;
        }

        // TODO generate BG dynamically
    }
}
