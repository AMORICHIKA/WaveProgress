using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace	WaveProgressControl
{
	/// <summary>
	/// WaveProgress.xaml の相互作用ロジック
	/// </summary>
	public	partial	class	WaveProgress : UserControl, INotifyPropertyChanged
	{
		/// <summary>波のイメージ</summary>
		private	Grid	waveGrid1 = WaveImage(10);
		private	Grid	waveGrid2 = WaveImage(20);

		/// <summary>塗りつぶし割合(-0.5～+0.5)</summary>
		public	double	Filling	{	get; set;	} = 0.5;
		/// <summary>進捗値(0～100%)</summary>
		public	double	Progress{
			get {	return	50.0-Filling*100.0;	}
			set {	Filling = (50-value)/100.0;
					OnPropertyChanged("Filling");
					OnPropertyChanged("Percent");
					OnPropertyChanged("WaveDraw");
			}
		}
		/// <summary>波の高さ調整(0～20%,80～100%は波の高さを小さく)</summary>
		public	Grid	WaveDraw{	get {	return	Progress < 20.0 || 80 < Progress ? waveGrid1: waveGrid2;	}	}
		/// <summary>進捗文字列(0～100)</summary>
		public	string	Percent	{	get {	return	$"{Progress:F0}";	}	}

		/// <summary>バインディング更新ハンドラ</summary>
		public	event	PropertyChangedEventHandler	PropertyChanged = (s, e) => { };
		/// <summary>プロパティの更新イベント</summary>
		public	void	OnPropertyChanged(string name)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public	WaveProgress()
		{
			InitializeComponent();
			Loaded += WaveProgress_Loaded;

			DataContext = this;
		}
		/// <summary>
		/// ロードイベント
		/// </summary>
		/// <param name="sender">送信元</param>
		/// <param name="e">イベント情報</param>
		private	void	WaveProgress_Loaded(object sender, RoutedEventArgs e)
		{
			var	sb = new Storyboard();
			var	an = new DoubleAnimation()
			{
				RepeatBehavior = RepeatBehavior.Forever,
				From = 0,
				To	 = 1,
				Duration = new Duration(TimeSpan.FromMilliseconds(1000))
			};
			Storyboard.SetTargetProperty(an, new PropertyPath("(UIElement.OpacityMask).(Brush.RelativeTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
			Storyboard.SetTarget(an, ellipse);
			sb.Children.Add(an);
			sb.Begin();
		}
		/// <summary>
		/// 波の描画
		/// </summary>
		/// <param name="h">波の高さ</param>
		private	static	Grid	WaveImage(double h)
		{
			// 背景を設定
			var	grid = new Grid();
			grid.Width = 160;
			grid.Height= 377;
			grid.Background = new SolidColorBrush(Colors.Transparent);
			grid.RowDefinitions.Add(new RowDefinition{ Height = new GridLength(1.0, GridUnitType.Star) });
			grid.RowDefinitions.Add(new RowDefinition{ Height = new GridLength(h) });
			grid.RowDefinitions.Add(new RowDefinition{ Height = new GridLength(1.0, GridUnitType.Star) });
			var	col = Color.FromArgb(0xFF, 0x82, 0xC6, 0xFF);
			var	geo = "M12.5,1.6925709 C31.25,1.6925709 31.25,18.615654 50,18.615654 68.75,18.615654 68.75,1.6925709 87.5,1.6925709 87.499909,27.077196 87.5,27.077107 87.5,27.077107 63.28125,27.077136 12.5,27.077196 12.5,27.077196 12.5,27.077196 12.500094,27.077196 12.5,1.6925709 z";
			var	pah = new Path()
			{
				Fill = new SolidColorBrush(col),
				Data = Geometry.Parse(geo),
				Stretch = Stretch.Fill,
			};
			grid.Children.Add(pah);
			var	rec = new Rectangle()
			{
				Fill = new SolidColorBrush(col),
			};
			grid.Children.Add(rec);
			Grid.SetRow(pah, 1);
			Grid.SetRow(rec, 2);
			return	grid;
		}
	}
}
