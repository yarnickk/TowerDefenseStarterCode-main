using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class ConstructionSite
{
    public Vector3Int TilePosition { get; set; }
    public Vector3 WorldPosition { get; set; }
    public SiteLevel Level { get; set; }
    public TowerType TowerType { get; set; }
    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        // Assign the tilePosition and worldPosition.
        this.TilePosition = tilePosition;
        this.WorldPosition = worldPosition;

        // Adjust the Y value of worldPosition (by 0.5)
        this.WorldPosition += new Vector3(0, 0.5f, 0);

        // Set tower to null initially
        this.tower = null;
    }

    public void SetTower(GameObject newTower, SiteLevel newLevel, TowerType newType)
    {
        // First, check if there is an existing tower
        if (tower != null)
        {
            // If there is an existing tower, destroy it first
            GameObject.Destroy(tower);
        }

        // Then assign the new tower
        tower = newTower;
        Level = newLevel;
        TowerType = newType;
    }

    public Vector3 BuildPosition()
    {
        return WorldPosition; // Or any other calculation for build position
    }

    public TowerType GetTowerType()
    {
        return TowerType;
    }
}
