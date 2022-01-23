using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Tube : MonoBehaviour
{
    public Stack<Ball> balls;

    public static Tube Instance;

    public GameObject[] allBall;

    internal void Start()
    {
        Instance = this;
    }

    public void createTube(int limitTube, int totalBall)
    {
        balls = new Stack<Ball>(4);
        Vector3 tagert = transform.position;
        tagert.y = -2.98f;
        Vector3 change = new Vector3();
        if (limitTube < totalBall - 2)
        {
            for (int i = 0; i < 4; i++)
            {
                int ranDomBall = ranDom(totalBall);
                Ball ball = new Ball();
                GameObject objectBall = Instantiate(allBall[ranDomBall], tagert, Quaternion.identity);

                change.x = tagert.x;
                change.y = tagert.y + 1.05f;
                change.z = tagert.z;

                ball.color = objectBall;
                balls.Push(ball);
                tagert = change;

            }
        }
    }

    internal int ranDom(int totalBall)
    {
        bool check = true;
        int ranDom = 0;
        while (check)
        {
            ranDom = Random.Range(0, totalBall - 2);
            GameController.Instance.checkRanDom[ranDom]++;
            if (GameController.Instance.checkRanDom[ranDom] <= 4)
            {
                check = false;
            }
        }
        return ranDom;
    }

    public void moveBallToTube(Vector3 psTube,int coutTubeNext)
    {
        Vector3 change = new Vector3();
        for (int i = 0; i < 4; i++)
        {
            if (i == coutTubeNext)
            {
                change.x = psTube.x;
                change.y = -2.98f + i * 1.05f;
                change.z = psTube.z;
            }
        }
        balls.Pop().color.transform.DOMove(change, 0.3f);
    }
    public void moveToTube(Vector3 psTube)
    {
        Vector3 psTubeF = new Vector3();
        psTubeF.x = psTube.x;
        psTubeF.y = psTube.y + 2.8f;
        psTubeF.z = psTube.z;
        balls.Peek().color.transform.DOMove(psTubeF, 0.3f);
    }
    public void comeBackTube(Vector3 psTube, int coutTubeNext)
    {
        Vector3 change = new Vector3();
        for (int i = 0; i < 4; i++)
        {
            if (i == coutTubeNext)
            {
                change.x = psTube.x;
                change.y = -1.8f + i * 1.05f-1.18f;
                change.z = psTube.z;
            }
        }
        balls.Peek().color.transform.DOMove(change,0.3f);

    }
    public void selBall(Vector3 psTube)
    {
        Vector3 change = new Vector3();
        change.x = balls.Peek().color.transform.position.x;
        change.y = psTube.y + 2.8f;
        change.z = balls.Peek().color.transform.position.z;
        balls.Peek().color.transform.position = change;
    }
    public bool checkAllSameBall()
    {
        int i = 0;
        bool checkAllSameBall = true;
        string[] nameTag = new string[balls.Count];
        foreach (var item in balls)
        {
            nameTag[i] = item.color.tag;
            i++;
        }
        for (int index = 0; index < nameTag.Length; index++)
        {
            for(int j = index + 1; j < nameTag.Length; j++)
            {
              if(string.Equals(nameTag[index],nameTag[j])==false)
                { 
                    checkAllSameBall = false;
                }    
            }
        }
        return (checkAllSameBall==true &&balls.Count==4)?true:false;
    }
    IEnumerator PrintfAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Done " + Time.time);
    }
    internal void Update()
    {
    }
}
