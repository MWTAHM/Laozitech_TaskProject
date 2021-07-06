using BLL.Project;
using Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace TaskProject.Pages.UserControl
{
    public partial class WebForm2 : Page
    {
        public static string EncryptPassword(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string DecryptPassword(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(!ValidateForm(out DateTime? bitrhdate))
                {
                    return;
                }

                var newUser = new UserModel
                {
                    Address1 = UAddress1.Text,
                    Address2 = UAddress2.Text,
                    BirthDate = bitrhdate,
                    City = UCity.Text,
                    Country = UCountry.Text,
                    CurrentProjectId = null,
                    CurrentTaskId = null,
                    EmailAddress = UEmail.Text,
                    FullName = UFullName.Text,
                    IsEmailConfirmed = false,
                    PhoneNumber = UPhone.Text,
                    Postal = UPostal.Text,
                    UserName = UName.Text,
                    UserRegistratonTime = DateTime.Now,
                    PasswordEncoded = EncryptPassword(Password.Text)
                };

                UserController.Register(newUser);
                Response.Redirect("List");
            }
        }

        private bool ValidateForm(out DateTime? bitrhdate)
        {
            /* Server Side Validation */
            validationMessages.Text = "Check ";
            bool IsValid = true;
            if (string.IsNullOrWhiteSpace(UName.Text))
            {
                IsValid = false;
                validationMessages.Text += "UserName, ";
            }

            if (string.IsNullOrWhiteSpace(UFullName.Text))
            {
                IsValid = false;
                validationMessages.Text += "FullName, ";
            }


            if (string.IsNullOrWhiteSpace(UEmail.Text))
            {
                IsValid = false;
                validationMessages.Text += "Email, ";
            }

            bitrhdate = null;
            try
            {
                bitrhdate = DateTime.Parse(BirthDate.Text);
            }
            catch (Exception)
            {
            }

            if (string.IsNullOrWhiteSpace(Password.Text) ||
                string.IsNullOrWhiteSpace(ConfirmPassword.Text))
            {
                IsValid = false;
                validationMessages.Text += "Passwords, ";
            }

            if (string.IsNullOrWhiteSpace(UPhone.Text))
            {
                IsValid = false;
                validationMessages.Text += "Phone, ";
            }

            if (string.IsNullOrWhiteSpace(UCountry.Text))
            {
                IsValid = false;
                validationMessages.Text += "Country, ";
            }

            if (string.IsNullOrWhiteSpace(UCity.Text))
            {
                IsValid = false;
                validationMessages.Text += "City, ";
            }
            if (string.IsNullOrWhiteSpace(UAddress1.Text))
            {
                IsValid = false;
                validationMessages.Text += "Address1, ";
            }

            if (string.IsNullOrWhiteSpace(UPostal.Text))
            {
                IsValid = false;
                validationMessages.Text += "Postal, ";
            }
            /* Server Side Validation */

            if (!IsValid)
            {
                validationMessages.Text = validationMessages.Text.Remove(validationMessages.Text.LastIndexOf(','));
            }

            return IsValid;
        }
    }
}