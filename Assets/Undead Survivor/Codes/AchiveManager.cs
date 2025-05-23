using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;
     

    enum Achive {UnlockPotato, UnlockBean  }
    Achive[] achives;
    WaitForSecondsRealtime wait;


    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);

        if (!PlayerPrefs.HasKey("MyData")){
            Init();
        }
    } 

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Achive achive in achives){
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for(int index = 0; index < lockCharacter.Length; index++) {
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1 ;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach (Achive achive in achives){
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isachive = false;

        switch (achive){
            case Achive.UnlockPotato:
                isachive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                isachive = GameManager.instance.gameTime == GameManager.instance.maxgameTime;
                break;
        }
            if(isachive && PlayerPrefs.GetInt(achive.ToString()) == 0){
                PlayerPrefs.SetInt(achive.ToString(), 1);   //해금됐다고 저장

                for(int index = 0; index < uiNotice.transform.childCount; index++){
                    bool isActive = index == (int)achive;
                    uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
                }

                StartCoroutine(NoticeRoutine());
            }
    }

    IEnumerator NoticeRoutine()     //활성화했다 다시 사라짐
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;

        uiNotice.SetActive(false);
    }
}
