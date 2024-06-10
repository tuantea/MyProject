using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string BLUE = "Blue";
    private const string RED = "Red";
    private const string GREEN = "Green";
    private const string FINISH = "Finish";
    private const string OBSTACLE = "Obstacle";
    Animator animator;
    Material playerColor;
    Material trailEffect;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerColor = this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        trailEffect=this.gameObject.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().trailMaterial;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(OBSTACLE))
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(BLUE))
        {
            ChangePlayerColor(0);
        }
        if (other.gameObject.CompareTag(RED))
        {
            ChangePlayerColor(1);
        }
        if (other.gameObject.CompareTag(GREEN))
        {
            ChangePlayerColor(2);
        }
        if (other.gameObject.CompareTag(FINISH))
        {
            GameManager.Instance.TouchedFinishLine();
        }

    }
    private void ChangePlayerColor(int num)
    { 
      playerColor=GameManager.Instance.GetPlayerColor(num);
      trailEffect=GameManager.Instance.GetTrailEffect(num);
      this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = playerColor;
      this.gameObject.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().material = trailEffect;
    }
    public void Run()
    {
        animator.SetInteger(PLAYER, 1);
    }
    private void FastRun()
    {
        animator.SetInteger(PLAYER, 2);
    }
    private void Die()
    {
        GameManager.Instance.PlayerDie(this.gameObject);
        int z = Random.Range(0, 359);
        GameObject dieFx = Instantiate(GameManager.Instance.GetDieEffext(), new Vector3(transform.position.x,0,this.transform.position.z),Quaternion.Euler(90,0,z));
        dieFx.GetComponent<SpriteRenderer>().color=this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color;
        Destroy(this.gameObject);
    }
}
