using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject controlsPanel;

    private void Start()
    {
        if(menuPanel == null)
            menuPanel =  transform.GetChild(0).gameObject;

        if (controlsPanel == null)
            controlsPanel = transform.GetChild(1).gameObject;

        menuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowControls()
    {
        menuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
    public void HideControls()
    {
        menuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
