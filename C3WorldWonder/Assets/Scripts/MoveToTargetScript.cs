using UnityEngine;
using System.Collections;

public class MoveToTargetScript : MonoBehaviour {

    public bool move = true;
    private bool roll = true;
    public float moveSpeed = 1;


    void Update()
    {
        if (MainManagerScript.haveTarget)
        {
            if (roll)
            {
                Roll();
            }
            if (move)
            {
                Move();
            }
        }
    }

    void Roll()
    {
        GetComponentInParent<RollScript>().enabled = false;
        transform.LookAt(MainManagerScript.target.transform.position);
        transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y, 0);
    }

    void Move()
    {
        if (Vector3.Distance(gameObject.transform.position, MainManagerScript.target.transform.position) > 1f)
        {
            gameObject.transform.position += (MainManagerScript.target.transform.position - gameObject.transform.position) * Time.deltaTime * moveSpeed;
            roll = true;
        }
        else
        {
            roll = false;
        }
    }

}
