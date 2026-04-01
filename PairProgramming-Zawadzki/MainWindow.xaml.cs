using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PairProgramming_Zawadzki
{
    public partial class MainWindow : Window
    {
        int collectedEggs = 0;

        Random fellaRandPos1 = new Random();
        Random fellaRandPos2 = new Random();

        int[,] gameBoard = new int[10, 10];
        int[] fellaPos = new int[] { };
        (int x, int y)[] easterEggPos = new (int, int)[10];
        (int x, int y)[] wallPos = new (int, int)[40];
        public MainWindow()
        {
            InitializeComponent();
            fellaPos = new int[] { fellaRandPos1.Next(0, 10), fellaRandPos2.Next(0, 10) };
            InitalizeEasterBoard();
            DisplayBoard();
        }

        private void InitalizeEasterBoard()
        {
            Random easterEggRand = new Random();
            Random wallRand = new Random();

            // 1️⃣ Generacja ścian
            for (int i = 0; i < 40; i++)
            {
                (int x, int y) pos;
                do
                {
                    pos = (wallRand.Next(0, 10), wallRand.Next(0, 10));
                }
                while (IsOccupied(pos, "wall")); // powtarzaj jeśli zajęte

                wallPos[i] = pos;

                gameBoard[pos.x, pos.y] = 1;
            }

            // 2️⃣ Generacja jajek
            for (int i = 0; i < 10; i++)
            {
                (int x, int y) pos;
                do
                {
                    pos = (easterEggRand.Next(0, 10), easterEggRand.Next(0, 10));
                }
                while (IsOccupied(pos, "egg")); // powtarzaj jeśli zajęte

                easterEggPos[i] = pos;

                gameBoard[pos.x, pos.y] = 2;
            }
        }

        // 🔹 Funkcja sprawdzająca kolizję
        private bool IsOccupied((int x, int y) pos, string type)
        {
            // sprawdzanie ścian
            for (int w = 0; w < 40; w++)
            {
                if (wallPos[w] == pos)
                {
                    if (type == "wall" || type == "egg") return true;
                }
            }

            // sprawdzanie jajek
            for (int e = 0; e < 10; e++)
            {
                if (easterEggPos[e] == pos)
                {
                    if (type == "egg" || type == "wall") return true;
                }
            }

            // sprawdzanie gracza (tylko dla jajek)
            if (type == "egg" && pos == (fellaPos[0], fellaPos[1])) return true;

            return false;
        }

        private void DisplayBoard()
        {
            GameBoardGrid.Children.Clear();

            Label fellaLabel = new Label();
            fellaLabel.Content = "Ziutek";
            fellaLabel.Width = Double.NaN;
            fellaLabel.Height = Double.NaN;
            Grid.SetColumn(fellaLabel, fellaPos[0]);
            Grid.SetRow(fellaLabel, fellaPos[1]);

            GameBoardGrid.Children.Add(fellaLabel);

            Label[] easterEggLabel = new Label[10];

            for (int i = 0; i < 10; i++)
            {
                if (gameBoard[easterEggPos[i].x, easterEggPos[i].y] == 2)
                {
                    easterEggLabel[i] = new Label();
                    easterEggLabel[i].Content = $"Easter Egg{i + 1}";
                    easterEggLabel[i].Width = Double.NaN;
                    easterEggLabel[i].Height = Double.NaN;
                    Grid.SetColumn(easterEggLabel[i], easterEggPos[i].x);
                    Grid.SetRow(easterEggLabel[i], easterEggPos[i].y);

                    GameBoardGrid.Children.Add(easterEggLabel[i]);
                }
            }

            Label[] wallLabel = new Label[40];

            for(int i = 0; i < 40; i++)
            {
                wallLabel[i] = new Label();
                wallLabel[i].Content = $"Wall{i+1}";
                wallLabel[i].Width = Double.NaN;
                wallLabel[i].Height = Double.NaN;
                wallLabel[i].Background = Brushes.Black;
                Grid.SetColumn(wallLabel[i], wallPos[i].x);
                Grid.SetRow(wallLabel[i], wallPos[i].y);

                GameBoardGrid.Children.Add(wallLabel[i]);
            }
        }

        private void Move(object sender, KeyEventArgs e)
        {
            string key = e.Key.ToString();

            int newX = fellaPos[0];
            int newY = fellaPos[1];

            // Obliczamy nowe współrzędne
            if (key == "Up") newY--;
            else if (key == "Down") newY++;
            else if (key == "Left") newX--;
            else if (key == "Right") newX++;

            // 1️⃣ Sprawdzenie granic planszy
            if (newX < 0 || newX >= 10 || newY < 0 || newY >= 10)
                return; // nie ruszamy gracza

            // 2️⃣ Sprawdzenie ściany
            if (gameBoard[newX, newY] == 1)
                return; // blokada ruchu

            // 3️⃣ Sprawdzenie jajka
            if (gameBoard[newX, newY] == 2)
            {
                gameBoard[newX, newY] = 0; // usuń jajko
                collectedEggs++;           // zwiększ licznik

                // ✅ Sprawdzenie wygranej
                if (collectedEggs == 10)
                {
                    MessageBox.Show("Wygrałeś! Zebrałeś wszystkie jajka!");
                }
            }

            // 4️⃣ Wykonanie ruchu
            fellaPos[0] = newX;
            fellaPos[1] = newY;

            DisplayBoard(); // rysujemy planszę
        }
    }
}