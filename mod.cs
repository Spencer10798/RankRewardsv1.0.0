/**
* <author>Spencer Maghrouri</author>
* <url>spencer@maghrouri.net</url>
* <credits>LifX Example Server Mod</credits>
* <description>This mod gives players gifts weekly depending on their rank</description>
* <license>GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007</license>
*/
if (!isObject(LiFxRankRewards))
{
    new ScriptObject(LiFxRankRewards)
    {
    };
}

package LiFxRankRewards
{

  function LiFxRankRewards::version() {
    return "v1.0.0.RankRewards";
  }
  
  function LiFxRankRewards::setup() {
    LiFxRankRewards::tenMinuteTick();
  }

  function LiFxRankRewards::tenMinuteTick() {
    dbi.select(LiFxRankRewards, "isRewardTime", "SELECT TIMESTAMPDIFF(MINUTE, tmi.last_reward, CURRENT_TIMESTAMP) as TimeSinceLastRewardCycle FROM `nyu_ttmod_info`");
    %this.Tick = LiFxRankRewards.schedule(600000, "tenMinuteTick");
  }

  function LiFxRankRewards::isRewardTime(%resultSet) {
    if(%resultSet.ok()) {
        if(%resultSet.nextRecord()) {
            %TimeSinceLastRewardCycle = %resultSet.getFieldValue("TimeSinceLastRewardCycle");
            if(%TimeSinceLastRewardCycle > 10080) {
                dbi.select(LiFxRankRewards, "issueRewards", "SELECT c.ID as ClientID, c.GuildID as GuildID FROM `lifx_character` lc JOIN `character` c ON c.ID = lc.ID JOIN `nyu_ttmod_info` info WHERE (lc.loggedIn > info.boot_time) AND ((lc.loggedOut < lc.loggedIn) OR (lc.loggedOut IS NULL)) AND ((TIMESTAMPDIFF(MINUTE, received_reward, CURRENT_TIMESTAMP) > 10000) OR (received_reward = 0))");
            }
        }
    }
    dbi.remove(%resultSet);
    %resultSet.delete();
  }

  function LiFxRankRewards::issueRewards() {
    if(%resultSet.ok()) {
        while(%resultSet.nextRecord()) {
            %ClientID = %resultSet.getFieldValue("ClientID");
            %GuildID = %resultSet.getFieldValue("GuildID");

            if(%GuildId == 1) {
                //Give items based on GuildID here 
            }
        }
    }
    dbi.remove(%resultSet);
    %resultSet.delete();
  }
};

activatePackage(LiFxRankRewards);
LiFx::registerCallback($LiFx::hooks::mods, setup, LiFxRankRewards);