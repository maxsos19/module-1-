using System.ComponentModel.DataAnnotations;

namespace SNB.BLL.ViewModels.Users
{
    /// <summary>
    /// Модель входа
    /// </summary>
    public class UserLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Введите email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string? Password { get; set; }
    }
}
