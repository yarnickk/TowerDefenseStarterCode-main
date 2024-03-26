using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class GameManager : MonoBehaviour
{
    public string creditsText;
    private int credits = 0;
    private int health = 0;
    private int currentWave = 0;
    private bool waveActive = false; // Hernoemde variabele toegevoegd

    public TopMenu topMenu;
    public EnemySpawner enemySpawner; // Nieuwe variabele toegevoegd

    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public GameObject towerMenuObject;
    private TowerMenu towerMenu;

    public List<GameObject> archerTowers;
    public List<GameObject> swordTowers;
    public List<GameObject> wizardTowers;

    private ConstructionSite selectedSite;

    public ConstructionSite SelectedSite
    {
        get { return selectedSite; }
        set { selectedSite = value; }
    }

    void Start()
    {
        towerMenu = towerMenuObject.GetComponent<TowerMenu>();
        StartGame();
    }

    void StartGame()
    {
        credits = 200;
        health = 10;
        currentWave = 0;
        
        waveActive = false; // Initialisatie van waveActive toegevoegd
    }

    public void AttackGate()
    {
        health--;
        
    }

    public void AddCredits(int amount)
    {
        credits += amount;
        
    }

    public void RemoveCredits(int amount)
    {
        credits -= amount;
    }

    public int GetCredits()
    {
        return credits;
    }

    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        // Implementeer deze functie om de kosten van torens te bepalen
        return 0;
    }

   
    public void SelectSite(ConstructionSite site)
    {
        selectedSite = site;
        towerMenu.SetSite(selectedSite);
    }

    public void Build(TowerType type, SiteLevel level)
    {
        // Controleer of er een locatie is geselecteerd
        if (selectedSite == null)
        {
            Debug.Log("Geen bouwlocatie geselecteerd.");
            return;
        }

        GameObject towerPrefab = null;

        // Selecteer het juiste prefab op basis van het torentype
        switch (type)
        {
            case TowerType.Archer:
                towerPrefab = archerTowers[(int)level];
                break;
            case TowerType.Sword:
                towerPrefab = swordTowers[(int)level];
                break;
            case TowerType.Wizard:
                towerPrefab = wizardTowers[(int)level];
                break;
            default:
                Debug.LogError("Ongeldig torentype.");
                return;
        }

        // Creëer een nieuwe toren op de geselecteerde locatie
        GameObject newTower = Instantiate(towerPrefab, selectedSite.WorldPosition, Quaternion.identity);

        // Configureer de geselecteerde locatie om de toren te bevatten
        selectedSite.SetTower(newTower, level, type);

        // Verberg het menu door null door te geven aan de SetSite-functie in TowerMenu
        towerMenu.SetSite(null);
    }

    // Functies voor het beheren van golven toegevoegd
    public void StartWave()
    {
        currentWave++;
        topMenu.SetWaveLabel("Wave: " + currentWave);
        waveActive = true;
        enemySpawner.StartWave(currentWave);
    }

    public void EndWave()
    {
        waveActive = false;
        topMenu.EnableWaveButton();
    }

    // Functies om het aantal vijanden bij te houden toegevoegd
    private int enemyInGameCounter = 0; // Variabele toegevoegd om het aantal vijanden bij te houden

    public void AddInGameEnemy()
    {
        // Verhoog het aantal vijanden in het spel
        // Deze functie wordt aangeroepen door EnemySpawner
        enemyInGameCounter++;
    }

    public void RemoveInGameEnemy()
    {
        // Verlaag het aantal vijanden in het spel
        // Deze functie wordt aangeroepen door Enemy
        if (!waveActive && enemyInGameCounter <= 0)
        {
            if (currentWave == 1)
            {
                // Logica voor het einde van het spel
            }
            else
            {
                topMenu.EnableWaveButton();
}
        }
    }
}