using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public UnityEngine.EventSystems.EventSystem eventSystem;
    public Camera PlayerCam;
    public EnemyOptions enemyOptions;
    PlayerCharacter PlayerChr;
    bool moving = false;
    bool rotating = false;
    Vector3 movePoint = Vector3.zero;
    public float speed = 1.0f;
    Vector3 camDistance;
    public Terrain terrain;
    public float playerHeight = 1.82f; //this is really bad
    Rigidbody playerRb;
    NavMeshAgent playerAgent;
    GameObject target;
    
    private Projector walkRadiusProjector;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerChr = gameObject.GetComponentInChildren<PlayerCharacter>();
        playerRb = PlayerChr.GetComponent<Rigidbody>();
        camDistance = PlayerCam.transform.position - PlayerChr.transform.position;
        playerAgent = PlayerChr.GetComponent<NavMeshAgent>();
        walkRadiusProjector = PlayerChr.GetComponentInChildren<Projector>();
        //playerRb.AddForce(new Vector3(10.0f, 0, 0));
    }

    // Just dump all the combat functionality into these updates el o el
    // Update is called once per frame
    void Update()
    {
        if (PlayerChr.combatMan.IsCombat)
        {
            CombatUpdate();
        }
        else
        {
            if (!eventSystem.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                Ray ray = PlayerCam.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Terrain"))
                    {
                        //terrain = hit.collider.gameObject.GetComponent<Terrain>();
                        movePoint = hit.point;
                        //movePoint.y += playerHeight;

                        PlayerChr.Moving = true;
                        PlayerChr.Rotating = true;
                        playerAgent.destination = movePoint;
                    }
                    else if (hit.collider.gameObject.CompareTag("Loot"))
                    {
                        movePoint = hit.point;
                        //movePoint.y += playerHeight; //this is wrong
                        //PlayerChr.gameObject.transform.Translate(movePoint);
                        PlayerChr.Moving = true;
                        PlayerChr.Rotating = true;
                        PlayerChr.SelectedTargetId = 1;
                        PlayerChr.Target = hit.collider.gameObject;
                        playerAgent.destination = movePoint;

                    }
                    else if (hit.collider.gameObject.CompareTag("Talk"))
                    {
                        movePoint = hit.collider.gameObject.transform.position;
                        //movePoint.y += playerHeight; //this is wrong
                        PlayerChr.Moving = true;
                        PlayerChr.Rotating = true;
                        PlayerChr.SelectedTargetId = 1;
                        PlayerChr.Target = hit.collider.gameObject.transform.parent.gameObject;
                        playerAgent.destination = movePoint;

                    }
                    else if (hit.collider.gameObject.CompareTag("Attack"))
                    {
                        movePoint = hit.collider.gameObject.transform.position;
                        //movePoint.y += playerHeight; //this is wrong
                        PlayerChr.Moving = true;
                        PlayerChr.Rotating = true;
                        PlayerChr.SelectedTargetId = 1;
                        PlayerChr.Target = hit.collider.gameObject.transform.parent.gameObject;
                        playerAgent.destination = movePoint;
                    }

                }
            }
        }
        

        if (PlayerChr.Moving)
        {

            /* earlier shit
            float step = speed * Time.deltaTime;
            Vector3 interpolatedPosition = Vector3.MoveTowards(PlayerChr.transform.position, movePoint, step);
            Vector3 dir = PlayerChr.transform.position - movePoint;
            interpolatedPosition.y = terrain.SampleHeight(interpolatedPosition) + playerHeight;
            //PlayerChr.gameObject.transform.Translate(dir * Time.deltaTime);
            PlayerChr.gameObject.transform.position = interpolatedPosition;

            Vector3 targetDir = movePoint - PlayerChr.transform.position;
            targetDir.y = 0;
            ////float angleBetween = Vector3.Angle(transform.forward, targetDir);

            Vector3 newDirection = Vector3.RotateTowards(PlayerChr.gameObject.transform.forward, targetDir, step, 0.0f);
            PlayerChr.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
            ////transform.rotation = Quaternion.LookRotation(newDirection);
            //if (Vector3.Distance(PlayerChr.gameObject.transform.position, movePoint) < 0.001f)
            if (Vector3.Distance(interpolatedPosition, movePoint) < 0.001f)
            {
                PlayerChr.Moving = false;
            }*/
            //float step = speed * Time.deltaTime;
            //Vector3 interpolatedPosition = Vector3.MoveTowards(PlayerChr.transform.position, movePoint, step);
            //interpolatedPosition.y = terrain.SampleHeight(interpolatedPosition) + playerHeight;
            //playerRb.MovePosition(movePoint * speed);

            //if (Vector3.Distance(playerRb.position, movePoint) < 0.001f)
            //{
            //    PlayerChr.Moving = false;
            //}
            
        }
        
    }

    private void CombatUpdate()
    {
        if (PlayerChr.combatMan.IsPlayerTurn && !PlayerChr.combatMan.HasMoved && !PlayerChr.combatMan.HasPerformedAction && !eventSystem.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Ray ray = PlayerCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Terrain"))
                {
                    float dist = Vector2.Distance(new Vector2(hit.point.x, hit.point.z), new Vector2(PlayerChr.transform.position.x, PlayerChr.transform.position.z));
                    
                    if (dist <= PlayerChr.MovementRange && PlayerChr.combatMan.IsPositionWithinBattleZone(hit.point))
                    {
                        //terrain = hit.collider.gameObject.GetComponent<Terrain>();
                        movePoint = hit.point;
                        //movePoint.y += playerHeight;
                        //combatMan.PlayerMoved();
                        PlayerChr.Moving = true;
                        PlayerChr.Rotating = true;
                        PlayerChr.combatMan.HasMoved = true;
                        playerAgent.destination = movePoint;
                    }
                }
                else if (hit.collider.gameObject.CompareTag("Attack"))
                {
                    movePoint = hit.collider.gameObject.transform.position;
                    //movePoint.y += playerHeight; //this is wrong
                    //combatMan.PlayerMoved();
                    
                    PlayerChr.Moving = true;
                    PlayerChr.Rotating = true;
                    PlayerChr.combatMan.HasPerformedAction = true;
                    PlayerChr.SelectedTargetId = 1;
                    PlayerChr.Target = hit.collider.gameObject.transform.parent.gameObject;
                    playerAgent.destination = movePoint;
                }
                
            }
        }

        if (PlayerChr.combatMan.IsPlayerTurn && !PlayerChr.combatMan.HasPerformedAction && !eventSystem.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Ray ray = PlayerCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    EnemyCharacter enemy = hit.collider.gameObject.GetComponent<EnemyCharacter>();

                    if (enemy.IsAlive && PlayerChr.combatMan.EnemyInRange(enemy.gameObject) && !PlayerChr.combatMan.HasPerformedAction)
                    {
                        enemyOptions.Enemy = enemy;
                        enemyOptions.gameObject.SetActive(true);
                        PlayerChr.combatMan.TurnPlayerCharacter(enemy);
                    }
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (PlayerChr.Moving)
        {
            //Vector3 targetDir = movePoint - PlayerChr.transform.position;

            //Vector3 newDirection = Vector3.RotateTowards(PlayerChr.gameObject.transform.forward, targetDir, 0.5f, 0.0f);
            //PlayerChr.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
            //playerRb.MoveRotation(Quaternion.Euler(newDirection));
            ///////////////////
            Vector3 targetDir = movePoint - playerRb.position;

            if (PlayerChr.Rotating)
            {
                Vector3 newDirection = Vector3.RotateTowards(playerRb.transform.forward, targetDir, 0.1f, 0.0f);
                newDirection.y = 0;
                playerRb.MoveRotation(Quaternion.LookRotation(newDirection));

                //PlayerChr.Rotating = false;
            }
            ////////////////////
            //Vector3 newDirection = Vector3.RotateTowards(playerRb.transform.forward, targetDir, 0.1f, 0.0f);
            //playerRb.MoveRotation(Quaternion.Euler(newDirection));
            //PlayerChr.gameObject.transform.rotation = Quaternion.Euler(newDirection);
            //playerRb.MoveRotation(Quaternion.Euler(new Vector3(0, 90.0f, 0)));
            //Vector3 inter = Vector3.MoveTowards(playerRb.position, movePoint, 0.1f);
            //inter.y = terrain.SampleHeight(inter) + playerHeight;
            //playerRb.MovePosition(inter);

            if (!playerAgent.pathPending && playerAgent.remainingDistance <= 0.3f)//Vector3.Distance(playerRb.position, movePoint) < 0.001f)
            {
                PlayerChr.Rotating = false;
                PlayerChr.Moving = false;
            }
        } 
        else if (PlayerChr.RotatingCombatTarget)
        {
            Vector3 targetDir = PlayerChr.Target.transform.position - playerRb.position;

            Vector3 newDirection = Vector3.RotateTowards(playerRb.transform.forward, targetDir, 0.1f, 0.0f);
            newDirection.y = 0;
            playerRb.MoveRotation(Quaternion.LookRotation(newDirection));

            if (playerRb.rotation == Quaternion.LookRotation(newDirection))
            {
                PlayerChr.RotatingCombatTarget = false;
            }
        }

    }

    private void LateUpdate()
    {
        if (PlayerChr.ReCenterCam)
        {
            PlayerChr.ReCenterCam = false;
            PlayerCam.transform.position = PlayerChr.transform.position + camDistance;
        }

        if (!PlayerChr.combatMan.IsCombat)
        {
            PlayerCam.transform.position = PlayerChr.transform.position + camDistance;
        } 
        else if (PlayerChr.combatMan.IsPlayerTurn)
        {
            MoveCameraWASD();
        }
        else
        {
            if (PlayerChr.CamFollowTarget)
                PlayerCam.transform.position = PlayerChr.CamFollowTarget.transform.position + camDistance;
        }
        
    }

    private void MoveCameraWASD()
    {
        PlayerCam.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * 0.1f, 0, Input.GetAxisRaw("Vertical") * 0.1f);

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    PlayerCam.transform.position += new Vector3(0, 0, 1);
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    PlayerCam.transform.position += new Vector3(0, 0, -1);
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    PlayerCam.transform.position += new Vector3(-1, 0, 0);
        //} 
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    PlayerCam.transform.position += new Vector3(1, 0, 0);
        //}
                
                
    }
}
