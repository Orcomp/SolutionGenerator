// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Wpf.Views
{
	using Catel.Windows;

	/// <summary>
	/// Interaction logic for MainWindow.xaml.
	/// </summary>
	public partial class MainWindow : DataWindow
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow" /> class.
		/// </summary>
		public MainWindow()
			: base(DataWindowMode.Custom)
		{
			InitializeComponent();
		}
	}
}