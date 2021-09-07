using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class NPC : MonoBehaviour
{
    public int stateNum = 0;

    Transform target;
    Transform PlayerPos;
    public Transform PlayerLastKnownPos;

    public GameObject player;

    public float speed = 10f;
    public float nextWaypointDistance = 3f;

    public Transform npcGFX;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;
    bool reachedEndofPath = false;

    GameObject[] waypoints;
    GameObject Closest_waypoint = null;
    GameObject Random_waypoint = null;
    GameObject Farest_waypoint = null;
    Vector2 direction;
    float closestDist = Mathf.Infinity;
    float farestDist = 0;

    public GameObject polygonView;
    public NPCView npcViewCircle;
    public NPCView npcViewPolygon;
    int polygonDir = 1;

    public bool is_Chase = false;
    public bool is_Attacked = false;
    public bool is_alarm = false;
    public bool is_alarm2 = false;
    public Transform alarmPos;

    public DoorMove firstDoor;

    public float curPx;
    public float curPy;
    public float nextPx;
    public float nextPy;
    public Zanim z;
    public Zsound zs;
    void Start()
    {
        PlayerPos = player.GetComponent<Transform>();
        target = PlayerPos;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        stateNum = 1;

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    void UpdatePath()
    {
      if(seeker.IsDone()){
        seeker.StartPath(rb.position, target.position, OnPathComplete);
      }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error){
          path = p;
          currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if(path == null)
          return;

        if(currentWaypoint >= path.vectorPath.Count){
          reachedEndofPath = true;
          return;
        }
        else{
          reachedEndofPath = false;
        }

        NPCState();

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime * 100;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        curPy = path.vectorPath[currentWaypoint].y;
        curPx = path.vectorPath[currentWaypoint].x;

        if (distance < nextWaypointDistance){
          currentWaypoint++;
          //nextPy = path.vectorPath[currentWaypoint].y;
          //nextPx = path.vectorPath[currentWaypoint].x;
          //if (Mathf.Abs(nextPy-curPy) >= Mathf.Abs(nextPx-curPx)) z.CHk();
        }
        float pvx = rb.position.x - target.position.x;
        if(Mathf.Abs(rb.velocity.x) <= Mathf.Abs(rb.velocity.y) - 3f){
          if(rb.velocity.y < 0){
            polygonDir = 1;
          }
          else if(rb.velocity.y > 0){
            polygonDir = 2;
          }
        }
        else{
          if(pvx >= 0){
            npcGFX.localScale = new Vector3(1f,1f,1f);
            polygonDir = 3;
          }
          else if(pvx < 0){
            npcGFX.localScale = new Vector3(-1f,1f,1f);
            polygonDir = 4;
          }
        }
        PolygonDirection();
    }

    void PolygonDirection(){
      if(polygonDir == 1) polygonView.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
      else if(polygonDir == 2) polygonView.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
      else if(polygonDir == 3) polygonView.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
      else if(polygonDir == 4) polygonView.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 270));
    }

    void NPCState(){

        float playerDist = Vector2.Distance(rb.position, PlayerPos.position);
        Debug.Log(playerDist);
        // 숨을 경우 Lost 상태에 돌입
        if (player.GetComponent<Player>().is_Hide) stateNum = 4;
        // 2단계 일정거리 안에 있을 경우 뛰거나 호근 후레시를 키거나 가까운 곳에 있을 경우
        else if (playerDist <= 120 || player.GetComponent<Player>().is_finding())
        { 
            PlayerLastKnownPos.position = PlayerPos.position;
            stateNum = 2;
            zs.EnterSearch();
        }
        else
            stateNum = 1;

        // 3단계 발견할 경우
        if(npcViewCircle.is_seen || npcViewPolygon.is_seen)
        {
            stateNum = 3; zs.EnterChase();
        }
        else{
            if(is_Chase){
                is_Chase = false;
                stateNum = 2;
                zs.EnterSearch();
            }
        }

      // 4 states
      if(stateNum == 1) Idle();
      else if(stateNum == 2) Search();
      else if(stateNum == 3) Chase();
      else if(stateNum == 4) Lost();

      if(is_alarm) AlarmHeard(alarmPos);

      else if(is_alarm2){
        stateNum = 1;
        is_alarm2 = false;
      }

      if(is_Attacked){
        speed = 0;
        Invoke("Attacked", 3f);
      }

      if(firstDoor.locked) speed = 0;
    }

    void Closest(){
      closestDist = Mathf.Infinity;
      foreach (GameObject waypoint in waypoints){
        float wpDistance = Vector2.Distance(waypoint.transform.position, PlayerPos.position);
        if(wpDistance < closestDist){
          Closest_waypoint = waypoint;
          closestDist = wpDistance;
        }
      }
    }

    void Farest(){
      farestDist = 0;
      foreach (GameObject waypoint in waypoints){
        float wpDistance = Vector2.Distance(waypoint.transform.position, PlayerPos.position);
        if(wpDistance > farestDist){
          Farest_waypoint = waypoint;
          farestDist = wpDistance;
        }
      }
    }

    void Idle(){
        Random();
        speed = 15f;
        target = Random_waypoint.GetComponent<Transform>();
    }

    void Random()
    {
        foreach (GameObject waypoint in waypoints)
        {
            float wpDistance = Vector2.Distance(waypoint.transform.position, PlayerPos.position);
            if (wpDistance <= 50f)
            {
                Random_waypoint = waypoint;
                return;
            }
        }
    }

    void Search(){
        Closest();
        speed = 20f;
        target = Closest_waypoint.GetComponent<Transform>();
    }

    void Chase(){
      target = PlayerPos;
      speed = 40f;
      is_Chase = true;
    }

    void Lost(){
        Farest();
        float pcDist = Vector2.Distance(this.gameObject.transform.position, PlayerPos.position);
        speed = 20f;
        if (pcDist < 30) return; // 좀비와 플레이가 위치가 가까우면 state 바뀌지 않음.
        target = Farest_waypoint.GetComponent<Transform>();
    }

    void Attacked(){
      is_Attacked = false;
      stateNum = 2;
      zs.EnterSearch();
    }

    void OnCollisionEnter2D(Collision2D col){
      if(col.gameObject.tag == "Player"){
            zs.stop_zSound();
            zs.GameOver();
            GameObject BG = GameObject.Find("BackGround");
            Destroy(BG);
            AutoFade.LoadLevel("GameOver", 3, 1, Color.black);
      }
    }
    public void AlarmHeard(Transform alarmPos){
      if(stateNum != 3){
        is_alarm2 = true;
        target = alarmPos;
      }
    }
}
