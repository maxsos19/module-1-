using System.ComponentModel.DataAnnotations;

namespace SNB.BLL.ViewModels.Tags
{
    /// <summary>
    /// Модель изменения тега
    /// </summary>
    public class TagEditViewModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string? Name { get; set; }
    }
}
