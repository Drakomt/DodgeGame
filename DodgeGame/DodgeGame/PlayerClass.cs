using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;

namespace DodgeGame
{
    public class PlayerClass
    {
        public Image playerImage { get; set; }
        public bool goLeft=false;
        public bool goRight = false;
        public bool goUp = false;
        public bool goDown = false;
        public bool jump = false;
        public double jumpLocationX;
        public double jumpLocationY;
        public const int JUMP_SAFE_RADIUS = 20;
        Random rnd = new Random();

        public PlayerClass(Image playerImage)
        {
            this.playerImage = playerImage;
        }
        public PlayerClass()
        {
        }
        public void PlayerMovement(int playerSpeed, double gameAreaHeight, double gameAreaWidth)
        {
            double x = Canvas.GetLeft(playerImage);
            double y = Canvas.GetTop(playerImage);

            if (goLeft == true && x > playerSpeed)
            {
                Canvas.SetLeft(playerImage, x - playerSpeed);
            }
            if (goRight == true && x + playerImage.Width*1.5 < gameAreaWidth)
            {
                Canvas.SetLeft(playerImage, x + playerSpeed);
            }
            if (goUp == true && y > playerSpeed)
            {
                Canvas.SetTop(playerImage, y - playerSpeed);
            }
            if (goDown == true && y + (playerImage.Height * 1.9) < gameAreaHeight)
            {
                Canvas.SetTop(playerImage, y + playerSpeed);
            }
        }

        public void PlayerJump(Rect[] EnemyHitBoxes, double gameAreaHeight, double gameAreaWidth)
        {
            if (jump == true)
            {
                jumpLocationX = rnd.Next(0, (int)(gameAreaWidth - playerImage.Width*1.4));
                jumpLocationY = rnd.Next(0, (int)(gameAreaHeight - (playerImage.Height*1.8)));
                Rect playerRect = new Rect(jumpLocationX - JUMP_SAFE_RADIUS, jumpLocationY - JUMP_SAFE_RADIUS, playerImage.Width + (JUMP_SAFE_RADIUS * 2), playerImage.Height + (JUMP_SAFE_RADIUS * 2));
                bool validLocation = true;
                for (int i = 0; i < EnemyHitBoxes.Length; i++)
                {
                    if (playerRect.IntersectsWith(EnemyHitBoxes[i]))
                    {
                        validLocation = false;
                        break;
                    }
                }
                if (validLocation == true)
                {
                      Canvas.SetLeft(playerImage, jumpLocationX);
                      Canvas.SetTop(playerImage, jumpLocationY);
                }
                else
                {
                    PlayerJump(EnemyHitBoxes, gameAreaHeight, gameAreaWidth);
                }
            }
        }
    }
}
