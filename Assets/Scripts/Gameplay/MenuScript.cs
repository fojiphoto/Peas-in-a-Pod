using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonobehaviourSingleton<MenuScript>
{
    [field: SerializeField] public GameObject m_MenuPanel;
    [field: SerializeField] public GameObject m_CompletePanel;
    [field: SerializeField] public Button m_PlayBtn;

    private void Start()
    {
        m_PlayBtn.onClick.AddListener(SwitchScene);
    }
    void SwitchScene()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
