// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
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
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow" /> class.
		/// </summary>
		public MainWindow()
			: base(DataWindowMode.Custom)
		{
			InitializeComponent();
		}
		#endregion
	}
}