using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlockSpawner : MonoBehaviour {

    public GameObject[] blocks;
    public PlayerController player;
    public Text points;
    public Text WSForward;
    public Text ADRotate;
    public Text SpaceJump;
    public Text ShiftGrab;
    public Text hitByLava;
    public Text highScoreT;
    float lavaAlpha = 1;
    float time;
    public static bool begin;
    int state = 0;

    public int highScore;

    public GameObject block;

    public GameObject tutorialBlock;
	// Use this for initialization
	void Start () {
        WSForward.color = new Color(0, 0, 0, 0);
        ADRotate.color = new Color(0, 0, 0, 0);
        ShiftGrab.color = new Color(0, 0, 0, 0);
        hitByLava.color = new Color(0, 0, 0, 0);
        if (!AppModel.TutorialMade)
            tutorialBlock = Instantiate(block, new Vector3(2, 1, 2), Quaternion.identity);
        if (PlayerPrefs.HasKey("highscore"))
            highScore = PlayerPrefs.GetInt("highscore");
        else
        {
            highScore = 0;
            PlayerPrefs.SetInt("highscore", 0);
        }

    }

    // Update is called once per frame
    void Update() {

        if (AppModel.TutorialMade || tutorialBlock.transform.position.y < GameObject.Find("Lava").transform.position.y)
            state = 4;

        if (state == 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)))
            state += 1;
        if (state == 1 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            state += 1;
        if (state == 2 && (Input.GetKeyDown(KeyCode.Space)))
            state += 1;
        if (state == 3 && tutorialBlock.transform.parent != null)
            state += 1;

        if(state == 0)
        {
            WSForward.color = new Color(0, 0, 0, 1);
            ADRotate.color = new Color(0, 0, 0, 0);
            SpaceJump.color = new Color(0, 0, 0, 0);
            ShiftGrab.color = new Color(0, 0, 0, 0);
        }
        if (state == 1)
        {
            WSForward.color = new Color(0, 0, 0, 0);
            ADRotate.color = new Color(0, 0, 0, 1);
            SpaceJump.color = new Color(0, 0, 0, 0);
            ShiftGrab.color = new Color(0, 0, 0, 0);
        }
        if (state == 2)
        {
            WSForward.color = new Color(0, 0, 0, 0);
            ADRotate.color = new Color(0, 0, 0, 0);
            SpaceJump.color = new Color(0, 0, 0, 1);
            ShiftGrab.color = new Color(0, 0, 0, 0);
        }
        if (state == 3)
        {
            WSForward.color = new Color(0, 0, 0, 0);
            ADRotate.color = new Color(0, 0, 0, 0);
            SpaceJump.color = new Color(0, 0, 0, 0);
            ShiftGrab.color = new Color(0, 0, 0, 1);
            
        }

        if(state== 4)
        {
            WSForward.color = new Color(0, 0, 0, 0);
            ADRotate.color = new Color(0, 0, 0, 0);
            SpaceJump.color = new Color(0, 0, 0, 0);
            ShiftGrab.color = new Color(0, 0, 0, 0);
            hitByLava.color = new Color(0, 0, 0, 1);
            begin = true;
            AppModel.TutorialMade = true;
            state = 5;
        }
        if (begin) {
            lavaAlpha -= 0.01f ;
            hitByLava.color= new Color(0,0,0, lavaAlpha);

        }
        if (!player.dead)
        {
            highScoreT.color = new Color(0, 0, 0, 0);
        }

        if (begin) {
            time += Time.deltaTime;
            if (!player.dead)
            {
                points.text = ((int)time).ToString();
            }
        if(player.dead && highScoreT.color.a == 0)
            {
                highScoreT.color = new Color(0, 0, 0, 1);
                
                if (PlayerPrefs.GetInt("highscore") < ((int)time))
                    PlayerPrefs.SetInt("highscore", (int)time);
                highScoreT.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString();
            }

        if (Time.frameCount % 60 == 0 && Random.value < 1)
            Instantiate(blocks[Random.Range(0, blocks.Length)], new Vector3(Random.Range(-3, 4), 10 + Mathf.Round(player.transform.position.y), Random.Range(-3, 4)),  Quaternion.Euler(Random.Range(0,2) * 90f, Random.Range(0, 2) * 90f, Random.Range(0, 4) * 90f));
        }
    }
}
