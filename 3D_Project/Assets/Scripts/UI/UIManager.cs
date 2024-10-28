using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 싱글톤

    private static UIManager _instance;
    public static UIManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
    [SerializeField] private TextMeshProUGUI promptText;

    public void SetPromptText(string text)
    {
        if (promptText != null)
        {
            promptText.text = text;
            promptText.gameObject.SetActive(true);
        }
    }

    public void HidePromptText()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }
}
