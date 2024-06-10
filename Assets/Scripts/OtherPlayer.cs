using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
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

    private bool stopPlayerCollision;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerColor =this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        trailEffect=this.gameObject.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().trailMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(PLAYER) && !stopPlayerCollision)
        {
            stopPlayerCollision = true;
            Material thisPlayer=this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
            Material otherplayer = collision.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
            if (thisPlayer.color == otherplayer.color)
            {
                trailEffect = collision.gameObject.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().trailMaterial;
                this.gameObject.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().trailMaterial = trailEffect;
                AddPlayer();
            }
            else
            {
                KillPlayer(collision);
            }
            
        }

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
    public void ChangePlayerColor(int num)
    {
        playerColor = GameManager.Instance.GetPlayerColor(num);
        trailEffect = GameManager.Instance.GetTrailEffect(num);
        this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = playerColor;
        this.gameObject.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().trailMaterial = trailEffect;
    }
    void AddPlayer()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetInteger(PLAYER, 1);
        this.gameObject.tag = PLAYER;
        GameManager.Instance.AddListPlayer(this.gameObject);
        this.transform.parent = GameManager.Instance.GetCharacterParentGroup().transform;
        GameObject addVFX = Instantiate(GameManager.Instance.GetAddEffect(), this.transform.position, GameManager.Instance.GetAddEffect().transform.rotation);
        addVFX.transform.parent = this.transform;
        GameManager.Instance.PlayerCounter();
        MusicManager.Instance.AddPlayer();
    }
    public void KillPlayer(Collision other)
    {
        Destroy(other.gameObject);
        int zrot1 = Random.Range(0, 359);
        GameObject dieFx1 = Instantiate(GameManager.Instance.GetDieEffext(), new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(90, 0, zrot1));
        dieFx1.GetComponent<SpriteRenderer>().color = other.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color;

        GameManager.Instance.RemoveListPlayer(other.gameObject);
        GameManager.Instance.PlayerCounter();
        GameManager.Instance.CharacterParentCheck();

        int zrot = Random.Range(0, 359);
        GameObject dieFx = Instantiate(GameManager.Instance.GetDieEffext(), new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(90, 0, zrot));
        dieFx.GetComponent<SpriteRenderer>().color = this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color;
        MusicManager.Instance.DiePlayer();
        Destroy(this.gameObject);
    }
    private void Die()
    {
        GameManager.Instance.RemoveListPlayer(this.gameObject);
        GameManager.Instance.PlayerCounter();
        GameManager.Instance.CharacterParentCheck();
        MusicManager.Instance.DiePlayer();
        int zrot = Random.Range(0, 359);
        GameObject dieFx = Instantiate(GameManager.Instance.GetDieEffext(), new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(90, 0, zrot));
        dieFx.GetComponent<SpriteRenderer>().color = this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color;
        MusicManager.Instance.DiePlayer();
        Destroy(this.gameObject);
    }
}
