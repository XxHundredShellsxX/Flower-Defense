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

        seedText.text = player.getSeedCount().ToString()+ "/"+flower.getNumSeedsRequired().ToString();
        dirtText.text = player.getDirtCount().ToString() + "/" + flower.getNumDirtRequired().ToString();

    }
}
