using UnityEngine;
using UnityEngine.UIElements;
using static Enums;

public class TowerMenu : MonoBehaviour
{
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;
    private ConstructionSite selectedSite;
    private VisualElement root;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        archerButton = root.Q<Button>("archer-tower");
        swordButton = root.Q<Button>("sword-tower");
        wizardButton = root.Q<Button>("wizard-tower");
        updateButton = root.Q<Button>("upgrade");
        destroyButton = root.Q<Button>("destroy");

        if (archerButton != null)
        {
            archerButton.clicked += OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked += OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked += OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked += OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked += OnDestroyButtonClicked;
        }

        root.visible = false;
    }

    public void EvaluateMenu()
    {
        if (selectedSite == null)
        {
            // If selectedSite is null, return without enabling any buttons
            return;
        }

        // Access the site level property of selectedSite
        int siteLevel = (int)selectedSite.Level;

        // Disable all buttons initially
        archerButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);

        // Enable buttons based on site level using a switch statement
        switch (siteLevel)
        {
            case 0:
                // For site level 0, enable archer, wizard, and sword buttons
                archerButton.SetEnabled(true);
                wizardButton.SetEnabled(true);
                swordButton.SetEnabled(true);
                break;
            case 1:
            case 2:
                // For site levels 1 and 2, enable update and destroy buttons
                updateButton.SetEnabled(true);
                destroyButton.SetEnabled(true);
                break;
            case 3:
                // For site level 3, only enable the destroy button
                destroyButton.SetEnabled(true);
                break;
            default:
                // Handle any other site levels if necessary
                break;
        }
    }

    public void SetSite(ConstructionSite site)
    {
        // Assign the site to the selectedSite variable
        selectedSite = site;

        if (selectedSite == null)
        {
            // If the selected site is null, hide the menu and return
            root.visible = false;
            return;
        }
        else
        {
            // If the selected site is not null, make sure the menu is visible
            root.visible = true;

            // Call the EvaluateMenu method to update button visibility
            EvaluateMenu();
        }
    }

    private void OnArcherButtonClicked()
    {
        GameManager.instance.Build(TowerType.Archer, SiteLevel.Unbuilt);
    }

    private void OnSwordButtonClicked()
    {
        GameManager.instance.Build(TowerType.Sword, SiteLevel.Unbuilt);
    }

    private void OnWizardButtonClicked()
    {
        GameManager.instance.Build(TowerType.Wizard, SiteLevel.Unbuilt);
    }

    private void OnUpdateButtonClicked()
    {
        if (selectedSite != null && selectedSite.Level < SiteLevel.Level3)
        {
            selectedSite.Level++;
            EvaluateMenu();
        }
    }

    private void OnDestroyButtonClicked()
    {
        if (selectedSite != null)
        {
            TowerType towerType = selectedSite.GetTowerType();
            // Destroy the tower on the selected site
            selectedSite.SetTower(null, SiteLevel.Unbuilt, towerType);
            // Set the level of the site to 0
            selectedSite.Level = SiteLevel.Unbuilt;
            // Update the buttons in the tower menu
            EvaluateMenu();
        }
    }

    private void OnDestroy()
    {
        if (archerButton != null)
        {
            archerButton.clicked -= OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked -= OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked -= OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked -= OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked -= OnDestroyButtonClicked;
        }
    }
}
