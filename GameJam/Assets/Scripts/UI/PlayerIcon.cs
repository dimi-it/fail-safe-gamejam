using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    Transform camTransform;
    Transform unit;
    Transform canvasTransform;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
        unit = transform.parent;
        canvasTransform = FindAnyObjectByType<WorldSpaceCanvas>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camTransform.transform.position);
        transform.position = unit.position + offset;
    }
}
