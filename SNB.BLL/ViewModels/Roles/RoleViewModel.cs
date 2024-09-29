using System.ComponentModel.DataAnnotations;

namespace SNB.BLL.ViewModels.Roles
{
    /// <summary>
    /// Модель роли
    /// </summary>
    public class RoleViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
