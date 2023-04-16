using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.VisualBasic;
using Student_Management_System.Models.ViewModel;

namespace Student_Management_System.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController( UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;

        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Registration(RegistrationViewModel NewAccount) 
        {
            //not need property Problem with model satate 
            //Coustomize IdentityUser ==> create view model the ==> Maping to this view modal 
            //validation on data type ===> meta data 
            if (ModelState.IsValid){
                //Map from View Model to Model
                IdentityUser user = new IdentityUser();
                user.UserName = NewAccount.UserName;
                user.Email = NewAccount.Email;  

                //How to Save user And creata Cookie


                IdentityResult result =  await userManager.CreateAsync(user, NewAccount.Password);

                if(result.Succeeded)//Created Successfyly
                {
                    //create cookie 
                    //Sign in With User
                    await signInManager.SignInAsync(user, isPersistent: false); // Is persistent life time = 20 day ==> false cookie per section 
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    //Reade Creation Result Error 
                    foreach (var  error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(NewAccount);
        }



        //Login 
        //open Link 

        [HttpGet]
        public IActionResult Login(string ReturnUrl = "~/Home/Index")
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Login(LoginViewModel loginUser,string ReturnUrl = "~/Home/Index")
        {
            var urlHelper = Url;
            var redirectUrl = urlHelper.Content(ReturnUrl);
            if (ModelState.IsValid)
            {
                //SingIn
                IdentityUser user = await userManager.FindByNameAsync(loginUser.UserName);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =   
                        await signInManager.PasswordSignInAsync(user,
                                    loginUser.Password,
                    loginUser.IsPersisite,false);

                    
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index), "Home");

                    //return LocalRedirect(redirectUrl);
                    else
                        ModelState.AddModelError("", "Incorrect username or Password ");


                    return View(loginUser);

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or Password");
                }
            }
            return View();
        }


        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
