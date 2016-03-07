using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 					
    public GameObject chooseMapPanel;
    public GameObject winPanel;
    public GameObject commandePanel;
    public GameObject creditPanel;
    public GameObject creditText1;
    public GameObject creditText2;
    public GameObject creditText3;


	public void LoadScene(string name)
	{
        chooseMapPanel.SetActive(false);
		SceneManager.LoadScene(name);	
	}

    public void LoadActualScene()
    {
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        GameObject.Find("GameController").GetComponent<GameManager>().resetGameController();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackMainMenu()
    {
        GameObject.Find("GameController").GetComponent<GameManager>().resetGameController();
        Destroy(this.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
    public void ShowCommande()
    {
        commandePanel.SetActive(true);
    }
    public void HideCommande()
    {
        commandePanel.SetActive(false);
    }
    public void ShowCredit()
    {
        creditPanel.SetActive(true);
    }

    public void SuiteCredit()
    {
        if (creditText1.activeSelf)
        {
            creditText1.SetActive(false);
            creditText2.SetActive(true);
        }
        else if (creditText2.activeSelf)
        {
            creditText2.SetActive(false);
            creditText3.SetActive(true);
        }
        else
        {
            creditText3.SetActive(false);
            creditText1.SetActive(true);
            HideCredit();
            ShowMenu();
        }
    }
    public void HideCredit()
    {
        creditPanel.SetActive(false);
    }

    public void ShowMenuChooseMap()
    {
        chooseMapPanel.SetActive(true);
    }

    public void HideShowMenuChooseMap()
    {
        chooseMapPanel.SetActive(false);
    }
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        Destroy(GameObject.Find("Ingame HUD"));
    }
    public void HideWinPanel()
    {
        winPanel.SetActive(false);

    }

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}
}
