using System.Collections;
using System.Collections.Generic;
using System;
using models.inputs;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Friend : MonoBehaviour
{
    
    public TMP_InputField IdInput;
    public Button BackButton, FriendsButton, RequestsButton, RequestButton, AcceptButton, RejectButton, RejectAllButton, DeleteButton;
    private void Start()
    {
        BackButton.onClick.AddListener(BackToMainMenu);
        FriendsButton.onClick.AddListener(GetMyFriends);
        RequestsButton.onClick.AddListener(GetMyFriendshipRequests);
        RequestButton.onClick.AddListener(RequestFriendship);
        AcceptButton.onClick.AddListener(AcceptFriendRequest);
        RejectButton.onClick.AddListener(RejectFriendRequest);
        RejectAllButton.onClick.AddListener(RejectAllFriendRequests);
        DeleteButton.onClick.AddListener(DeleteFriend);

    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    private async void GetMyFriends()
    {
        var friends = await DynamicPixels.Table.GetServices().Friendship.GetMyFriends(new GetMyFriendsParams { });
        foreach (var f in friends)
        {
            Debug.Log(f.ToString());
            
        }
    }

    private async void GetMyFriendshipRequests()
    {
        var requests = await DynamicPixels.Table.GetServices().Friendship.GetMyFriendshipRequests(new GetMyFriendshipRequestsParams { });
        foreach (var r in requests)
        {
            Debug.Log(r.ToString());
        }
    }

    private async void RequestFriendship()
    {
        var id = IdInput.text;
        var request = await DynamicPixels.Table.GetServices().Friendship.RequestFriendship(
            new RequestFriendshipParams
            {

                UserId = Convert.ToInt32(id)
            });
    }

    private async void AcceptFriendRequest()
    {
        var id = IdInput.text;
        var request = await DynamicPixels.Table.GetServices().Friendship.AcceptRequest(
            new AcceptRequestParams()
            {
                RequestId = Convert.ToInt32(id)
            });
    }

    private async void RejectFriendRequest()
    {
        var id = IdInput.text;
        var request = await DynamicPixels.Table.GetServices().Friendship.RejectRequest(
            new RejectRequestParams()
            {
                RequestId = Convert.ToInt32(id)
            });
    }

    private async void RejectAllFriendRequests()
    {
        var request = await DynamicPixels.Table.GetServices().Friendship.RejectAllRequests(
            new RejectAllRequestsParams()
            {
            });
    }

    private async void DeleteFriend()
    {
        var id = IdInput.text;
        var request = await DynamicPixels.Table.GetServices().Friendship.DeleteFriend(
            new DeleteFriendParams()
            {
                UserId = Convert.ToInt32(id)
            });
    }
}


