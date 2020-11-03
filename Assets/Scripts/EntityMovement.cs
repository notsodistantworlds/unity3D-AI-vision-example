using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour {

    [SerializeField]
    float moveSpeed;

    public Vector3 input;

    void FixedUpdate() {
        input.y = 0f; // just a technical detail. don't want to allow vertical movement

        GetComponent<Rigidbody>().MovePosition(
            transform.position + // move this object  
            (Vector3)input.normalized // in the direction of input (.normalized returns a 1-unit long vector)
                * moveSpeed // apply speed
                * Time.deltaTime); // scale by the amount of time that passed since last frame
    }
}
