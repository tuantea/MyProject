using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Vector3 fistVector;
    Vector3 secondVector;
    private float speed = 5f;
    float minX = -19;
    float maxX = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.DragControl())
        {
            fistVector = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
        }
        if (Input.GetMouseButton(0) && GameManager.Instance.DragControl())
        {
            secondVector = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
            Vector3 diff = secondVector - fistVector;
            transform.position += diff * speed;
            this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), this.transform.position.y, this.transform.position.z);
            fistVector = secondVector;
        }
    }
}
