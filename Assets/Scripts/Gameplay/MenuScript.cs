using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [field: SerializeField] public GameObject m_MenuPanel;
    [field: SerializeField] public GameObject m_CompletePanel;
    [field: SerializeField] public GameObject m_FailPanel;
    [field: SerializeField] public Button m_PlayBtn;
    [field: SerializeField] public Button m_NextBtn;
    [field: SerializeField] public Button m_Restart;



    private void Start()
    {
        m_PlayBtn.onClick.AddListener(SwitchScene);
        m_NextBtn.onClick.AddListener(Next);
        m_Restart.onClick.AddListener(RestartLevel);
    }
    void SwitchScene()
    {
        m_MenuPanel.SetActive(false);
        SceneManager.LoadScene("GameplayScene");
    }

    public void Reset()
    {
        m_FailPanel.SetActive(false);
        m_CompletePanel.SetActive(false);
    }
    public void Next()
    {
        Reset();
        GameEvents.GameplayEvents.ResetGame.Raise();
        SceneManager.LoadScene("GameplayScene");
    }
    public void RestartLevel()
    {
        Reset();
        GameEvents.GameplayEvents.ResetGame.Raise();
        SceneManager.LoadScene("GameplayScene");
    }
}
