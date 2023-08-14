using System;
using System.Collections;
using System.Collections.Generic;
using models.inputs;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Users : MonoBehaviour
{
    [SerializeField] Button GetCurrentUserButtton;
    [SerializeField] Button GetUserButtton;
    [SerializeField] Button BackButton;
    [SerializeField] TMP_InputField UserIdInput;
    [SerializeField] TextMeshProUGUI MyCurrentUserText;
    [SerializeField] TextMeshProUGUI OtherUser;

    // Start is called before the first frame update
    void Awake()
    {
        GetCurrentUserButtton.onClick.AddListener(GetCurrentUser);

        GetUserButtton.onClick.AddListener(GetUser);

        BackButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("mainMenu");
        });
    }
    async void GetCurrentUser(){
        var user = await DynamicPixels.Table.GetServices().Users.GetCurrentUser();
        MyCurrentUserText.text ="My Current User: "+ $"ID: {user.Id}\nName:{user.Name}\nEmail:{user.Email}\n";
    }
    async void GetUser()
    {
        var userId = UserIdInput.text;
        var user = await DynamicPixels.Table.GetServices().Users.FindUserById(new FindUserByIdParams
        {
            UserId = Int32.Parse(userId)
        });
        OtherUser.text = "User: " + $"ID: {user.Id}\nName:{user.Name}\nEmail:{user.Email}\n";
    }
}
