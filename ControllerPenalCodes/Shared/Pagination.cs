using System;
using System.Collections.Generic;
using System.Linq;

namespace ControllerPenalCodes.Shared
{
	public class Pagination<T>
	{
		public int TotalPages { get; set; }

		public int CurrentPage { get; set; }

		public int TotalItems { get; set; }

		public int TotalItemsByPage { get; set; }

		public IEnumerable<T> ItemsCurrentPage { get; set; }


		public static int CalculateTotalPages(int totalItems, int amountItemsByPage)
		{
			return (totalItems == 0) ? 1 : (int)Math.Ceiling(decimal.Divide(totalItems, amountItemsByPage));
		}

		private static int AdjustCurrentPage(int currentPage, int amountItemsByPage, int totalItems)
		{
			var totalPages = CalculateTotalPages(totalItems, amountItemsByPage);

			currentPage = (currentPage > totalPages) ? totalPages : currentPage;

			return currentPage;
		}

		public static Pagination<T> GetPagination(int totalItems, int amountItemsByPage, int currentPage, IEnumerable<T> itemsCurrentPage)
		{
			var totalPages = CalculateTotalPages(totalItems, amountItemsByPage);

			if (currentPage > totalPages)
				currentPage = totalPages;

			return new Pagination<T>
			{
				TotalPages = totalPages,
				CurrentPage = currentPage,
				TotalItems = totalItems,
				TotalItemsByPage = amountItemsByPage,
				ItemsCurrentPage = itemsCurrentPage
			};
		}

		public static IQueryable<T> PaginateQuery(int currentPage, int amountItemsByPage, int totalItems, IQueryable<T> query)
		{
			currentPage = AdjustCurrentPage(currentPage, amountItemsByPage, totalItems);

			amountItemsByPage = ((amountItemsByPage > totalItems) && totalItems > 0) ? totalItems : amountItemsByPage;

			var amountItemsToSkip = amountItemsByPage * (currentPage - 1);

			query = query
				.Skip(amountItemsToSkip)
				.Take(amountItemsByPage);

			return query;
		}
	}
}
