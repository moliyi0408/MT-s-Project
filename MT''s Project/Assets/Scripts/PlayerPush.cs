using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    ////public Transform grabPoint; //綁在player上
    //public Transform boxHolder; //綁在player上
    //public float rayDist;


    //// Update is called once per frame
    //void Update()
    //{
    //    //Vector2.right * transform.localScale
    //    RaycastHit2D grabCheck = Physics2D.Raycast(boxHolder.position, transform.right, rayDist);
    //    if(grabCheck.collider !=null && grabCheck.collider.tag == "Box")
    //    {
    //        if (Input.GetKey(KeyCode.G))
    //        {
    //            grabCheck.collider.gameObject.transform.parent = boxHolder;
    //            grabCheck.collider.gameObject.transform.position = boxHolder.position;
    //            grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    //        }
    //        else
    //        {
    //            grabCheck.collider.gameObject.transform.parent = null;
                
    //            grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    //        }
    //    }
    //    Debug.DrawRay(boxHolder.position, transform.right * rayDist);
    //}
}
