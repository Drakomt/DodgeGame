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
    public class EnemyClass
    {
        public bool enemyAlive { get; set; }
        public int enemyxSpeed { get; set; }
        public int enemyySpeed { get; set; }
        public Image enemyImage { get; set; }
        public double enemyHeight;
        public double enemyWidth;
        public const int SAFE_START_RADIUS = 40;
        public const int START_CHANGE_BY = 1;
        Random rnd = new Random();
        public EnemyClass(Image enemyImage, int enemyxSpeed, int enemyySpeed, double enemyHeight, double enemyWidth)
        {
            enemyAlive = true;
            this.enemyImage = enemyImage;
            this.enemyxSpeed = enemyxSpeed;
            this.enemyySpeed = enemyySpeed;
            this.enemyHeight = enemyHeight;
            this.enemyWidth = enemyWidth;
        }

        public EnemyClass()
        {
        }

        public EnemyClass(Image enemyImage, int enemyxSpeed, int enemyySpeed)
        {
            enemyAlive = true;
            this.enemyImage = enemyImage;
            this.enemyxSpeed = enemyxSpeed;
            this.enemyySpeed = enemyySpeed;
            this.enemyHeight = enemyImage.Height;
            this.enemyWidth = enemyImage.Width;
        }

        //public void Movement(Rectangle Enemy, int enemyDirection, int enemySpeed, int numberOfTimes, double gameAreaHeight, double gameAreaWidth)
        //{
        //    for (int i = 0; i < numberOfTimes; i++)
        //    {
        //        if (enemyDirection == 1 && Canvas.GetTop(Enemy) > 4)
        //        {
        //            Canvas.SetTop(Enemy, Canvas.GetTop(Enemy) - enemySpeed);
        //        }
        //        if (enemyDirection == 2 && Canvas.GetLeft(Enemy) + (Enemy.Width + 30) < gameAreaWidth)
        //        {
        //            Canvas.SetLeft(Enemy, Canvas.GetLeft(Enemy) + enemySpeed);
        //        }
        //        if (enemyDirection == 3 && Canvas.GetTop(Enemy) + (Enemy.Height * 2) < gameAreaHeight)
        //        {
        //            Canvas.SetTop(Enemy, Canvas.GetTop(Enemy) + enemySpeed);
        //        }
        //        if (enemyDirection == 4 && Canvas.GetLeft(Enemy) > 4)
        //        {
        //            Canvas.SetLeft(Enemy, Canvas.GetLeft(Enemy) - enemySpeed);
        //        }
        //    }
        //}

        public void W2WMovement(Image Enemy, double gameAreaHeight, double gameAreaWidth)
        {
            if (Canvas.GetTop(Enemy) < 4 || Canvas.GetTop(Enemy) + (Enemy.Height * 2) > gameAreaHeight)
            {
                enemyySpeed = -enemyySpeed;
            }

            if (Canvas.GetLeft(Enemy) < 4 || Canvas.GetLeft(Enemy) + (Enemy.Width * 2) > gameAreaWidth)
            {
                enemyxSpeed = -enemyxSpeed;
            }

            Canvas.SetTop(Enemy, Canvas.GetTop(Enemy) + enemyySpeed);
            Canvas.SetLeft(Enemy, Canvas.GetLeft(Enemy) + enemyxSpeed);
        }
    }  
}
