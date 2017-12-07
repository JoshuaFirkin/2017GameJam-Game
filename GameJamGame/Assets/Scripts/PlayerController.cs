using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    public float rollSpeed = 500.0f;
    public Text timerText;

    protected string playerID;
    protected int loserID;


    private Rigidbody rb;
    private float timerSeconds = 45.0f;
    private int timerMinutes = 12;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        DisplayTime();
	}


    void Update()
    {
        ReduceTime();
    }


    void FixedUpdate ()
    { 
        // Get player input.
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal_" + playerID), 0.0f, Input.GetAxisRaw("Vertical_" + playerID));

        // Add force to bomb.
        rb.AddForce(input * rollSpeed * Time.deltaTime);
	}


    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 6 && collision.gameObject.tag == "Player")
        {
            Debug.Log("Taking away");
            Vector3 directionToLaunch = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(directionToLaunch * 10, ForceMode.Impulse);
        }

    }


    public void TakeAwayTime(int secs)
    {
        for (int i = 0; i < secs; i++)
        {
            if (timerSeconds > 0)
            {
                timerSeconds -= 1;
            }
            else
            {
                timerMinutes -= 1;
                timerSeconds = 60;
            }
                
        }
    }


    void ReduceTime()
    {
        // Takes away seconds
        timerSeconds -= Time.deltaTime;

        // If timer (seconds) is less than or equal to 0.
        if (timerSeconds <= 0)
        {
            // If timer (minutes) is less than 0.
            if (timerMinutes <= 0)
            {
                // Destroy the player.
                DestroyPlayer();       
            }
            // If its not less than 0.
            else
            {
                // Take away a minute.
                timerMinutes -= 1;
                // Reset seconds back to 60.
                timerSeconds = 60;
            }
        }

        // Display the remaining time.
        DisplayTime();
    }


    void DisplayTime()
    {
        if (timerMinutes <= 0)
            timerText.text = (Mathf.RoundToInt(timerSeconds).ToString());
        else
            timerText.text = (timerMinutes + ":" + Mathf.RoundToInt(timerSeconds));
    }


    void DestroyPlayer()
    {
        GameMaster.instance.TimeRanOut(loserID);
        Time.timeScale = 0;
        Destroy(this.gameObject);
    }


    public int GetLoserID()
    {
        return loserID;
    }

}
