using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoMovement : MonoBehaviour
{
    private Animator thisAnim;
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
    thisAnim = GetComponent<Animator> ();
    rigid = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
    var h = Input.GetAxis ("Horizontal");
    var v = Input.GetAxis ("Vertical");
 
    thisAnim.SetFloat ("Speed", v);
    }
}
