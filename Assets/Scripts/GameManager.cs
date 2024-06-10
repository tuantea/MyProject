using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string DOOR = "Door";
    private const string DIANO = "Diano";
    private const string PLAYER = "Player";
    public static GameManager Instance { get; private set; }
    [SerializeField] private PlayerParent playerParent;
    [SerializeField] private Player player;
    [SerializeField] private GameObject characterParentGroup;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject generaUIElements;

    [SerializeField] private Transform characterEndReachPoint;

    [SerializeField] private List<Material> listPlayerColor;
    [SerializeField] private List<GameObject> playerList;
    [SerializeField] private List<Material> trailEffects;

    private bool playerMove;
    private bool playerMoveDead;
    private bool dragControl;
    private bool cameraFollow;
    private bool moveCamera;
    private bool fire;
    private bool startCannonRotation;

    private float totalEnemies;
    private float enemiesDied;
    private int bullets;
    private float time;

    [SerializeField] private ProgessBar playerProgressBar;
    [SerializeField] private ProgessBar enemyProgressBar;
    [SerializeField] private TextMeshProUGUI playerNumber;
    [SerializeField] private TextMeshProUGUI enemyNumber;
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private GameObject levelFail;
    private GameObject swipe;

    [SerializeField] private GameObject addEffect;
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private GameObject popParticle1;
    [SerializeField] private GameObject popParticle2;

    private void Awake()
    {
        Instance = this;
    }
    public Material GetPlayerColor(int num)
    {
        return listPlayerColor[num];
    }
    public Material GetTrailEffect(int num)
    {
        return trailEffects[num];
    }
    public void PlayerDie(GameObject player)
    {
        playerList.Remove(player);
        PlayerCounter();
        CharacterParentCheck();
        MusicManager.Instance.DiePlayer();
    }

    private void Start()
    {
        AddPlayersInList();
        dragControl = true;
        cameraFollow = true;
        PlayerCounter();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !playerMove && !playerMoveDead) 
        {
            playerMove = true;
            playerMoveDead = false;
            player.Run();   
        }
        PlayerProgress();
        EnemyProgress();
        Debug.Log(playerList.Count);
        if (playerMove)
        {
            playerParent.Move();
        }
        if (moveCamera)
        {
            MoveCamera();
        }
    }
    public GameObject GetDieEffext()
    {
        return dieEffect;
    }
    public GameObject GetAddEffect()
    {
        return addEffect;
    }
    private void AddPlayersInList()
    {
        for(int i = 0; i < characterParentGroup.gameObject.transform.childCount; i++)
        {
            playerList.Add(characterParentGroup.transform.GetChild(i).gameObject);
        }
    }

    public void AddListPlayer(GameObject player)
    {
        playerList.Add(player);
    }
    public void RemoveListPlayer(GameObject player)
    {
        playerList.Remove(player);
    }
    public void CharacterParentCheck()
    {
        Debug.Log(playerList.Count);
        if(playerList.Count == 0)
        {
            playerMove = false;
            playerMoveDead = true;
            playerParent.Stop();
            StartCoroutine(LevelFailDelay());
        }
    }
    
    private void MoveCamera()
    {
        playerProgressBar.ShowParent();
        //if (Vector3.Distance(mainCamera.transform.position, castleCameraPoint.position) > .2f)
        //{
        //    mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, castleCameraPoint.position, speed * Time.deltaTime);
        //    mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, Quaternion.Euler(8.224f, 0, 0), speedRotation * Time.timeScale);
        //}
        //else
        //{
        //    moveCamera = false;
        //    fire = true;
        //    bullets = playerList.Count;
        //    startCannonRotation = true;
        //    swipe.SetActive(true);
        //    for (int i = 0; i < playerList.Count; i++)
        //    {
        //        playerList[i].gameObject.SetActive(false);
        //    }
        //    enemyProgressBar.ShowParent();
        //}
    }
    public void PlayerCounter()
    {
        bullets=playerList.Count;
        playerNumber.text=bullets.ToString();
    }
    private void EnemyProgress()
    {
        enemyNumber.text=enemiesDied.ToString();
        enemyProgressBar.ProgessBarImage(enemiesDied, totalEnemies);
    }
    private void PlayerProgress()
    {
        playerProgressBar.ProgessBarImage(characterParentGroup.transform.position.z , characterEndReachPoint.position.z);
    }
    private void EnemyDiedIncrementer()
    {
        enemiesDied++;
    }
    public void TouchedFinishLine()
    {
        playerMove = false;
        dragControl = false;
        cameraFollow = false;
        StartCoroutine(LevelCompleteDelay());
    }
    public bool CameraFollow()
    {
        return cameraFollow;
    }
    public bool DragControl()
    {
        return dragControl;
    }
    public GameObject GetCharacterParentGroup()
    {
        return characterParentGroup; 
    }

    IEnumerator LevelFailDelay()
    {
        generaUIElements.SetActive(true);
        yield return new WaitForSeconds(2f);
        MusicManager.Instance.Loose();
        levelFail.SetActive(true);
        Canvas.Instance.Active_Privacy_Policy();
    }
    IEnumerator LevelCompleteDelay()
    {
        generaUIElements.SetActive(false);
        yield return new WaitForSeconds(2);
        popParticle1.SetActive(true);
        popParticle2.SetActive(true);
        levelComplete.SetActive(true);
        Canvas.Instance.Active_Privacy_Policy();
    }
}
