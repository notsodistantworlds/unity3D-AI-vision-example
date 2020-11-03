using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simply takes the keyboard input and sends it to the specified EntityMovement
/// </summary>
public class PlayerInput : MonoBehaviour {
    
    [SerializeField]
    string moveLeftRightAxis;
    [SerializeField]
    string moveUpDownAxis;

    [SerializeField]
    EntityMovement controlledEntity;

    void Update() {
        float moveLR = Input.GetAxis(moveLeftRightAxis);
        float moveUD = Input.GetAxis(moveUpDownAxis);
        controlledEntity.input = new Vector3(moveLR, 0f, moveUD);
    }
}
