using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public HomePanel homePanel;
    public GamePlayPanel gamePlayPanel;
    public PlayerProfilePanel playerProfilePanel;
    public LeaderBoardPanel leaderBoardPanel;

    public Mode mode;
}
