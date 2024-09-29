using System.ComponentModel.DataAnnotations;

namespace SNB.BLL.ViewModels.Comments
{
    /// <summary>
    /// Модель изменения комментария
    /// </summary>
    public class CommentEditViewModel
    {
        /// <summary>
        /// Содержание комментария
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Содержание", Prompt = "Содержание")]
        public string? Content { get; set; }

        /// <summary>
        /// Автор комментария
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Автор", Prompt = "Автор")]
        public string? Author { get; set; }

        /// <summary>
        /// Id комментария
        /// </summary>
        public Guid Id { get; set; }
    }
}
