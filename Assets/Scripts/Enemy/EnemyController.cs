using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How much health this enemy has")]
    private int m_MaxHealth;

    [SerializeField]
    [Tooltip("How fast the enemy can move")]
    private float m_Speed;

    [SerializeField]
    [Tooltip("Approximate amount of damage dealt per frame")]
    private float m_Damage;

    //to create a new object in code
    [SerializeField]
    [Tooltip("The explosion that occurs when this enemy dies")]
    private ParticleSystem m_DeathExplosion;

    [SerializeField]
    [Tooltip("The probability that the enemy drops a health pill")]
    private float m_HealthPillDropRate;

    [SerializeField]
    [Tooltip("The type of health pill this enemy drops")]
    private GameObject m_HealthPill;

    [SerializeField]
    [Tooltip("How many points killing this enemy provides")]
    private int m_Score;
    #endregion

    #region Private Variables
    private float p_curHealth;
    #endregion

    #region Cached Components
    //this is the enemy's rigid body
    private Rigidbody cc_Rb;
    #endregion

    #region Cached References
    //the player's rigid body
    //for components that we will be creating variables for but for other game objects
    private Transform cr_Player;
    #endregion
    #region Initialization
    private void Awake()
    {
        p_curHealth = m_MaxHealth;

        //a few ways to get the enemy's transform. We could
            //getcomponent
            //search by name with gameobject.find
            //otherwise search by tag (how we label object)
            //rather costly, so we do this in start nd awkwae
        cc_Rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //we want to make sure object exists before assigning (after awake, we know it exists)
        cr_Player = FindObjectOfType<PlayerController>().transform;
    }
    #endregion

    #region Main Updates
    private void FixedUpdate()
    {
        Vector3 dir = cr_Player.position - transform.position;
        //so we can apply the speed to moveposition
        dir.Normalize();

        cc_Rb.MovePosition(cc_Rb.position + dir * m_Speed * Time.fixedDeltaTime);
    }
    #endregion

    #region Collision Methods
    private void OnCollisionStay(Collision collision) {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().DecreaseHealth(m_Damage);
        }
     }

    #endregion

    #region Health Methods
    public void DecreaseHealth(float amount)
    {
        p_curHealth -= amount;
        if (p_curHealth <= 0)
        {
            ScoreManager.singleton.IncreaseScore(m_Score);
            if (Random.value < m_HealthPillDropRate)
            {
                Instantiate(m_HealthPill, transform.position, Quaternion.identity);
            }
            Instantiate(m_DeathExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    #endregion
}
