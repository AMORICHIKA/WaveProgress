using System.Windows;

namespace WaveProgress
{
	/// <summary>
	/// TestWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class TestWindow : Window
	{
		public TestWindow()
		{
			InitializeComponent();
		}

		private void volume1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			progress.Progress = volume1.Value;	// 0～100%
		}
	}
}
