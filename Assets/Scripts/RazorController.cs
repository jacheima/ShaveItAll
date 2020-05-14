using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorController : MonoBehaviour
{
    public LayerMask shaveableLayer;
    public float shaveDistance;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = default;

        if (Physics.Raycast(ray, out hit, shaveDistance, shaveableLayer.value))
        {

            //move the razor the be where we want
            transform.position = hit.point;

            //controlling the rotation
            if(Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0f, 40 * Time.deltaTime, 0f);
            }

            if(Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0f, -40 * Time.deltaTime, 0f);
            }
            
            if(Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0f, 0f, 40f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, 0f, -40f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(40f * Time.deltaTime, 0f, 0f);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(-40f * Time.deltaTime, 0, 0f);
            }
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("I'm shaving");

        if(Input.GetMouseButton(0))
        {
            GameManager.instance.DestroyBeardPiece(collision.gameObject);
            collision.GetComponent<Beard>().CountCuts();
        }
    }
}
