using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed=1f;

    Rigidbody2D myRigidbody2D;

	// Use this for initialization
	void Start () {
        myRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsFaceRight())
        {
            myRigidbody2D.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            myRigidbody2D.velocity = new Vector2(-moveSpeed, 0);
        }
	}

    bool IsFaceRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);
    }
}
