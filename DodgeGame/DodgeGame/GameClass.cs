using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Markup;
using Microsoft.Win32;
using System.Xml;

namespace DodgeGame
{
    public class GameClass
    {
        public PlayerClass player { get; set; }
        public EnemyClass[] enemyArray { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public GameClass(PlayerClass player, EnemyClass[] enemyArray, double height, double width)
        {
            this.player = player;
            this.enemyArray = enemyArray;
            this.height = height;
            this.width = width;
        }
        //public Image Image_Player { get { return player.playerImage; } }         <--- Properties
        public GameClass()
        {
        }
        public void EnemyStart()
        {
            bool validLocation = true;
            Random rnd = new Random();
            Rect playerHitBox = new Rect(Canvas.GetLeft(player.playerImage), Canvas.GetTop(player.playerImage), player.playerImage.Width, player.playerImage.Height);
            Rect[] enemyHitBoxes = new Rect[enemyArray.Length];
            for (int i = 0; i < enemyHitBoxes.Length; i++)
            {
                Rect elementRec = new Rect(Canvas.GetLeft(enemyArray[i].enemyImage), Canvas.GetTop(enemyArray[i].enemyImage), enemyArray[i].enemyImage.Width, enemyArray[i].enemyImage.Height);
                enemyHitBoxes[i] = elementRec;
            }
            for (int i = 0; i < enemyHitBoxes.Length; i++)
            {
                enemyHitBoxes[i].X = rnd.Next(0, (int)(width - enemyHitBoxes[i].Width * 1.9));
                enemyHitBoxes[i].Y = rnd.Next(0, (int)(height - enemyHitBoxes[i].Height * 1.9));
                for (int j = i - 1; j >= 0; j--)
                {
                    validLocation = false;
                    while (validLocation == false)
                    {
                        if (enemyHitBoxes[i].IntersectsWith(enemyHitBoxes[j]))
                        {
                            enemyHitBoxes[i].X = rnd.Next(0, (int)(width - enemyHitBoxes[i].Width * 1.9));
                            enemyHitBoxes[i].Y = rnd.Next(0, (int)(height - enemyHitBoxes[i].Height * 1.9));
                        }
                        else
                        {
                            validLocation = true;
                        }
                    }
                }
                validLocation = false;
                while (validLocation == false)
                {
                    if (playerHitBox.IntersectsWith(enemyHitBoxes[i]))
                    {
                        enemyHitBoxes[i].X = rnd.Next(0, (int)(width - enemyHitBoxes[i].Width * 1.9));
                        enemyHitBoxes[i].Y = rnd.Next(0, (int)(height - enemyHitBoxes[i].Height * 1.9));
                    }
                    else
                    {
                        validLocation = true;
                    }
                }
            }
            for (int i = 0; i < enemyHitBoxes.Length; i++)
            {
                Canvas.SetLeft(enemyArray[i].enemyImage, enemyHitBoxes[i].X);
                Canvas.SetTop(enemyArray[i].enemyImage, enemyHitBoxes[i].Y);
            }

        }

        public void Save()
        {
            string savedGame = XamlWriter.Save(this);
            File.WriteAllText("../../GameSaves/DodgeGameSave.xml", savedGame);

            //Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.FileName = "GameSave";
            //dlg.DefaultExt = ".txt"; 
            //dlg.Filter = "Text documents (.txt)|*.txt";
            //Nullable<bool> result = dlg.ShowDialog();

            //if (result == true)
            //{
            //    string filename = dlg.FileName;
            //}
        }
        public static GameClass loadGame()
        {
            string gameXmlString = File.ReadAllText("../../GameSaves/DodgeGameSave.xml");
            
            StringReader stringReader = new StringReader(gameXmlString);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            return (GameClass)XamlReader.Load(xmlReader);
        }
    }
}
