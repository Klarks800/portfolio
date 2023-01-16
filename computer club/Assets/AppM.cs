using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppM : MonoBehaviour
{
    private static Dictionary<string, object> Data = new Dictionary<string, object>();

    private static int startTime;

    public static AppM app;

    private void Awake()
    {
        app = this;
    }
    public  void SendLevelStart()
    {
        Data.Clear();

        Debug.Log("Send LevelStart Event");

        startTime = (int)Time.time;

        Data["level_number"] = 0;
        Data["level_name"] = "default";
        Data["level_count"] = 0;
        Data["level_diff"] = "easy";
        Data["level_loop"] = 1;
        Data["level_random"] = 0;
        Data["level_type"] = "normal";
        Data["game_mode"] = "default";

        AppMetrica.Instance.ReportEvent("level_start", Data);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public  void SendLevelFinish()
    {
        Data.Clear();

        Debug.Log("Send LevelFinish Event");

        int finishTime = (int)Time.time - startTime;

        Data["level_number"] = 0;
        Data["level_name"] = "default";
        Data["level_count"] = 0;
        Data["level_diff"] = "easy";
        Data["level_loop"] = 1;
        Data["level_random"] = 0;
        Data["level_type"] = "normal";
        Data["game_mode"] = "default";
        Data["result"] = "leave";
        Data["time"] = finishTime;
        Data["progress"] = (int)Mathf.Round(GameManager.manager.max * 7.69f);
        Data["continue"] = 0;

        AppMetrica.Instance.ReportEvent("level_finish", Data);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public  void SendTutorial(int i)
    {
        Data.Clear();

        Debug.Log("Send Tutorial Event");

       

        Data["step_name"] = "0"+i+"_tutorial";


        AppMetrica.Instance.ReportEvent("tutorial", Data);
        AppMetrica.Instance.SendEventsBuffer();
    }
}
