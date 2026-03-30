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
        int[,] gameBoard = new int[10, 10];
        int[] fellaPos = new int[]{0, 0};
        public MainWindow()
        {
            InitializeComponent();

            DisplayBoard();
        }

        private void DisplayBoard()
        {
            GameBoardGrid.Children.Clear();

            Label fellaLabel = new Label();
            fellaLabel.Content = "Ziutek";
            Grid.SetColumn(fellaLabel, fellaPos[0]);
            Grid.SetRow(fellaLabel, fellaPos[1]);

            GameBoardGrid.Children.Add(fellaLabel);
        }

        private void Move(object sender, KeyEventArgs e)
        {
            string key = e.Key.ToString();

            if (key == "Up")
            {
                if (fellaPos[1] != 0 /*&& gameBoard[fellaPos[0], fellaPos[1] - 1] != 0 */)
                {
                    fellaPos[1]--;
                    DisplayBoard();
                }
            }
            else if (key == "Down")
            {
                if (fellaPos[1] != 9 /*&& gameBoard[fellaPos[0], fellaPos[1] + 1] != 0 */)
                {
                    fellaPos[1]++;
                    DisplayBoard();
                }
            }
            else if (key == "Left")
            {
                if (fellaPos[0] != 0 /*&& gameBoard[fellaPos[0] - 1, fellaPos[1]] != 0 */)
                {
                    fellaPos[0]--;
                    DisplayBoard();
                }
            }
            else if (key == "Right")
            {
                if (fellaPos[0] != 9 /*&& gameBoard[fellaPos[0] + 1, fellaPos[1]] != 0 */)
                {
                    fellaPos[0]++;
                    DisplayBoard();
                }
            }
        }
    }
}