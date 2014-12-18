// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleView.xaml.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Wpf.Views
{
	using System.ComponentModel;
	using Catel.Windows.Controls;

	/// <summary>
	/// Interaction logic for ConsoleView.xaml.
	/// </summary>
	public partial class ConsoleView : UserControl
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleView" /> class.
		/// </summary>
		public ConsoleView()
		{
			InitializeComponent();
		}
		#endregion

		#region Methods
		protected override void OnViewModelPropertyChanged(PropertyChangedEventArgs e)
		{
			if (string.Equals(e.PropertyName, "Output"))
			{
				outputTextBox.ScrollToEnd();
			}
		}
		#endregion
	}
}