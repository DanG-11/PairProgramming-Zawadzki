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
            Grid.SetRow(fellaLabel, fellaPos[0]);
            Grid.SetColumn(fellaLabel, fellaPos[1]);

            GameBoardGrid.Children.Add(fellaLabel);
        }

        private void Move(object sender, KeyEventArgs e)
        {
            string key = e.Key.ToString();

            if (key == "Up")
            {
                fellaPos[0]--;
                DisplayBoard();
            }
            else if (key == "Down")
            {
                fellaPos[0]++;
                DisplayBoard();
            }
            else if (key == "Left")
            {
                fellaPos[1]--;
                DisplayBoard();
            }
            else if (key == "Right")
            {
                fellaPos[1]++;
                DisplayBoard();
            }
        }
    }
}