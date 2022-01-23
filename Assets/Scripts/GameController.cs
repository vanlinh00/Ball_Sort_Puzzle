using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject Tube;

    public int[] checkRanDom = new int[11];

    internal GameObject[] allTube = new GameObject[12];

    internal int ranDomTube = 0;

    internal int checkClick = 0;

    internal int psTubleRequire = -1;
    int checkDonelevel = 0;
    public AudioClip addBallClip;
    public AudioClip doneTubeclip;
    public AudioSource audioSoure;

    internal void Start()
    {
        Instance = this;
        audioSoure = audioSoure.GetComponent<AudioSource>();
        ranDomTube = UnityEngine.Random.Range(5, 8);
        createLevel(ranDomTube);
    }

    public void createLevel(int totalBall)
    {
        Vector3 Poschange;
        for (int i = 0; i < totalBall; i++)
        {
            GameObject createTube = Instantiate(Tube, transform.position, Quaternion.identity);
            createTube.GetComponent<Tube>().createTube(i, totalBall);
            createTube.tag = "tube" + i;
            allTube[i] = createTube;
            Poschange.x = transform.position.x + 2.35f;
            Poschange.y = transform.position.y;
            Poschange.z = 0;
            transform.position = Poschange;

        }
    }

    public void CheckRanDom()
    {
      SceneManager.LoadScene(1);
    }

    internal void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                String tubeSelect = hit.collider.gameObject.tag;
                int psTubleMove = checkTube(tubeSelect);
                checkClick++;
                if (checkClick == 1)
                {
                    psTubleRequire = psTubleMove;
                    if (allTube[psTubleRequire].GetComponent<Tube>().balls.Count != 0)
                    {
                        moveBallAudio();
                        allTube[psTubleRequire].GetComponent<Tube>().selBall(allTube[psTubleRequire].GetComponent<Tube>().transform.position);

                    }
                }
                if (checkClick == 2)
                {
                    int countBallInTubeMove = allTube[psTubleMove].GetComponent<Tube>().balls.Count;  // vi tri muon den
                    int countBallTube2 = allTube[psTubleRequire].GetComponent<Tube>().balls.Count;    // vi tri bat dau

                    Vector3 checkTubePs = allTube[psTubleMove].GetComponent<Tube>().transform.position;
                    Vector3 checkTubeStart = allTube[psTubleRequire].GetComponent<Tube>().transform.position;

                    if (countBallTube2 != 0)
                    {
                        moveBallAudio();

                        if (countBallInTubeMove == 0)
                        {  
                            allTube[psTubleMove].GetComponent<Tube>().balls.Push(allTube[psTubleRequire].GetComponent<Tube>().balls.Peek());


                            allTube[psTubleRequire].GetComponent<Tube>().moveBallToTube(checkTubePs, countBallInTubeMove);
                            if(allTube[psTubleMove].GetComponent<Tube>().checkAllSameBall())
                            {
                                tubleFulllSameColorAudio();
                                
                            }    
                        }
                        else
                        {
                            if (sameColor(psTubleMove, psTubleRequire) && countBallInTubeMove < 4)
                            {
                                allTube[psTubleMove].GetComponent<Tube>().balls.Push(allTube[psTubleRequire].GetComponent<Tube>().balls.Peek());
                                int countBallMove = (psTubleMove == psTubleRequire) ? countBallInTubeMove - 1 : countBallInTubeMove;
              
                                allTube[psTubleRequire].GetComponent<Tube>().moveBallToTube(checkTubePs, countBallMove);
                               
                                if (allTube[psTubleMove].GetComponent<Tube>().checkAllSameBall())
                                {
                                   tubleFulllSameColorAudio();
                                }
                            }
                            else
                            {
                                allTube[psTubleRequire].GetComponent<Tube>().comeBackTube(checkTubeStart, countBallTube2 - 1);
                            }
                        }
                    }
                    checkClick = 0;
                }
            }
        }
    }

    public int checkTube(String tubelSelect)
    {
        int checkTube = 0;

        switch (tubelSelect)
        {
            case "tube0":
                checkTube = 0;
                break;
            case "tube1":
                checkTube = 1;
                break;
            case "tube2":
                checkTube = 2;
                break;
            case "tube3":
                checkTube = 3;
                break;
            case "tube4":
                checkTube = 4;
                break;
            case "tube5":
                checkTube = 5;
                break;
            case "tube6":
                checkTube = 6;
                break;
            case "tube7":
                checkTube = 7;
                break;

        }
        return checkTube;
    }

    public bool sameColor(int psTubeA, int psTubeB)
    {
        return (allTube[psTubeA].GetComponent<Tube>().balls.Peek().color.tag.Equals(allTube[psTubeB].GetComponent<Tube>().balls.Peek().color.tag)) ? true : false;
    }
    public void moveBallAudio()
    {
        audioSoure.clip = addBallClip;
        audioSoure.Play();
     
    }
    public void tubleFulllSameColorAudio()
    {
        audioSoure.clip = doneTubeclip;
        audioSoure.Play();
        UiController.Instance.addCoin();
    }

}
