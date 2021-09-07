using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zsound : MonoBehaviour
{
    public AudioSource[] sound = new AudioSource[10];
    public Transform player;
    public Transform zom;
    public NPC npc;

    public AudioSource DS;
    AudioSource ss;

    // 좀비 워킹 소리
    public AudioSource[] Walksound = new AudioSource[2];
    int idx;
    float interval;
    void Awake()
    {
        idx = 0;
        for (int i = 0; i < 2; i++)
            Walksound[i] = Walksound[i].GetComponent<AudioSource>();
        Zwalking();
    }
    void Zwalking() // 좀비 walking sound는 항상나게 하되 거리와 방향에 영향
    {
        // 좀비와의 거리 측정
        float dis = Vector2.Distance(zom.position, player.position) / 100;
        dis = dis > 1 ? 1 : dis;

        // 소리 간격 결정
        if (dis > 0.7f) interval = 0.7f;
        else if (dis < 0.3f) interval = 0.3f;
        else interval = dis;

        // 오른쪽 왼쪽 구분
        if (zom.position.x - player.position.x < -8)
            Walksound[idx].panStereo = -0.8f;
        else if (zom.position.x - player.position.x > 8)
            Walksound[idx].panStereo = 0.8f;
        else
            Walksound[idx].panStereo = 0f;

        // 소리 실행
        Walksound[idx].volume = 1 - dis;
        Walksound[idx].Play();
        idx = (idx + 1) % 2;

        Invoke("Zwalking", interval);
    }
    public void SoundOn(AudioSource S)
    {
        if(!S.isPlaying)
            S.Play();

        ss = S;
        if (zom.position.x - player.position.x < -8)
            S.panStereo = -0.8f;
        else if (zom.position.x - player.position.x > 8)
            S.panStereo = 0.8f;
        else
            S.panStereo = 0f;

        float dis = Vector2.Distance(zom.position, player.position) / 120;
        dis = dis > 1 ? 1 : dis;
        S.volume = 1-dis;
    }
    public void soundOff()
    {
        if(ss.isPlaying) ss.Stop();
    }
    // 좀비소리 중지
    public void stop_zSound()
    {
        CancelInvoke("Zwalking");
    }
    public void EnterChase()
    {
        if (sound[2].isPlaying || sound[3].isPlaying) return;
        int randomValue = Random.Range(2,3);
        sound[randomValue].Play();
    }

    public void EnterSearch()
    {
        int randomValue;
        randomValue = Random.Range(0,1);
        if(!sound[randomValue].isPlaying)
            sound[randomValue].Play();
        Debug.Log("search");
    }

    public void GameOver()
    {
        int randomValue;
        randomValue = Random.Range(5, 6);

        if(!sound[randomValue].isPlaying)
            sound[randomValue].Play();
    }

    public void DistanceSonar(){
        SoundOn(DS);
        Invoke("soundOff", 5f);
    }
}
