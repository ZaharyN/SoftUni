using System.ComponentModel.DataAnnotations;
using static ForumApp.Data.Constants.ValidationConstants;

namespace ForumApp.Core.Models
{
	public class PostModel
	{
		public int Id { get; init; }

		[Required(ErrorMessage = RequireErrorMessage)]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = StringLengthErrorMessage)]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		[StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = StringLengthErrorMessage)]
		public string Content { get; set; } = string.Empty;
	}
}
