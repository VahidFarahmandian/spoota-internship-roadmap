using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principles.Kiss
{
   public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public string CurrentGame { get; set; }
        public int Age { get; set; }
    }
    public class CheckingPlayer
    {
        public string GetPlayerStatus(Player player)
        {
            if (player.IsOnline)
            {
                if (player.CurrentGame != null)
                {
                    return "Player is currently in a game";
                }
                else
                {
                    if (player.Age >= 18)
                    {
                        return "Player  is of legal age";
                    }
                    else
                    {
                        return "Player  is  not of legal age ";
                    }
                }
            }
            else
            {
                return "Player is offline";
            }
        }

        // GOOD: Reducing nesting and more simple
        public string GetPlayerStatusBetter(Player player)
        {
            if (!player.IsOnline)
                return "Player is offline";

            if (player.CurrentGame != null)
                return "Player is currently in a game";

            return (player.Age >= 18) ? "Player  is of legal age" : "Player not is of legal age";
        }
    }
}
