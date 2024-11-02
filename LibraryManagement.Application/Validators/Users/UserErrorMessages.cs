using System.Runtime.InteropServices;

namespace LibraryManagement.Application.Validators.Users
{
    public static class UserErrorMessages
    {
        public static string NameEmpty = "Nome tem que ser preenchido!";
        public static string NameMaximumLength = "O Nome pode ter no máximo 100 caracteres";

        public static string EmailEmpty = "Email tem que ser preenchido!";
        public static string EmailNotStandard = "Email não deve conter números ou hífens para separadores!";
        public static string EmailMaximumLength = "O Email pode ter no máximo 100 caracteres";


        public static string PasswordEmpty = "Senha tem que ser preenchida!";
        public static string PasswordNotStandard = "Senha deve conter ao menos um dígito, uma letra minúscula, uma letra maiúscula e um caractere especial (ex: @#)!";
        public static string PasswordMaximumLength = "O Password pode ter no máximo 20 caracteres";
        public static string PasswordMinimumLength = "O Password pode ter no máximo 20 caracteres";
    }
}
