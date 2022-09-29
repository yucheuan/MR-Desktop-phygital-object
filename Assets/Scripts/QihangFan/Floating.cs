using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public Vector3 moveDirection;
    public float moveDistance = 0.0015f;
    public float moveSpeed = 1f;
    public float moveOffset;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + moveDirection * (moveDistance * Mathf.Sin(Time.time*moveSpeed + moveOffset));
    }
}
