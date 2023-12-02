using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using File = System.IO.File;
using Timer = System.Timers.Timer;

namespace printTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Settings settings = new();

        List<string> RussianWords = new();
        List<string> EnglistWords = new();
        List<Label> characters = new();

        System.Timers.Timer gameTimer = new System.Timers.Timer();

        Game game = null;

        double xKoef = 0;
        double yKoef = 0;

        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Enabled = false;
            gameTimer.Elapsed += GameTimer_Elapsed;

            Task.Run(()=>GetWords());
        }

       

        private void GameTimer_Elapsed(object? sender, ElapsedEventArgs e)=>EndGame();

        void CreateStartingUi()
        {
            box.TextChanged += box_TextChanged;
            box.KeyUp += box_KeyDown;
        }

        void EndGame()
        {
            gameTimer.Enabled = false;
            gameTimer.Stop();

            double acc = game.correct;
            acc /= (game.correct + game.incorrect);
            acc *= 100;

            double speed = game.correct;
            speed /= game.lengthOfGame;
            speed *= 60000;

            GraphPoint.Max = 0;

            Dispatcher.Invoke(() =>
            {
                resultSpeed.Content = speed;
                resultAccuracy.Content = Math.Round(acc,2)+"%";
                characterTrack.Children.Clear();
                scroll.ScrollToHome();
            });

            characters.Clear();
        }
        void StartGame()
        {
            game.StartGame();

            box.Focus();
            waitLabel.Visibility = Visibility.Collapsed;

            gameTimer.Enabled = true;
            gameTimer.Start();
        }
        public void CreateCanvas(List<GraphPoint> graphPoints)
        {
            Dispatcher.Invoke(() =>
            {
                canvas.Children.Clear();

                double xKoef = canvas.ActualWidth / game.lengthOfGame * 1000;
                double yKoef = canvas.ActualHeight / (1 + GraphPoint.Max);

                double maxSpeed = GraphPoint.Max;
                maxSpeed *= 60;

                MaxSpeed1.Content=Math.Round(maxSpeed,2);
                MaxSpeed2.Content=Math.Round(maxSpeed,2);   

                double Height = canvas.ActualHeight;
                for (int i = 0; i < graphPoints.Count - 1; i++)
                {
                    var line = new Line();

                    line.X1 = xKoef * i;
                    line.Y1 = Height - (yKoef * (graphPoints[i].Value));

                    line.X2 = xKoef * (i + 1);
                    line.Y2 = Height - (yKoef * (graphPoints[i + 1].Value));

                    line.StrokeThickness = 1;
                    line.Stroke = new SolidColorBrush(Colors.Blue);

                    canvas.Children.Add(line);
                }
            });
        }
        void GetWords()
        {
            var rw = Properties.Resources.Words;
            var ew = Properties.Resources.EnglishWords;
            RussianWords.AddRange(rw.Split('\n'));
            EnglistWords.AddRange(ew.Split('\n'));
            Dispatcher.Invoke(() => CreateStartingUi());
        }
        public void UpdateTimer(int minInt,int secInt, int millisecondsInt)
        {
            Dispatcher.Invoke(() =>
            {
                minutesLabel.Content = minInt.ToString();
                secondsLabel.Content = secInt.ToString();
                millisecondsLabel.Content = millisecondsInt.ToString();
            });
        }
        private void box_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb is null)
                return;

            char currentChar = tb.Text[^1];
            game.InputChar(currentChar);
        }
        private void box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                e.Handled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            Label button = new Label();
            switch (key)
            {
                case Key.Space:
                    button = btnSpace;
                    break;
                case Key.D0:
                    button = btn0;
                    break;
                case Key.D1:
                    button = btn1;
                    break;
                case Key.D2:
                    button = btn2;
                    break;
                case Key.D3:
                    button = btn3;
                    break;
                case Key.D4:
                    button = btn4;
                    break;
                case Key.D5:
                    button = btn5;
                    break;
                case Key.D6:
                    button = btn6;
                    break;
                case Key.D7:
                    button = btn7;
                    break;
                case Key.D8:
                    button = btn8;
                    break;
                case Key.D9:
                    button = btn9;
                    break;
                case Key.A:
                    button = btnA;
                    break;
                case Key.B:
                    button = btnB;
                    break;
                case Key.C:
                    button = btnC;
                    break;
                case Key.D:
                    button = btnD;
                    break;
                case Key.E:
                    button = btnE;
                    break;
                case Key.F:
                    button = btnF;
                    break;
                case Key.G:
                    button = btnG;
                    break;
                case Key.H:
                    button = btnH;
                    break;
                case Key.I:
                    button = btnI;
                    break;
                case Key.J:
                    button = btnJ;
                    break;
                case Key.K:
                    button = btnK;
                    break;
                case Key.L:
                    button = btnL;
                    break;
                case Key.M:
                    button = btnM;
                    break;
                case Key.N:
                    button = btnN;
                    break;
                case Key.O:
                    button = btnO;
                    break;
                case Key.P:
                    button = btnP;
                    break;
                case Key.Q:
                    button = btnQ;
                    break;
                case Key.R:
                    button = btnR;
                    break;
                case Key.S:
                    button = btnS;
                    break;
                case Key.T:
                    button = btnT;
                    break;
                case Key.U:
                    button = btnU;
                    break;
                case Key.V:
                    button = btnV;
                    break;
                case Key.W:
                    button = btnW;
                    break;
                case Key.X:
                    button = btnX;
                    break;
                case Key.Y:
                    button = btnY;
                    break;
                case Key.Z:
                    button = btnZ;
                    break;
                case Key.Oem1:
                    button = btn_2;
                    break;
                case Key.OemPlus:
                    button = btnEqual;
                    break;
                case Key.OemComma:
                    button = btn_4;
                    break;
                case Key.OemMinus:
                    button = btnTire;
                    break;
                case Key.OemPeriod:
                    button = btn_5;
                    break;
                case Key.Oem2:
                    button = btnSlash;
                    break;
                case Key.Oem5:
                    button = btnBackSlash;
                    break;
                case Key.Oem6:
                    button = btn_1;
                    break;
                case Key.Oem7:
                    button = btn_3;
                    break;
            }
            if (button!=null)
            {
                var back = button.Background;
                button.Background = new SolidColorBrush(Colors.LightBlue);
                Task.Run(() => BtnBack(button,back));
            }
        }
        void BtnBack(Label button, Brush? brush)
        {
            Thread.Sleep(200);
            Dispatcher.Invoke(() => button.Background = brush);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int backScore = 2;
            waitLabel.Content = 3;
            waitLabel.Visibility = Visibility.Visible;

            gameTimer.Interval = settings.LengthOfGame;

            var referenceCharacters = settings.RussianLanguage ? RussianWords : EnglistWords;
            game = new(referenceCharacters, settings, scroll, characterTrack, characters, this);

            Timer timer = new()
            {
                Interval = 1000,
            };

            timer.Enabled = true;
            timer.Start();
            timer.Elapsed += delegate
            {
                Dispatcher.Invoke(() => waitLabel.Content = backScore);
                if (backScore == 0)
                {
                    Dispatcher.Invoke(() => StartGame());
                    timer.Stop();
                    timer.Enabled = false;
                }
                else if (backScore==1)
                {
                    Dispatcher.Invoke(() => game.Update(0));
                    
                }
                backScore--;
            };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new SettingsPage().ShowDialog();
        }

        public void UpdateCharacterCount(int totalCharacters,int correctCharacters)
        {
            totalCharactersCount.Content = totalCharacters;
            correctCharactersCount.Content = correctCharacters;
        }
    }

    public class Game
    {
        public int correct = 0;
        public int incorrect = 0;
        public int lengthOfGame = 0;
        private List<GraphPoint> graphPoints = new List<GraphPoint>();
        private int currentPosition = 0;
        private string text = "";
        private int minInt = 0;
        private int secInt = 0;
        private int millisecondsInt = 0;

        private Settings Settings;
        private List<string> ReferenceWords;

        System.Timers.Timer timer = new System.Timers.Timer();

        ScrollViewer scrollViewer;
        StackPanel characterTrack;
        List<Label> characters;
        MainWindow mainWindow;

        public Game(List<string> ReferenceWords, Settings Settings,ScrollViewer scrollViewer, StackPanel characterTrack, List<Label> characters,MainWindow mainWindow)
        {                
            this.scrollViewer = scrollViewer;
            this.characterTrack = characterTrack;
            this.characters = characters;
            this.mainWindow = mainWindow;


            correct = 0;                                          
            incorrect = 0;

            graphPoints = new List<GraphPoint>();

            this.ReferenceWords = ReferenceWords;
            this.Settings = Settings;

            lengthOfGame = Settings.LengthOfGame;

            currentPosition = 0;
            text = "";

            minInt = 0;
            secInt = 0;
            millisecondsInt = 0;

            timer.Enabled = false;
            timer.Interval = 10;
            timer.Elapsed += Timer_Elapsed;
            
        }

        public void InputChar(char character)
        {
            var label = null as Label;
            try
            {
                label = characters[currentPosition];
            }
            catch { return; }

            if (label == null)
                return;

            if (text[currentPosition] == character)
            {
                correct++;
                if (label.Content.ToString() != " ")
                {
                    label.Foreground = new SolidColorBrush(Colors.Green);
                }
                double offset = 0;

                for (int i = 0; i < currentPosition; i++)
                {
                    var lab = (characterTrack.Children[i] as Label);
                    if (lab == null)
                        continue;
                    offset += lab.ActualWidth;
                }

                scrollViewer.ScrollToHorizontalOffset(offset - 100);

                currentPosition++;

                Update(offset);
            }
            else
            {
                incorrect++;
                if (label.Content.ToString() != " ")
                {
                    label.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            mainWindow.UpdateCharacterCount(correct + incorrect, correct);

        }

        public void Update(double totalWeight)
        {
            var goalWeihgt = mainWindow.ActualWidth * 1.5;

            while (totalWeight < goalWeihgt)
            {
                var index = Random.Shared.Next(ReferenceWords.Count);
                var word = ReferenceWords[index];
                foreach (var item in word)
                {
                    CreateCharacter(item);
                }
                CreateCharacter(' ');

                totalWeight += word.Length * 41;
            }
        }

        public void CreateCharacter(char character)
        {
            var label = new Label()
            {
                FontSize = 40,
                FontWeight = FontWeights.Bold,
                Content = character,
                Margin = new Thickness(0, 0, 0, 0)
            };
            text += character;
            characters.Add(label);
            characterTrack.Children.Add(label);
        }
        public void StartGame()
        {
            timer.Enabled = true;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (minInt * 60000 + secInt * 1000 + millisecondsInt >= lengthOfGame)
            {
                return;
            }
            millisecondsInt += 10;
            if (millisecondsInt >= 990)
            {
                secInt++;
                millisecondsInt = 0;
                double speed = (double)correct / (double)secInt;
                var GraphPoint = new GraphPoint()
                {
                    Value = speed,
                    Second = secInt,
                };
                graphPoints.Add(GraphPoint);

                mainWindow.CreateCanvas(graphPoints);

            }
            if (secInt >= 59)
            {
                secInt = 0;
                minInt++;
            }
            mainWindow.UpdateTimer(minInt,secInt,millisecondsInt);
        }
    }

    public class Settings
    {
        public bool RussianLanguage { get; set; } = true;
        public int LengthOfGame { get; set; } = 60000;
    }

    public class GraphPoint
    {
        public static double Max { get; set; } = 0;
        private double value;

        public double Value
        {
            get { return value; }
            set { 
                this.value = value;
                if (value>Max)
                {
                    Max = value;
                }
            }
        }

        public int Second { get; set; }
    }
}
