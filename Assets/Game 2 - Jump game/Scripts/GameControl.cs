using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameState { playing, gameover };

public class GameControl : MonoBehaviour {

	public Transform platformPrefab;
	public Transform dotPrefab;
	public Transform canvas;
    public static GameState gameState;

    private Transform playerTrans;
    private float platformsSpawnedUpTo = 0.0f;
    public ArrayList platforms;
	List<Platform> plats = new List<Platform> ();
    private float nextPlatformCheck = 0.0f;
	public int currentPlatform = -1;
	public Text timeText;
	public float initialTime = 60.0f;
	float currentTime = 0.0f;
	string minutes, seconds;

	//Camera camera;

	public Transform nextText;
	public Transform rulerTransform;

	Vector3 prevDot = Vector3.zero;
	Vector3 curDot = Vector3.zero;

	float correctDistance = 0.0f;

	public InputField inputText;
	public Text placeholder;
    
	void Awake () {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        platforms = new ArrayList();
        
		SpawnPlatforms(25.0f);

		nextText.localPosition = plats[0].dot.transform.localPosition + (-40) * Vector3.up + 60*Vector3.right;
		currentTime = initialTime;

        StartGame();
	}

	void Start()
	{
		
		inputText.Select ();
		inputText.ActivateInputField ();
	}

    void StartGame()
    {
        Time.timeScale = 1.0f;
        gameState = GameState.playing;

		//Set the first platform
		rulerTransform.GetComponent<Ruler> ().NextPlat = plats [0].transform;
    }

    void GameOver()
    {
        Time.timeScale = 0.0f; //Pause the game
        gameState = GameState.gameover;
        GameGUI.SP.CheckHighscore();
    }

	void Update () {
        //Do we need to spawn new platforms yet? (we do this every X meters we climb)
        float playerHeight = playerTrans.position.y;
        if (playerHeight > nextPlatformCheck)
        {
            PlatformMaintenaince(); //Spawn new platforms
        }

        //Update camera position if the player has climbed and if the player is too low: Set gameover.
        float currentCameraHeight = transform.position.y;
        float newHeight = Mathf.Lerp(currentCameraHeight, playerHeight, Time.deltaTime * 10);
        if (playerTrans.position.y > currentCameraHeight)
        {
            transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
        }else{
            //Player is lower..maybe below the cameras view?
            if (playerHeight < (currentCameraHeight - 10))
            {
                GameOver();
            }
        }

        //Have we reached a new score yet?
        if (playerHeight > GameGUI.score)
        {
            GameGUI.score = (int)playerHeight;
        }

		currentTime -= Time.deltaTime;
		minutes = ((int)currentTime / 60).ToString ();
		seconds = ((int)currentTime % 60).ToString ();
		if (float.Parse (seconds) < 10.0f)
			seconds = "0" + seconds;
		timeText.text = "Time: " + minutes + ":" + seconds;
		if (currentTime <= 0.0f)
			GameOver ();

		correctDistance = rulerTransform.GetComponent<Ruler> ().CorrectDistance;
	}



    void PlatformMaintenaince()
    {
		nextPlatformCheck = playerTrans.position.y + 10;

        //Delete all platforms below us (save performance)
        for(int i = platforms.Count-1;i>=0;i--)
        {
            Transform plat = (Transform)platforms[i];
            if (plat.position.y < (transform.position.y - 10))
            {
				Destroy(plat.GetComponent<Platform>().dot);
                Destroy(plat.gameObject);
                platforms.RemoveAt(i);
            }            
        }

        //Spawn new platforms, 25 units in advance
        SpawnPlatforms(nextPlatformCheck + 10);
    }


    void SpawnPlatforms(float upTo)
    {
        float spawnHeight = platformsSpawnedUpTo;
        while (spawnHeight <= upTo)
        {
            float x = Random.Range(-8.0f, 8.0f);
            Vector3 pos = new Vector3(x, spawnHeight, 12.0f);

            Transform plat = (Transform)Instantiate(platformPrefab, pos, Quaternion.identity);
			Transform dot = (Transform)Instantiate(dotPrefab, pos, Quaternion.identity);
			dot.SetParent(canvas);
			dot.localScale = Vector3.one;
			Vector3 tmpPos = camera.WorldToScreenPoint(plat.position);
			tmpPos.y -= Screen.height;
			tmpPos.z = 0;
			dot.localPosition = tmpPos + 5*Vector3.up;

			plat.GetComponent<Platform>().dot = dot.gameObject;

            platforms.Add(plat);
			plats.Add(plat.GetComponent<Platform>());

			spawnHeight += Random.Range(6.4f, 6.5f);
        }
        platformsSpawnedUpTo = upTo;

		if ((currentPlatform + 1) < plats.Count) {
			if (currentPlatform >= 0){
				prevDot = plats [currentPlatform].GetComponent<Platform> ().dot.transform.position;
				Transform curPlat = plats [currentPlatform].transform;
				playerTrans.position = curPlat.position;
				
				rulerTransform.GetComponent<Ruler> ().CurPlat = curPlat;

			rulerTransform.GetComponent<Ruler> ().NextPlat = plats [currentPlatform + 1].transform;
			
			nextText.GetComponent<Next> ().CurPlat = plats [currentPlatform + 1].transform;
			
			curDot = plats [currentPlatform + 1].GetComponent<Platform> ().dot.transform.position;

			}
			else
			{
				nextText.GetComponent<Next> ().CurPlat = plats [0].transform;
			}
		}
    }

	public void Submit()
	{
		float dis = float.Parse (inputText.text);
		if (dis < correctDistance + 0.2f && dis > correctDistance - 0.2f) {

			prevDot = plats [currentPlatform + 1].GetComponent<Platform> ().dot.transform.position;

			currentPlatform++;

			Transform curPlat = plats [currentPlatform].transform;
			playerTrans.position = curPlat.position;
		
			rulerTransform.GetComponent<Ruler> ().CurPlat = curPlat;

			if ((currentPlatform + 1) < plats.Count) {
				rulerTransform.GetComponent<Ruler> ().NextPlat = plats [currentPlatform + 1].transform;
				nextText.GetComponent<Next> ().CurPlat = plats [currentPlatform + 1].transform;
				curDot = plats [currentPlatform + 1].GetComponent<Platform> ().dot.transform.position;
			}

			placeholder.text = "Πληκτρολογήστε τη σωστή απόσταση που μετρήσατε και πατήστε το πλήκτρο Enter..";
		} else {
			placeholder.text = "Ξαναπροσπαθήστε!";
		}

		inputText.text = "";
		inputText.Select();
		inputText.ActivateInputField();
	}

}