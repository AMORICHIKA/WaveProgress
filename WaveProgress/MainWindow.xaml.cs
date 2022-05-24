using System.Windows;

// https://www.web-dev-qa-db-ja.com/ja/c%23/%E6%A5%95%E5%86%86%E3%82%92%E6%B3%A2%E3%81%AE%E3%82%A2%E3%83%8B%E3%83%A1%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%81%A7%E5%A1%97%E3%82%8A%E3%81%A4%E3%81%B6%E3%81%99/834332561/
// https://stackoverflow.com/questions/22044041/animate-circles-fill-using-sine-wave

namespace WaveProgress
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var	test = new TestWindow();
			test.Show();
		}
	}
}
