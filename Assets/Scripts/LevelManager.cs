using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    private FlowerModerator flower;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private Text seedText;

    [SerializeField]
    private Text dirtText;

    [SerializeField]
    private Enemy eatingEnemy;

    [SerializeField]
    private Enemy flyingEnemy;

    private int prevGrowthState = -1;
    private int enemyLimit;

    // Use this for initialization
    void Awake() {

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // update player giving seeds to flower when it has enough
        if (player.onGrowthPlatform())
        {
            int numSeedsNeeded = flower.getNumSeedsRequired();
            int numDirtNeeded = flower.getNumDirtRequired();
            flower.useSeeds(player.getSeeds(numSeedsNeeded));
            flower.useDirt(player.getDirt(numDirtNeeded));
        }
        string seedDisplay;
        if(flower.getNumSeedsRequired() != 0)
        {
            seedDisplay = player.getSeedCount().ToString() + "/"+flower.getNumSeedsRequired().ToString();
        }
        else
        {
            seedDisplay = "COMPLETE";
        }
        seedText.text = seedDisplay;
        string dirtDisplay;
        if (flower.getNumDirtRequired() != 0)
        {
            dirtDisplay = player.getDirtCount().ToString() + "/" + flower.getNumDirtRequired().ToString();
        }
        else
        {
            dirtDisplay = "COMPLETE";
        }
        dirtText.text =  dirtDisplay;

        if(flower.getGrowth() > prevGrowthState)
        {
            Spawn();
            prevGrowthState++;
        }
        GenerateEnemies();

    }

    void GenerateEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int numberOfEnemies = enemies.Length;
        if (numberOfEnemies < enemyLimit)
        {
            Debug.Log(numberOfEnemies);
            spawnRandomEnemy();
        }
    }

    void Spawn()
    {
        if (flower.getGrowth() == 0)
        {
            enemyLimit = 3;
        }
        else if (flower.getGrowth() == 1)
        {
            enemyLimit = 3;
        }
        else if (flower.getGrowth() == 2)
        {
            enemyLimit = 3;
        }
        else
        {
            enemyLimit = 3;
        }


    }

    void spawnRandomEnemy()
    {
        float side = Random.Range(0, 3);
        float chance = Random.Range(0, 5);
        if(chance < 3)
        {
            float y = -3.4f;
            float x;
            if (side < 1)
            {
                x = Random.Range(-25, -20);
            }
            else
            {
                x = Random.Range(12, 17);
            }
            Instantiate(eatingEnemy, new Vector3(x, y, 0), Quaternion.identity);
        }
        else
        {
            float y = Random.Range(-3, 0);
            float x;
            if (side < 1)
            {
                x = Random.Range(-25, -20);
            }
            else
            {
                x = Random.Range(12, 17);
            }
            Instantiate(flyingEnemy, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    void DestroyAllEnemiess()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
