using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ChessApp.ViewModels;

namespace ChessApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ChessViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
