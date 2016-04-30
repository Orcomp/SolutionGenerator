// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EFEntityPluralService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	using System.Data.Entity.Infrastructure.Pluralization;
	using Orc.Csv;

	/// <summary>
	/// Class EfEntityPluralService. 
	/// Entity Framework's IPluralizationService bases implementation of Orc.CsvHelper's IEntityPluralService 
	/// </summary>
	internal class EfEntityPluralService : IEntityPluralService
	{
		private readonly IPluralizationService _pluralizationService;

		/// <summary>
		/// Initializes a new instance of the <see cref="EfEntityPluralService"/> class.
		/// Accepts pluggable IPluralizationService as constructor parameter
		/// </summary>
		/// <param name="pluralizationService">The pluralization service.</param>
		public EfEntityPluralService(IPluralizationService pluralizationService)
		{
			_pluralizationService = pluralizationService;
		}

		/// <summary>
		/// Converts the input parameter to its plural form. 
		/// Leaves the parameter intact if it is already in its plural form
		/// </summary>
		/// <param name="entity">The entity to be pluralized.</param>
		/// <returns>System.String.</returns>
		public string ToPlural(string entity)
		{
			return _pluralizationService.Pluralize(entity);
		}

		/// <summary>
		/// Converts the input parameter to its singular form. 
		/// Leaves the parameter intact if it is already in its singular form
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>System.String.</returns>
		public string ToSingular(string entity)
		{
			return _pluralizationService.Singularize(entity);
		}
	}
}