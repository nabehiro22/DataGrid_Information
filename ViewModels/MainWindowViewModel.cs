using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;

namespace DataGrid_Information.ViewModels
{
	public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
	{
		public readonly ReactivePropertySlim<string> Title = new ReactivePropertySlim<string>("DataGrod_Informtion");

		/// <summary>
		/// CurrentCellプロパティにBinding
		/// </summary>
		public ReactivePropertySlim<DataGridCellInfo> CellInfo { get; } = new ReactivePropertySlim<DataGridCellInfo>();

		/// <summary>
		/// SelectedIndexプロパティにBinding
		/// </summary>
		public ReactivePropertySlim<int> Index { get; } = new ReactivePropertySlim<int>();

		/// <summary>
		/// DataGridのMouseDoubleClickイベント
		/// </summary>
		public ReactiveCommand DataTableEvent { get; } = new ReactiveCommand();

		/// <summary>
		/// ウィンドウのClosedイベント
		/// </summary>
		public ReactiveCommand ClosedCommand { get; } = new ReactiveCommand();

		/// <summary>
		/// データグリッドに表示するデータ
		/// </summary>
		public ObservableCollection<Data> SettingList { get; } = new ObservableCollection<Data>();

		/// <summary>
		/// Disposeが必要な処理をまとめてやる
		/// </summary>
		private CompositeDisposable Disposable { get; } = new CompositeDisposable();

		public MainWindowViewModel()
		{
			// Windowが閉じられる時はClosedメソッドを実行
			ClosedCommand.Subscribe(Closed).AddTo(Disposable);
			// DataGridがダブルクリックされた時はgetRowAndColumnメソッドを実行
			DataTableEvent.Subscribe(getRowAndColumn).AddTo(Disposable);

			SettingList.Add(new Data
			{
				Name = "中田 花子",
				Age = 25,
				Department = "総務課"
			});
			SettingList.Add(new Data
			{
				Name = "本山 太郎",
				Age = 30,
				Department = "開発課"
			});
			SettingList.Add(new Data
			{
				Name = "本山 太郎",
				Age = 25,
				Department = "総務課"
			});
		}

		/// <summary>
		/// Windowsが閉じられる時に実行するメソッド
		/// </summary>
		private void Closed()
		{
			Disposable.Dispose();
		}

		/// <summary>
		/// DataGridのセルがダブルクリックされた時に実行するメソッド
		/// </summary>
		private void getRowAndColumn()
		{
			MessageBox.Show(Application.Current.MainWindow, $"クリックされたセルは\r\n行：{Index.Value}\r\n列：{CellInfo.Value.Column.DisplayIndex}\r\nです。", "Cell情報", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}

	public class Data
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public string Department { get; set; }
	}
}
