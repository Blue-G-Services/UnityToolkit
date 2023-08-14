using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using models.inputs;
using UnityEngine.SceneManagement;
public class Leaderboard : MonoBehaviour
{
    [SerializeField] Button BackButton;
    [SerializeField] Button SubmitButton;
    [SerializeField] Button GetScoresButton;
    [SerializeField] Button GetMyScoreButton;
    [SerializeField] Button GetMyFriendsScoresButton;
    [SerializeField] TMP_InputField MyScoreInputText;
    [SerializeField] TMP_Text MyScoreText;
    [SerializeField] GameObject userinfo;
    [SerializeField] Transform tablecontent;
    string leaderboardid = "1";




    async void Start()
    {
        GetScoresButton.onClick.AddListener(GetScores);

         SubmitButton.onClick.AddListener(SubmitMyScore);

        BackButton.onClick.AddListener(async () =>
        {
            SceneManager.LoadScene("mainMenu");
        });

        GetMyScoreButton.onClick.AddListener(GetMyScore);

       /* GetMyFriendsScoresButton.onClick.AddListener(async () =>
        {
            var leaderboardId = leaderboardid;
            var result = await DynamicPixels.Table.GetServices().Leaderboard.GetFriendsScores(new GetFriendsScoresParams()
            {
                LeaderboardId = int.Parse(leaderboardId)
            });
            foreach (var s in result)
            {
                Debug.Log(s.ToString());
            }
        });

        var leaderboards = await DynamicPixels.Table.GetServices().Leaderboard.GetLeaderboards(new GetLeaderboardsParams());

        foreach (var l in leaderboards)
        {
            Debug.Log(l.ToString());
        }*/
    }

    async void GetMyScore()
    {
        var leaderboardId = leaderboardid;
        var result = await DynamicPixels.Table.GetServices().Leaderboard.GetMyScore(new GetCurrentUserScoreParams
        {
            LeaderboardId = int.Parse(leaderboardId)
        });

        Debug.Log(result.ToString());
        UserData userData = JsonUtility.FromJson<UserData>(result.ToString());
        MyScoreText.text ="MyScore: "+userData.value.ToString();


    }

    async void GetScores()
    {
            foreach(Transform objs in tablecontent)
            {
            Destroy(objs.gameObject);
            }
            var leaderboardId = leaderboardid;
            var scores = await DynamicPixels.Table.GetServices().Leaderboard.GetUsersScores(new GetScoresParams
            {
                LeaderboardId = int.Parse(leaderboardId),
                
            });
        Debug.Log(scores[0]);
        for (int i = 0; i < scores.Count && i < 10; i++)
        {
            var s = scores[i];
            Debug.Log(s.ToString());
            UserData userData = JsonUtility.FromJson<UserData>(s.ToString());

            Debug.Log("Name: " + userData.name);
            Debug.Log("Value: " + userData.value);
            var myuserclone = Instantiate(userinfo, tablecontent);
            //myuserclone.transform.GetChild(0).GetComponent<TextMeshPro>().text = userData.name;
            GameObject namegm = myuserclone.transform.GetChild(0).gameObject;
            myuserclone.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = userData.value.ToString();
            namegm.GetComponent<TextMeshProUGUI>().text = userData.name;
        }
   

    }
    async void SubmitMyScore()
    {
        var leaderboardId = leaderboardid;
        var value = MyScoreInputText.text;

      var subscore=  await DynamicPixels.Table.GetServices().Leaderboard.SubmitScore(new SubmitScoreParams
        {
            LeaderboardId = int.Parse(leaderboardId),
            Score = Convert.ToInt32(value)
        });
        Debug.Log(subscore.ToString());
    }

}
