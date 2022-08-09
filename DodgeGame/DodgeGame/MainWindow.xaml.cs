using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DodgeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int jumpDelay = 10;
        bool pauseGame= false;
        int countEnemies = 0;
        double gameAreaWidth = 0;
        double gameAreaHeight = 0;
        Random rnd = new Random();
        GameClass game;
        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer gameTimer2 = new DispatcherTimer();
        public MainWindow(bool isNewGame)
        {
            InitializeComponent();

            PlayerClass player;
            EnemyClass[] enemyArray;

            if (isNewGame)
            {
                gameAreaHeight = GameArea.Height;
                gameAreaWidth = GameArea.Width;
                Image playerImage = new Image();
                playerImage.Source = new BitmapImage(new Uri("../../Assets/PlayerImage.png", UriKind.Relative));         //new Uri(@"pack://application:,,,/Assets/PlayerImage.png")
                playerImage.Width = 50;
                playerImage.Height = 50;
                playerImage.Stretch = Stretch.Fill;
                //playerImage.HorizontalAlignment = HorizontalAlignment.Center;
                //playerImage.VerticalAlignment = VerticalAlignment.Center;
                GameArea.Children.Add(playerImage);
                Canvas.SetLeft(playerImage, gameAreaHeight/2.2);
                Canvas.SetTop(playerImage, gameAreaWidth/2.2);
                player = new PlayerClass(playerImage);
                enemyArray = new EnemyClass[5];

                for (int i = 0; i < enemyArray.Length; i++)
                {
                    Image enemyImage = new Image();
                    enemyImage.Source = new BitmapImage(new Uri("../../Assets/EnemyImage.png", UriKind.Relative));              //new Uri(@"pack://application:,,,/Assets/EnemyImage.png")
                    enemyImage.Width = 50;
                    enemyImage.Height = 50;
                    enemyImage.Stretch = Stretch.Fill;
                    GameArea.Children.Add(enemyImage);
                    enemyArray[i] = new EnemyClass(enemyImage, rnd.Next(8, 12), rnd.Next(8, 12));
                }
                countEnemies = enemyArray.Length;
                game = new GameClass(player, enemyArray, gameAreaHeight, gameAreaWidth);

                game.EnemyStart();
            }
            else
            {
                game = GameClass.loadGame();
                player = new PlayerClass(game.player.playerImage);
                enemyArray = new EnemyClass[game.enemyArray.Length];
                enemyArray = game.enemyArray;
                gameAreaHeight = game.height;
                gameAreaWidth = game.width;
                GameArea.Children.Add(player.playerImage);
                for (int i = 0; i < enemyArray.Length; i++)
                {
                    if (enemyArray[i].enemyAlive == true)
                    {
                        GameArea.Children.Add(enemyArray[i].enemyImage);
                        countEnemies++;
                    }
                }

            }
            
            GameArea.Focus();
            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //gameTimer.Start();

            //gameTimer2.Tick += GameTimer2Event;
            //gameTimer2.Interval = TimeSpan.FromMilliseconds(200);
            //gameTimer2.Start();
        }

        //private void GameTimer2Event(object sender, EventArgs e)
        //{
        //    enemy1.Movement(Enemy1, rnd.Next(1, 5), rnd.Next(8, 12), rnd.Next(3, 6), Application.Current.MainWindow.Height, Application.Current.MainWindow.Width);
        //    enemy2.Movement(Enemy2, rnd.Next(1, 5), rnd.Next(8, 12), rnd.Next(3, 6), Application.Current.MainWindow.Height, Application.Current.MainWindow.Width);
        //    enemy3.Movement(Enemy3, rnd.Next(1, 5), rnd.Next(8, 12), rnd.Next(3, 6), Application.Current.MainWindow.Height, Application.Current.MainWindow.Width);
        //    enemy4.Movement(Enemy4, rnd.Next(1, 5), rnd.Next(8, 12), rnd.Next(3, 6), Application.Current.MainWindow.Height, Application.Current.MainWindow.Width);
        //    enemy5.Movement(Enemy5, rnd.Next(1, 5), rnd.Next(8, 12), rnd.Next(3, 6), Application.Current.MainWindow.Height, Application.Current.MainWindow.Width);

        //}
        
        private void GameTimerEvent(object sender, EventArgs e)
        {
            game.player.PlayerMovement(12, gameAreaHeight, gameAreaWidth);
            for (int i = 0; i < game.enemyArray.Length; i++)
            {
                game.enemyArray[i].W2WMovement(game.enemyArray[i].enemyImage, gameAreaHeight, gameAreaWidth);
            }

            Rect[] EnemyHitBoxes = new Rect[game.enemyArray.Length];
            Rect playerHitBox = new Rect(Canvas.GetLeft(game.player.playerImage), Canvas.GetTop(game.player.playerImage), game.player.playerImage.Width, game.player.playerImage.Height);

            for (int i = 0; i < game.enemyArray.Length; i++)
            {
                EnemyHitBoxes[i] = new Rect(Canvas.GetLeft(game.enemyArray[i].enemyImage), Canvas.GetTop(game.enemyArray[i].enemyImage), game.enemyArray[i].enemyImage.Width, game.enemyArray[i].enemyImage.Height);
            }

            jumpDelay++;
            if (jumpDelay >= 5)
            {
                game.player.PlayerJump(EnemyHitBoxes, gameAreaHeight, gameAreaWidth);
                jumpDelay = 0;
            }
            
            for (int i = 0; i < 5; i++)
            {
                if (playerHitBox.IntersectsWith(EnemyHitBoxes[i]) && game.enemyArray[i].enemyAlive == true)
                {
                    gameTimer.Stop();
                    gameTimer2.Stop();
                    MessageBox.Show("You lose");
                    Environment.Exit(0);
                }
                for (int j = 0; j < 5; j++)
                {
                    if (EnemyHitBoxes[j].IntersectsWith(EnemyHitBoxes[i]) && i != j && game.enemyArray[i].enemyAlive == true && game.enemyArray[j].enemyAlive == true)
                    {
                        GameArea.Children.Remove(game.enemyArray[j].enemyImage);
                        game.enemyArray[j].enemyAlive = false;
                        countEnemies--;
                    }
                }
            }

            if (countEnemies == 1)
            {
                gameTimer.Stop();
                gameTimer2.Stop();
                MessageBox.Show("You Win");
                Environment.Exit(0);
            }

            if (pauseGame == true)
            {
                PauseMenu objPauseMenu = new PauseMenu();
                gameTimer.Stop();
                objPauseMenu.ShowDialog();
                if (objPauseMenu.DialogResult == true)
                {
                    if (objPauseMenu.choice == eChoice.choicePause)
                    {
                        gameTimer.Start();
                        pauseGame = false;
                    }

                    if (objPauseMenu.choice == eChoice.choiceSave)
                    {
                        //MessageBox.Show("Hi");
                        //gameTimer.Start();
                        //pauseGame = false;
                        game.Save();
                        pauseGame = false;
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                game.player.goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                game.player.goRight = true;
            }
            if (e.Key == Key.Up)
            {
                game.player.goUp = true;
            }
            if (e.Key == Key.Down)
            {
                game.player.goDown = true;
            }
            if (e.Key == Key.Space)
            {
                game.player.jump = true;
            }
            if (e.Key == Key.Delete)
            {
                Environment.Exit(0);
            }
            if (e.Key == Key.Escape)
            {
                pauseGame = true;
            }
            if (e.Key == Key.Enter)
            {
                gameTimer.Start();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                game.player.goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                game.player.goRight = false;
            }
            if (e.Key == Key.Up)
            {
                game.player.goUp = false;
            }
            if (e.Key == Key.Down)
            {
                game.player.goDown = false;
            }
            if (e.Key == Key.Space)
            {
                game.player.jump = false;
            }
            if (e.Key == Key.Escape)
            {
                pauseGame = false;
            }
        }
    }
}
