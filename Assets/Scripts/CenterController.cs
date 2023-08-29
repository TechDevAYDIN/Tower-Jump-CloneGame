//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterController : MonoBehaviour
{

    private Vector3 startRotation;
    private Vector2 lastTapPos;

    private float helixDistance;
    public Transform topTransform;
    public Transform goalTransform;
    public Material partMaterial;
    public Material deadPartMaterial;
    public Material goalPartMaterial;
    public MeshRenderer bgMesh;
    public Material bgmeshMat;

    public GameObject LevelTopobj;
    public GameObject LevelTopprefab;

    public GameObject helixLevelPrefab0;
    public GameObject helixLevelPrefab1;
    public GameObject helixLevelPrefab2;
    public GameObject helixLevelPrefab3;
    public GameObject helixLevelPrefab4;
    public GameObject helixLevelPrefab5;
    public GameObject helixLevelPrefab6;
    public GameObject helixLevelPrefab7;
    public GameObject helixLevelPrefab8;
    public GameObject helixLevelPrefab9;
    public GameObject helixLevelPrefab10;
    public GameObject helixLevelPrefab11;
    public GameObject helixLevelPrefab12;
    public GameObject helixLevelPrefab13;
    public GameObject helixLevelPrefab14;
    public GameObject helixLevelPrefab15;
    public GameObject helixLevelPrefab16;
    public GameObject helixLevelPrefab17;
    public GameObject helixLevelPrefab18;
    public GameObject helixLevelPrefab19;
    public GameObject helixLevelPrefab20;
    public GameObject helixLevelPrefab21;
    public GameObject helixLevelPrefab22;
    public GameObject helixLevelPrefab23;
    public GameObject helixLevelPrefab24;
    public GameObject helixLevelPrefab25;

    public Image loadBar;
    public List<Stage> allStages = new List<Stage>();
    private List<GameObject> spawnedLevels = new List<GameObject>();

    public Transform rotationOBJ;
    [SerializeField] private Text levelCompletedText = null;
    private float delta;
    private int percent;
    // Start is called before the first frame update
    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
        LoadStage(GameManager.singleton.currentStage);
    }

    void Update()
    {
        if (GameManager.singleton.isAlive)
        {
            // Spin center by using click (or finger press) and drag
            if (Input.GetMouseButton(0))
            {

                Vector2 curTapPos = Input.mousePosition;

                if (lastTapPos == Vector2.zero)
                    lastTapPos = curTapPos;

                delta = lastTapPos.x - curTapPos.x;
                lastTapPos = curTapPos;

                transform.Rotate(Vector3.up * (delta / 14f));
                //transform.eulerAngles = Vector3.SlerpUnclamped(transform.eulerAngles, Vector3.up * (delta / 2), Time.deltaTime * 0.05f);
                //transform.Rotate(Vector3.up * Mathf.Lerp(transform.localEulerAngles.y, delta, Time.deltaTime*10f));
            }

            if (Input.GetMouseButtonUp(0))
            {
                lastTapPos = Vector2.zero;
            }
            loadBar.fillAmount = FindObjectOfType<PlayerMovement>().GetComponent<Transform>().position.y / goalTransform.position.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationOBJ.rotation, Time.deltaTime * 5f);
            percent = (int)((FindObjectOfType<PlayerMovement>().GetComponent<Transform>().position.y / goalTransform.position.y)*100f);
            levelCompletedText.text = percent.ToString() + "% LEVEL COMPLETED";
        } 
    }
    public void Resurrection()
    {
        Instantiate(LevelTopprefab, new Vector3(gameObject.transform.position.x, GameManager.singleton.yPassed - 3, gameObject.transform.position.z), Quaternion.identity, transform);
    }
    public void LoadStage(int stageNumber)
    {
        for (int i = stageNumber; i > allStages.Count-1; i -= 80)
        {
            Debug.Log("Stage " + stageNumber);
            stageNumber -= 80;
        }
        // Get the correct stage
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if (stage == null)
        {
            Debug.LogError("No stage " + stageNumber + " found in allStages list (HelixController). All stages assigned in list?");
            return;
        }

        goalTransform.position = new Vector3(0, -160 - (allStages[stageNumber].goalMesafe),0);
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
        // Set the new Center color
        GameObject.Find("Center").GetComponent<Renderer>().material.color = allStages[stageNumber].stageBackgroundColor;

        // Set the new DeadPart color
        deadPartMaterial.color = allStages[stageNumber].deadPartColor;

        // Set the new GoalPart color
        goalPartMaterial.color = allStages[stageNumber].stageBallColor;

        // Set the new Ball color
        FindObjectOfType<PlayerMovement>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        // Reset the helix rotation
        transform.localEulerAngles = startRotation;

        partMaterial.color = allStages[stageNumber].stageLevelPartColor;

        bgmeshMat.color = allStages[stageNumber].stageBackgroundMaterial;

        // Destroy the old levels if there are some
        foreach (GameObject go in spawnedLevels)
            Destroy(go);

        // Create the new levels
        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;
        float spawnRotY = topTransform.localRotation.y;
        /*
        if(GameManager.singleton.yPassed == 32)
        {
            Instantiate(LevelTopprefab, transform);

        }
        */
        for (int i = 1; i < stage.levels.Count; i++)
        {
            GameObject level;
            int x = Random.Range(stage.levels[i].minValue, stage.levels[i].maxValue);
            switch (x)
            {
                case 1:
                    spawnPosY -= levelDistance;
                    spawnRotY += Random.Range(-30, 30);
                    level = Instantiate(helixLevelPrefab0, transform);
                    level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                    level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                    spawnedLevels.Add(level);
                    break;
                    case 2:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab1, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 3:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab2, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 4:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab3, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 5:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab4, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 6:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab5, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 7:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab6, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 8:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab7, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 9:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab8, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 10:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab9, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 11:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab10, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 12:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab11, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 13:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab12, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 14:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab13, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 15:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab14, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 16:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab15, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 17:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab16, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 18:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab17, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 19:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab18, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 20:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab19, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 21:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab20, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 22:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab21, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 23:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab22, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 24:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab23, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 25:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab24, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
                    case 26:
                        spawnPosY -= levelDistance;
                        spawnRotY += Random.Range(-180, 180);
                        level = Instantiate(helixLevelPrefab25, transform);
                        level.transform.localPosition = new Vector3(0, spawnPosY, 0);
                        level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
                        spawnedLevels.Add(level);
                        break;
            }


            /*
            spawnPosY -= levelDistance;
            spawnRotY += Random.Range(-20, 20);
            GameObject level = Instantiate(helixLevelPrefab, transform);
            Debug.Log("Spawned Level");
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            level.transform.eulerAngles = new Vector3(0, spawnRotY, 0);
            spawnedLevels.Add(level);
            */


            /*
            // Disable some parts (depending on level setup)
            int partsToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            Debug.Log("Should disable " + partsToDisable);

            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                    Debug.Log("Disabled Part");
                }
            }*/
        }
    }
}