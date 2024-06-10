 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    public void Move()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    public void Stop()
    {
        this.transform.Translate(Vector3.zero);
    }
}
