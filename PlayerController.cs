using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PlayerController : ApiController
    {
        List<Player> players = new List<Player>();

        public List<Player> loadFile()
        {
            List<Player> players = new List<Player>();
            string[] text = System.IO.File.ReadAllLines(@"H:\INFS3204\players.txt");
            

            for (int i = 0; i < text.Length; i++)
            {
                Player player = new Player();
                string[] playerElement = text[i].Split(',');
                player.registeration_ID = playerElement[0];
                player.Player_name = playerElement[1];
                player.Team_name = playerElement[2];
                player.Date_of_birth = playerElement[3];

                players.Add(player);
            }
            return players;
        }


        public IHttpActionResult GetAllPlayers()
        {
            string[] text = System.IO.File.ReadAllLines(@"H:\INFS3204\players.txt");
            



            for (int i = 0; i < text.Length; i++)
            {
                //if (textArray.Length >= 4) { 
                    Player player = new Player();
                    string[] playerElement = text[i].Split(',');
                    player.registeration_ID = playerElement[0];
                    player.Player_name = playerElement[1];
                    player.Team_name = playerElement[2];
                    player.Date_of_birth = playerElement[3];

                    players.Add(player);
                //}
            }
            
            
            return Ok(players);
        }


        public IHttpActionResult GetPlayerInfo(string field, string input)
        {
            List<Player> result = new List<Player>();
            List<Player> players = loadFile();
            if (field.Equals("PlayerID"))
            {
                foreach (Player player in players)
                {
                    if (player.registeration_ID.Equals(input))
                    {
                        result.Add(player);
                    }
                }
            }
            if (field.Equals("PlayerFullName"))
            {
                foreach (Player player in players)
                {
                    if (player.Player_name.ToLower().Contains(input.ToLower()))
                    {
                        result.Add(player);
                    }
                }
            }
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }



        public IHttpActionResult DeletePlayer(string field, string input)
        {
            bool findFlag = false;
            List<Player> result = new List<Player>();
            List<Player> players = loadFile();
            System.IO.File.WriteAllText(@"H:\INFS3204\players.txt", string.Empty);
            if (field.Equals("PlayerID"))
            {
                foreach (Player player in players)
                {
                    if (player.registeration_ID.Equals(input))
                    {
                        
                        findFlag = true;
                    }
                    if (!(player.registeration_ID.Equals(input)))
                    {
                        result.Add(player);
                        string playerDesc = player.registeration_ID + "," + player.Player_name + "," + player.Team_name + "," + player.Date_of_birth;
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"H:\INFS3204\players.txt", true))
                        {
                            file.WriteLine(playerDesc);
                        }
                    }
                }
            }
            if (field.Equals("PlayerFullName"))
            {
                foreach (Player player in players)
                {
                    if (player.Player_name.ToLower().Contains(input.ToLower()))
                    {
                        
                        findFlag = true;
                    }
                    if (!(player.Player_name.ToLower().Contains(input.ToLower())))
                    {
                        result.Add(player);
                        string playerDesc = player.registeration_ID + "," + player.Player_name + "," + player.Team_name + "," + player.Date_of_birth;
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"H:\INFS3204\players.txt", true))
                        {
                            file.WriteLine(playerDesc);
                        }
                    }
                }
            }
            if (findFlag == false)
            {
                return NotFound();
            }
            return Ok(result);
        }



        public IHttpActionResult PostPlayer(string ID, string name, string team, string date)
        {
            bool existFlag = false;
            List<Player> players = loadFile();
            List<Player> final = new List<Player>();
            List<Player> result = new List<Player>();
            Player player = new Player();
            player.registeration_ID = ID;
            player.Player_name = name;
            player.Team_name = team;
            player.Date_of_birth = date;

           

                foreach (Player p in players)
            {
                if (p.registeration_ID.Equals(ID))
                {
                    string[] text = System.IO.File.ReadAllLines(@"H:\INFS3204\players.txt");
                    for (int i = 0; i < text.Length; i++)
                    {
                        string[] playerElement = text[i].Split(',');
                        if (playerElement[0].Equals(ID))
                        {
                            text[i] = "\0";
                        }
                    }

                } else
                {
                    final.Add(p);
                }
            }
            if (existFlag == false)
            {
                string playerDesc = player.registeration_ID + "," + player.Player_name + "," + player.Team_name + "," + date;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"H:\INFS3204\players.txt", true))
                {
                    file.WriteLine(playerDesc);
                }
                result.Add(player);
            }
            List<Player> players2 = loadFile();
            return Ok(players2);

        }
    }
}
