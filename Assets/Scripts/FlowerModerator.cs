using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerModerator : MonoBehaviour {

    private Animator animator;
    private int growthStage = 0;
    private List<List<int>> requiredMaterial = new List<List<int>>();
    public List<BoxCollider2D> Platforms = new List<BoxCollider2D>();
    public BoxCollider2D GrowthPlatform;



    // Use this for initialization
    void Awake () {
        // initialize the material of (numSeeds, numDirt) we need for each growth stage
        requiredMaterial.Add(new List<int> {1, 1});
        requiredMaterial.Add(new List<int> { 3, 5 });
        requiredMaterial.Add(new List<int> { 6, 6 });
        requiredMaterial.Add(new List<int> { 8, 6 });
        animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        checkLevelUp();
		
	}

    public int getNumSeedsRequired()
    {
        Debug.Log(requiredMaterial[growthStage][1]);
        return requiredMaterial[growthStage][0];  
    }

    public int getNumDirtRequired()
    {
        return requiredMaterial[growthStage][1];
    }

    public void checkLevelUp()
    {
        if(requiredMaterial[growthStage][0] == 0 && requiredMaterial[growthStage][1] == 0)
        {
            if (growthStage == 0)
            {
                animator.SetTrigger("grow1");
            }
        }
    }

    public void useSeeds(int numSeeds)
    {
        requiredMaterial[growthStage][0] -= numSeeds;
    }

    public void useDirt(int numDirt)
    {
        requiredMaterial[growthStage][1] -= numDirt;
    }


}
