using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityInfo
{
    //we dont need stuff like start and update since we are just using this as a data structure
    #region Editor Variables
    [SerializeField]
    [Tooltip("How muhc power this ability has")]
    private int m_Power;
    public int Power
    {
        get
        {
            return m_Power;
        }
    }

    [SerializeField]
    [Tooltip("If this is an attack that shoots something out, this value describes how far the attack can shoot")]
    private float m_Range;
    public float Range
    {
        get {
            return m_Range;

        }
    }
    #endregion

}
