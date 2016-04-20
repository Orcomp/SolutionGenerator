// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleView.xaml.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
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
		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleView" /> class.
		/// </summary>
		public ConsoleView()
		{
			InitializeComponent();
		}

		protected override void OnViewModelPropertyChanged(PropertyChangedEventArgs e)
		{
			if (string.Equals(e.PropertyName, "Output"))
			{
				outputTextBox.ScrollToEnd();
			}
		}
	}
}