using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using models.inputs;
using TMPro;
public class Achievement : MonoBehaviour
{
    [SerializeField] private Button GetAchievemetnList;
    [SerializeField] private Button UnlockAchievement;
    [SerializeField] private TMP_InputField AchievementId;
    [SerializeField] private TMP_InputField StepId;
    [SerializeField] private Button BackButton;


    // Start is called before the first frame update
    void Start()
    {


        GetAchievemetnList.onClick.AddListener(GetAchievements);
        UnlockAchievement.onClick.AddListener(UnlockAchievements);



        BackButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("mainMenu");
        });
    }
    async void GetAchievements()
    {
        var Achievementlist = await DynamicPixels.Table.GetServices().Achievement.GetAchievements(new GetAchievementParams { }) ;
        
        foreach (var achiv in Achievementlist)
        {
            Debug.Log("Achievementlist: " + achiv.ToString());
           
        }
    }
    async void UnlockAchievements()
    {
        var Achievementlist = await DynamicPixels.Table.GetServices().Achievement.UnlockAchievement(new UnlockAchievementParams
        {
            AchievementId =int.Parse(AchievementId.text),
            StepId = int.Parse(StepId.text),
            
            

        }) ;
        Debug.Log("Unlock New Achievement");
    }
}
