using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlowerModerator : MonoBehaviour {

    private Animator animator;
    private int growthStage = 0;
    private List<List<int>> requiredMaterial = new List<List<int>>();
    public List<BoxCollider2D> Platforms = new List<BoxCollider2D>();
    public GameObject GrowthPlatform;
    private bool canGrow = false;

    private GameObject hpBar;

    // Use this for initialization
    void Awake () {
        // initialize the material of (numSeeds, numDirt) we need for each growth stage
        requiredMaterial.Add(new List<int> {0, 1});
        requiredMaterial.Add(new List<int> { 1, 0 });
        requiredMaterial.Add(new List<int> { 1, 0 });
        requiredMaterial.Add(new List<int> { 1, 0 });
        animator = GetComponent<Animator>();
        //hpBar = Transform.FindObjectOfType();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        checkLevelUp();
		
	}

    public int getNumSeedsRequired()
    {
        
        return requiredMaterial[Math.Min(growthStage, 3)][0];  
    }

    public int getNumDirtRequired()
    {
        return requiredMaterial[Math.Min(growthStage, 3)][1];
    }

    public void checkLevelUp()
    {
        if(requiredMaterial[Math.Min(growthStage, 3)][0] == 0 && requiredMaterial[Math.Min(growthStage, 3)][1] == 0)
        {
            
            changeGrowthPlatformPos();
            handleAnimations();
            enablePlatforms();
            growthStage++;

        }
    }

    public void readyToGrow()
    {
        canGrow = true;
    }

    public void useSeeds(int numSeeds)
    {
        requiredMaterial[Math.Min(growthStage, 3)][0] -= numSeeds;
    }

    public void useDirt(int numDirt)
    {
        requiredMaterial[Math.Min(growthStage, 3)][1] -= numDirt;
    }

    private void changeGrowthPlatformPos()
    {
        if (growthStage == 0)
        {
            GrowthPlatform.transform.position = new Vector2(GrowthPlatform.transform.position.x, -1.22f);
        }
        if (growthStage == 1)
        {
            GrowthPlatform.transform.position = new Vector2(GrowthPlatform.transform.position.x, 2.08f);
        }
        if (growthStage == 2)
        {
            GrowthPlatform.transform.position = new Vector2(GrowthPlatform.transform.position.x, 5.44f);
        }
        if (growthStage == 3)
        {
            GrowthPlatform.transform.position = new Vector2(GrowthPlatform.transform.position.x, 8.63f);
        }
    }

    private void enablePlatforms()
    {
        if (growthStage == 0)
        {
            Platforms[0].enabled = true;
        }
        if (growthStage == 1)
        {
            Platforms[1].enabled = true;
            Platforms[2].enabled = true;
        }
        if (growthStage == 2)
        {
            Platforms[3].enabled = true;
            Platforms[4].enabled = true;
        }
        if (growthStage == 3)
        {
            Platforms[5].enabled = true;
            Platforms[6].enabled = true;
        }
    }

    private void handleAnimations()
    {
        if (growthStage == 0)
        {
            animator.SetTrigger("grow1");
        }
        if (growthStage == 1)
        {
            animator.SetTrigger("grow2");
        }
        if (growthStage == 2)
        {
            animator.SetTrigger("grow3");
        }
        if (growthStage == 3)
        {
            animator.SetTrigger("grow4");
        }
    }

}
