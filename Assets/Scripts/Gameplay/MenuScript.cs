using UnityEngine.UI;
using UnityEngine;

public class MenuScript : MonobehaviourSingleton<MenuScript>
{
    public PuzzleController m_PuzzleController;
    [field: SerializeField] public GameObject m_MenuPanel;
    [field: SerializeField] public GameObject m_CompletePanel;
    [field: SerializeField] public Button m_PlayBtn;

}
