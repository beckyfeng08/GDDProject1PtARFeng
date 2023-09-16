using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    #region Editor Variables
    [SerializeField]
    [Tooltip("The player to follow the players location")]
    //we want to store its transform of the player
    private Transform m_PlayerTransform;

    [SerializeField]
    [Tooltip("The offset from the player's origin to the camera")]
    //how far the camera is from the player
    private Vector3 m_Offset;

    [SerializeField]
    [Tooltip("How quickly the player can rotate the camera to the left and the right")]
    private float m_RotationSpeed = 10;

    #endregion

    #region Main Updates
    //we want to have the camera follow the player's position
    //after all physics calculated, we will know where hte camera will be
    private void LateUpdate()
    {
        //the position of the player, and then the offset that we define (where the camera is)
        Vector3 newPos = m_PlayerTransform.position + m_Offset;

        //slerp creates a transition as smooth as possible
        transform.position = Vector3.Slerp(transform.position, newPos, 1);

        float rotationAmount = m_RotationSpeed * Input.GetAxis("Mouse X");
        //this rotates the camera
        transform.RotateAround(m_PlayerTransform.position, Vector3.up, rotationAmount);
        //account for the rotation (otherwise we will get weird behavior (tryignt o maintiandsitance form player to camera)
        m_Offset = transform.position - m_PlayerTransform.position;
    }

    #endregion
}
