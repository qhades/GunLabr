using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnableObject<T>
{
    private struct chanceBounderies
    {
        public T spawnableObject;
        public int lowBoundaryValue;
        public int highBoundaryValue;
    }

    private int ratioValueTotal = 0;
    private List<chanceBounderies> chanceBounderiesList = new List<chanceBounderies>();
    private List<SpawnableObjectByLevel<T>> spawnableObjectByLevelList;
    
    public RandomSpawnableObject(List<SpawnableObjectByLevel<T>> spawnableObjectByLevelList)
    {
        this.spawnableObjectByLevelList = spawnableObjectByLevelList;
    }

    public T GetItem()
    {
        int upperBoundary = -1;
        ratioValueTotal = 0;
        chanceBounderiesList.Clear();
        T spawnableObject = default(T);

        foreach (SpawnableObjectByLevel<T> spawnableObjectByLevel in spawnableObjectByLevelList)
        {
            if (spawnableObjectByLevel.dungeonLevel == GameManager.Instance.GetCurrentDungeonLevel())
            {
                foreach (SpawnableObjectRatio<T> spawnableObjectRatio in spawnableObjectByLevel.spawnableObjectRatioList)
                {
                    int lowerBoundary = upperBoundary + 1;

                    upperBoundary = lowerBoundary + spawnableObjectRatio.ratio - 1;

                    ratioValueTotal += spawnableObjectRatio.ratio;

                    chanceBounderiesList.Add(new chanceBounderies() { spawnableObject = spawnableObjectRatio.dungeonObject, lowBoundaryValue = lowerBoundary, highBoundaryValue = upperBoundary });
                }
            }
        }

        if (chanceBounderiesList.Count == 0) return default(T);

        int lookUpValue = Random.Range(0, ratioValueTotal);

        foreach (chanceBounderies spawnChance in chanceBounderiesList)
        {
            if (lookUpValue >= spawnChance.lowBoundaryValue && lookUpValue <= spawnChance.highBoundaryValue)
            {
                spawnableObject = spawnChance.spawnableObject;
                break;
            }
        }

        return spawnableObject;
    }
}