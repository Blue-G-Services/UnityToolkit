using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField]
    public Button leaderboardButton;
    [SerializeField]
    public Button achievementButton;
    [SerializeField]
    public Button userButton;

    [SerializeField]
    public Button friendButton;

    [SerializeField]
    public Button tableButton;
    [SerializeField]
    public Button logoutButton;

    void Start()
    {
        leaderboardButton.onClick.AddListener(() => SceneManager.LoadScene("leaderboard", LoadSceneMode.Single));
        achievementButton.onClick.AddListener(() => SceneManager.LoadScene("achievement", LoadSceneMode.Single));
        userButton.onClick.AddListener(() => SceneManager.LoadScene("Users", LoadSceneMode.Single));
        friendButton.onClick.AddListener(() => SceneManager.LoadScene("Friend", LoadSceneMode.Single));
        tableButton.onClick.AddListener(() => SceneManager.LoadScene("Table", LoadSceneMode.Single));
        logoutButton.onClick.AddListener(() =>
        {
            DynamicPixels.Authentication.Logout();
            SceneManager.LoadScene("Auth", LoadSceneMode.Single);
        });

    }
}
