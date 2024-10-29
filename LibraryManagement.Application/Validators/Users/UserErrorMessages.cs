using System.Runtime.InteropServices;

namespace LibraryManagement.Application.Validators.Users
{
    public static class UserErrorMessages
    {
        public static string NameEmpty = "Nome tem que ser preenchido!";
        public static string NameMaximuLength = "O Nome pode ter no máximo 100 caracteres";

        public static string EmailEmpty = "Email tem que ser preenchido!";
        public static string EmailNotStandard = "Email não deve conter números ou hífens para separadores!";
        public static string EmailMaximuLength = "O Email pode ter no máximo 100 caracteres";

        public static string PasswordNotStandard = "Senha deve conter ao menos um dígito, uma letra minúscula, uma letra maiúscula, um caractere especial e no máximo 20 dos caracteres!";
        public static string PasswordMaximuLength = "O Password pode ter no máximo 20 caracteres";
    }
}
