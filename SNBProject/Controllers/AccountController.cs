using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNB.BLL.Services.IServices;
using SNB.BLL.ViewModels.Users;
using NLog;

namespace SNBProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// [Get] Метод, login
        /// </summary>
        [Route("Account/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// [Post] Метод, login
        /// </summary>
        [Route("Account/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(model);

                if (result.Succeeded)
                {
                    Logger.Info($"Осуществлен вход пользователя с адресом - {model.Email}");
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        /// <summary>
        /// [Get] Метод, создания пользователя
        /// </summary>
        [Route("Account/Create")]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        /// <summary>
        /// [Post] Метод, создания пользователя
        /// </summary>
        [Route("Account/Create")]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUser(model);

                if (result.Succeeded)
                {
                    Logger.Info($"Создан аккаунт, пользователем с правами администратора, с использованием адреса - {model.Email}");
                    return RedirectToAction("GetAccounts", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// [Get] Метод, регистрации
        /// </summary>
        [Route("Account/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// [Post] Метод, регистрации
        /// </summary>
        [Route("Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Register(model);

                if (result.Succeeded)
                {
                    Logger.Info($"Создан аккаунт с использованием адреса - {model.Email}");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// [Get] Метод, редактирования аккаунта
        /// </summary>
        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditAccount(Guid id)
        {
            var model = await _accountService.EditAccount(id);

            return View(model);
        }

        /// <summary>
        /// [Post] Метод, редактирования аккаунта
        /// </summary>
        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditAccount(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.EditAccount(model);
                Logger.Info($"Аккаунт {model.UserName} был изменен");

                return RedirectToAction("GetAccounts", "Account");
            }

            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, удаление аккаунта
        /// </summary>
        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveAccount(Guid id, bool confirm = true)
        {
            if (confirm)

                await RemoveAccount(id);

            return RedirectToAction("GetAccounts", "Account");
        }

        /// <summary>
        /// [Post] Метод, удаление аккаунта
        /// </summary>
        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveAccount(Guid id)
        {
            var account = await _accountService.GetAccount(id);
            await _accountService.RemoveAccount(id);
            Logger.Info($"Аккаунт с id - {id} удален");

            return RedirectToAction("GetAccounts", "Account");
        }

        /// <summary>
        /// [Post] Метод, выхода из аккаунта
        /// </summary>
        [Route("Account/Logout")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LogoutAccount()
        {
            await _accountService.LogoutAccount();
            Logger.Info($"Осуществлен выход из аккаунта");

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, получения всех пользователей
        /// </summary>
        [Route("Account/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> GetAccounts()
        {
            var users = await _accountService.GetAccounts();

            return View(users);
        }

        /// <summary>
        /// [Get] Метод, получения одного пользователя по Id
        /// </summary>
        [Route("Account/Details")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> DetailsAccount(Guid id)
        {
            var model = await _accountService.GetAccount(id);

            return View(model);
        }
    }
}
