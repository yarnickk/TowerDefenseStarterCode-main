using UnityEngine;
using UnityEngine.UI;

public class TopMenu : MonoBehaviour
{
    public Text creditsLabel;
    public Text healthLabel;
    public Text waveLabel;
    public Button waveButton; // Nieuwe variabele toegevoegd

    void Start()
    {
        
    }

    void OnDestroy()
    {
        // Verwijder hier eventuele callbacks
    }

    public void StartWave()
    {
        GameManager.instance.StartWave();
        waveButton.interactable = false; // Nieuwe code toegevoegd om de waveButton uit te schakelen bij het starten van een nieuwe wave
    }

    public void EnableWaveButton()
    {
        waveButton.interactable = true;
    }

    public void SetCreditsLabel(string text)
    {
        creditsLabel.text = text;
    }

    public void SetHealthLabel(string text)
    {
        healthLabel.text = text;
    }

    public void SetWaveLabel(string text)
    {
        waveLabel.text = text;
    }

    
    

    // Voeg meer functies toe indien nodig
}
