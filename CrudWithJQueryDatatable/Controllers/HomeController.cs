using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.services;
using CrudWithJQueryDatatable.viewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

//using System.Web.HttpRequestBase

namespace CrudWithJQueryDatatable.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserServices _Services;
        //private readonly IRoleServices _role;

        public HomeController(IUserServices Services/*, IRoleServices role*/)
        {
            _Services = Services;
            //_role = role;
        }

        //GET: Home
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(viewModel.Login l, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userfromdb = _Services.userLogin(l.username);
                    bool verify = userfromdb.IsVerify;

                    if (_Services.VerifyPasswordHash(userfromdb.password, l.password))
                    {
                        if (verify == true)
                        {
                            SignInUser(userfromdb, false);
                            return Redirect("~/Employee/Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Your Account is not verified.please verify your account first.");
                            return Redirect("~/Home/VerifyAccount");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Username or Password.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        private void SignInUser(login currentUser, bool isPersistent)
        {
            var userrole = _Services.UserRoleById(currentUser.id).ToArray();
            //Initialization
            var claims = new List<Claim>();
            try
            {
                //setting
                claims.Add(new Claim(ClaimTypes.Name, currentUser.username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, currentUser.id.ToString()));
                //claims.Add(new Claim(ClaimTypes.Role, currentUser.R_Name));

                foreach (var item in userrole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.R_Name));
                }
                //custom claims
                claims.Add(new Claim("ID", currentUser.id.ToString()));
                claims.Add(new Claim("username", currentUser.username));
                claims.Add(new Claim("email", currentUser.email));
                claims.Add(new Claim("ProfilePic", currentUser.image.ToString()));

                // Id Profile Picutue

                var ClaimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                //signIn
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, ClaimIdenties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClaimIdentities(string username, bool isPersistent)
        {
            //Initialization
            var claims = new List<Claim>();
            try
            {
                //setting
                claims.Add(new Claim(ClaimTypes.Name, username));

                var ClaimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CheckUserNameExists(viewModel.Login userexist)

        {
            bool UserExists = false;
            try
            {
                var nameexits = _Services.userLogin(userexist.username);
                if (nameexits != null && nameexits.username == userexist.username)
                {
                    UserExists = true;
                }
                else
                {
                    UserExists = false;
                }
                return Json(!UserExists, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Signup SignupUser)
        {
            var newUser = new login
            {
                username = SignupUser.username,
                gender = SignupUser.gender,
                email = SignupUser.email,
                password = _Services.CreatePasswordHash(SignupUser.password),
            };
            int a = _Services.AddUser(newUser);
            if (a > 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.RedirectToAction("LogOff", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult changePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult changePassword(changePassword changepassword)
        {
            if (ModelState.IsValid)
            {
                var hannan = customClaims.username(User).ToString();
                var req = _Services.userLogin(hannan);
                if (_Services.VerifyPasswordHash(req.password, changepassword.password))
                {
                    req.password = changepassword.Newpassword;
                    var hash = _Services.CreatePasswordHash(req.password);
                    _Services.updatePassword(req.id, hash);
                    ViewBag.Message = "Password updated successfully.";
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ViewBag.Message = "Old Password is incorrect!";
                    return View();
                }
            }
            return View();
        }

        public ActionResult profile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProfilePicture()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Save()
        //{
        //    string base64 = Request.Form["imgCropped"];
        //    byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
        //    using (FileStream stream = new FileStream(Server.MapPath("~/images/Cropped.png"), FileMode.Create))
        //    {
        //        stream.Write(bytes, 0, bytes.Length);
        //        stream.Flush();
        //    }
        //    return RedirectToAction("Index","Employee");
        //}

        [HttpPost]
        public ActionResult ProfilePicture(profilePicture s)
        {
            var currentUser = customClaims.username(User).ToString();
            var req = _Services.userLogin(currentUser);
            if (s.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
                string extension = Path.GetExtension(s.ImageFile.FileName);
                DateTime datetime = DateTime.Now;
                long tick = datetime.Ticks;
                HttpPostedFileBase postedFile = s.ImageFile;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    fileName = tick + extension;
                    s.image = "~/images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                    s.ImageFile.SaveAs(fileName);
                    req.image = s.image;
                    var newImage = req.image;
                    _Services.updateImage(req.id, newImage);
                    var deleteImg = (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == "ProfilePic").FirstOrDefault();
                    if (deleteImg != null)
                    {
                        string oldimg = Request.MapPath(deleteImg.Value.ToString());
                        if (System.IO.File.Exists(oldimg))
                        {
                            System.IO.File.Delete(oldimg);
                        }
                    }
                    ViewBag.Message = "profile picture updated successfully.";

                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ViewBag.Message = "Extension is not supported!";
                }
            }
            return View();
        }

        public ActionResult userdetails()
        {
            return View();
        }

        public ActionResult VerifyAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyAccount(string email)
        {
            //verify Account
            //generate link
            //send email
            bool status = false;

            {
                var getEmail = _Services.GetUserByEmail(email);
                if (getEmail != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificatonLinkEmail(getEmail.email, resetCode, "AccountVerification");
                    getEmail.ResetPasswordCode = resetCode;
                    _Services.saveResetPasswordCode(email, getEmail.ResetPasswordCode);
                    ViewBag.message = "Verification link has been sent successfully to your email.";
                }
                else
                {
                    ViewBag.message = "Account not found. please enter valid account";
                }
            }
            return View();
        }

        public ActionResult AccountVerification(string id)
        {
            var getResetPasswordCode = _Services.GetResetPasswordCode(id);
            var match = getResetPasswordCode.ResetPasswordCode;
            if (getResetPasswordCode != null && match == id)
            {
                getResetPasswordCode.ResetPasswordCode = "";
                getResetPasswordCode.IsVerify = true;
                _Services.EmptyResetPassword(getResetPasswordCode.id, getResetPasswordCode.IsVerify, getResetPasswordCode.ResetPasswordCode);
                ViewBag.message = "Your Account is verified successfully";
                return Redirect("~/Home/Login");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public void SendVerificatonLinkEmail(string email, string activationCode, string emailFor = "AccountVerification")
        {
            var verifyUrl = "/Home/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("usmanali52a@gmail.com", "Social App");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "mctzcqbrgwutqtie";
            const string subject = "Subject";
            const string body = "Body";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            if (emailFor == "ResetPassword")
            {
                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = "Reset Password",
                    Body = "<h3>Hi,</h3><br/><p>We got request for reset your account password. Please click on the below link to reset your password. Please click on below button:<p><br/><button class='btn btn-default'><a href=" + link + ">Reset Password</a></button> ",
                    IsBodyHtml = true,
                })
                    smtp.Send(message);
            }
            else if (emailFor == "AccountVerification")
            {
                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = "Account Verification",
                    Body = "<h3>Hi,</h3><br/><p>We got request for verify your account. Please click on the below link to verify your account. Please click on below button:<p><br/><button class='btn btn-default'><a href=" + link + ">Verification</a></button> ",
                    IsBodyHtml = true,
                })
                    smtp.Send(message);
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            //verify email
            //generate reset password link
            //send email
            bool status = false;

            {
                var getEmail = _Services.GetUserByEmail(email);
                if (getEmail != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificatonLinkEmail(getEmail.email, resetCode, "ResetPassword");
                    getEmail.ResetPasswordCode = resetCode;
                    _Services.saveResetPasswordCode(email, getEmail.ResetPasswordCode);
                    ViewBag.message = "Reset password link has been sent to your email.";
                }
                else
                {
                    ViewBag.message = "Account not found";
                }
            }
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            var getResetPasswordCode = _Services.GetResetPasswordCode(id);
            if (getResetPasswordCode != null)
            {
                ResetPassword model = new ResetPassword();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                var getResetPasswordCode = _Services.GetResetPasswordCode(model.ResetCode);
                if (getResetPasswordCode != null)
                {
                    getResetPasswordCode.password = model.Newpassword;
                    var hashPass = _Services.CreatePasswordHash(getResetPasswordCode.password);
                    getResetPasswordCode.ResetPasswordCode = "";
                    _Services.ResetPassword(getResetPasswordCode.id, hashPass, getResetPasswordCode.ResetPasswordCode);
                    ViewBag.message = "password updated.";
                }
            }
            else
            {
                ViewBag.message = "invalid";
            }
            return View(model);
        }

        //roles

        //public ActionResult GetUser()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult GetUser(UserDetail ud)
        //{
        //    var user = _Services.GetUserByRole(ud).ToList();
        //    return Json(new { data = user }, JsonRequestBehavior.AllowGet);
        //    //return View(user);
        //}

        //public JsonResult ServerSideProcessing()
        //{
        //    var request = new DataTableRequest();
        //    request.Draw = Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
        //    request.Start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
        //    request.Length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
        //    request.Search = new DataTableSearch()
        //    {
        //        Value = Request.Form["search[value]"]
        //    };
        //    request.Order = new DataTableOrder[] {
        //    new DataTableOrder()
        //    {
        //        Dir = Request.Form["order[0][dir]"],
        //        Column = Convert.ToInt32(Request.Form["order[0][column]"])
        //    }};

        //    //return Json(new { data = _Services.GetAllUserAsync(request).Result, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        //    //return Json();
        //    // return Json(new { _Services.GetAllUserAsync(request).Result }, JsonRequestBehavior.AllowGet);
        //    return Json(_Services.GetAllUserAsync(request).Result, JsonRequestBehavior.AllowGet);
        //}

        //[Authorize(Roles = "User")]
        //public ActionResult CreateNewRole()
        //{
        //    return View();
        //}
        ////[Authorize(Roles = "User")]
        //[HttpPost]
        //public ActionResult CreateNewRole(Role role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int a = _Services.AddRole(role);
        //        return Redirect("~/Home/GetUser");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Not Added! !");

        //    }
        //    return View();
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //public ActionResult EditUser(int id)
        //{
        //    var User = _Services.GetUserById(id).email;
        //    ViewBag.Email = User.ToString();

        //    Session["UserId"] = id;
        //    var role = _Services.GetAllRole(id);
        //    return View(role);
        //}

        //[HttpPost]
        //public ActionResult EditUser(List<UserRoleEdit> UserRoleEdit)
        //{
        //    int UId = (int)Session["UserId"];
        //    var roleChk = UserRoleEdit.Where(x => x.Checked == true);

        //    foreach (var item in roleChk)
        //    {
        //        if (item.Checked == true)
        //        {
        //            _Services.RemoveUserRole(UId, item.R_Id);
        //            _Services.AddUserRole(UId, item.R_Id);

        //        }
        //    };

        //    var roleUchk = UserRoleEdit.Where(x => x.Checked == false);
        //    foreach (var item in roleUchk)
        //    {
        //        _Services.RemoveUserRole(UId, item.R_Id);

        //    };
        //    return Redirect("~/Home/GetUser");
        //}

        //[HttpPost]
        //public ActionResult DeleteUser(int id)
        //{
        //    _Services.DeleteUser(id);
        //    return Redirect("~/Home/GetUser");
        //}
    }
}